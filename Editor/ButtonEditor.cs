using UnityEditor;

namespace EasyButtons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Custom inspector for Object including derived classes.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ObjectEditor : Editor
    {
        private readonly List<ButtonInfo> _buttons = new List<ButtonInfo>();

        protected virtual void OnEnable()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;

                _buttons.Add(new ButtonInfo(method, buttonAttribute));
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawEasyButtons();
        }

        protected void DrawEasyButtons()
        {
            foreach (ButtonInfo button in _buttons)
            {
                button.Draw(targets);
            }
        }

        private readonly struct ButtonInfo
        {
            private readonly string _name;
            private readonly ButtonSpacing _spacing;
            private readonly bool _enabled;
            private readonly bool _hasParams;
            private readonly MethodInfo _method;
            private readonly ParamInfo[] _parameters;

            public ButtonInfo(MethodInfo method, ButtonAttribute buttonAttribute)
            {
                _name = string.IsNullOrEmpty(buttonAttribute.Name)
                    ? ObjectNames.NicifyVariableName(method.Name)
                    : buttonAttribute.Name;

                _spacing = buttonAttribute.Spacing;

                bool inAppropriateMode = EditorApplication.isPlaying
                    ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                    : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

                _enabled = buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;

                var parameters = method.GetParameters();
                _hasParams = parameters.Length != 0;
                _parameters = parameters.Select(parameter => new ParamInfo(parameter)).ToArray();
                _method = method;
            }

            private void Invoke(IEnumerable<Object> objects)
            {
                var paramValues = _hasParams ? _parameters.Select(param => param.GetValue()).ToArray() : null;

                foreach (Object obj in objects)
                {
                    _method.Invoke(obj, paramValues);
                }
            }

            public void Draw(IEnumerable<Object> targets)
            {
                bool previousValue = GUI.enabled;
                GUI.enabled = _enabled;

                DrawButtonWithSpacing(targets);

                foreach (ParamInfo param in _parameters)
                {
                    param.Draw();
                }

                GUI.enabled = previousValue;
            }

            private void DrawButtonWithSpacing(IEnumerable<Object> targets)
            {
                const float spacingHeight = 10f;

                if (_spacing.HasFlag(ButtonSpacing.Before))
                    GUILayout.Space(spacingHeight);

                if (GUILayout.Button(_name))
                    Invoke(targets);

                if (_spacing.HasFlag(ButtonSpacing.After))
                    GUILayout.Space(spacingHeight);
            }

            private readonly struct ParamInfo
            {
                private readonly FieldInfo _fieldInfo;
                private readonly ScriptableObject _scriptableObj;
                private readonly Editor _editor;

                public ParamInfo(ParameterInfo paramInfo)
                {
                    Type generatedType = ScriptableObjectCache.GetClass(paramInfo.Name, paramInfo.ParameterType);
                    _scriptableObj = ScriptableObject.CreateInstance(generatedType);
                    _fieldInfo = generatedType.GetField(paramInfo.Name);
                    _editor = Editor.CreateEditor(_scriptableObj, typeof(NoScriptFieldEditor));
                }

                public object GetValue() => _fieldInfo.GetValue(_scriptableObj);

                public void Draw()
                {
                    _editor.OnInspectorGUI();
                }
            }
        }
    }
}
