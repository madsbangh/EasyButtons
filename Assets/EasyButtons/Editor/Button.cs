namespace EasyButtons.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    internal readonly struct Button
    {
        private readonly string _name;
        private readonly ButtonSpacing _spacing;
        private readonly bool _enabled;
        private readonly bool _hasParams;
        private readonly MethodInfo _method;
        private readonly ParamInfo[] _parameters;

        public Button(MethodInfo method, ButtonAttribute buttonAttribute)
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

        private void Invoke(IEnumerable<Object> objects)
        {
            var paramValues = _hasParams ? _parameters.Select(param => param.GetValue()).ToArray() : null;

            foreach (Object obj in objects)
            {
                _method.Invoke(obj, paramValues);
            }
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