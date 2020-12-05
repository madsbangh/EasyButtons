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
            // Draw the rest of the inspector as usual
            DrawDefaultInspector();
            DrawEasyButtons();
        }

        protected void DrawEasyButtons()
        {
            int buttonsLength = _buttons.Count;

            // Iterate without foreach to avoid losing performance on structs boxing.
            for (int i = 0; i < buttonsLength; i++)
            {
                var button = _buttons[i];

                if (button.HasParams)
                {
                    DrawButtonWithParams(button);
                }
                else
                {
                    DrawButtonWithoutParams(button);
                }
            }
        }

        private void DrawButtonWithoutParams(ButtonInfo button)
        {
            const float spacingHeight = 10f;

            DrawWithEnabledGUI(button.Enabled, () =>
            {
                if (button.Spacing.HasFlag(ButtonSpacing.Before))
                    GUILayout.Space(spacingHeight);

                if (GUILayout.Button(button.Name))
                    button.Invoke(targets, null);

                if (button.Spacing.HasFlag(ButtonSpacing.After))
                    GUILayout.Space(spacingHeight);
            });
        }

        private static void DrawWithEnabledGUI(bool enabled, Action drawStuff)
        {
            bool previousValue = GUI.enabled;
            GUI.enabled = enabled;
            drawStuff();
            GUI.enabled = previousValue;
        }

        private void DrawButtonWithParams(ButtonInfo button)
        {
            GUILayout.Button(button.Name);

            foreach (Editor paramEditor in button.Parameters)
            {
                paramEditor.OnInspectorGUI();
            }
        }

        private readonly struct ButtonInfo
        {
            public readonly string Name;
            public readonly ButtonSpacing Spacing;
            public readonly bool Enabled;
            public readonly bool HasParams;
            public readonly Editor[] Parameters;
            private readonly MethodInfo _method;

            public ButtonInfo(MethodInfo method, ButtonAttribute buttonAttribute)
            {
                Name = string.IsNullOrEmpty(buttonAttribute.Name)
                    ? ObjectNames.NicifyVariableName(method.Name)
                    : buttonAttribute.Name;

                Spacing = buttonAttribute.Spacing;

                bool inAppropriateMode = EditorApplication.isPlaying
                    ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                    : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

                Enabled = buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;

                var parameters = method.GetParameters();
                HasParams = parameters.Length != 0;
                Parameters = parameters.Select(CreateEditor).ToArray();

                _method = method;
            }

            public void Invoke(IEnumerable<Object> objects, params object[] parameters)
            {
                foreach (Object obj in objects)
                {
                    _method.Invoke(obj, parameters);
                }
            }

            private static Editor CreateEditor(ParameterInfo parameter)
            {
                var generatedType = ScriptableObjectCache.GetClass(parameter.Name, parameter.ParameterType);
                var scriptableObject = ScriptableObject.CreateInstance(generatedType);
                var editor = Editor.CreateEditor(scriptableObject, typeof(NoScriptFieldEditor));
                return editor;
            }
        }
    }
}
