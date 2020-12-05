using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EasyButtons
{
    public static class EasyButtonsEditorExtensions
    {
        public static void DrawEasyButtons(this Editor editor)
        {
            // Loop through all methods
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = editor.target.GetType().GetMethods(flags);

            foreach (var method in methods)
            {
                // Get the ButtonAttribute on the method (if any)
                var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

                if (buttonAttribute == null)
                    return;

                if (method.GetParameters().Length == 0)
                {
                    DrawButtonWithoutParams(editor, method, buttonAttribute);
                }
                else
                {
                    DrawButtonWithParams(editor, method, buttonAttribute);
                }
            }
        }

        private static void DrawButtonWithoutParams(Editor editor, MethodInfo method, ButtonAttribute buttonAttribute)
        {
            // Determine whether the button should be enabled based on its mode
            var wasEnabled = GUI.enabled;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

            GUI.enabled = buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;


            if (buttonAttribute.Spacing.HasFlag(ButtonSpacing.Before))
                GUILayout.Space(10);

            // Draw a button which invokes the method
            var buttonName = GetButtonName(method, buttonAttribute);

            if (GUILayout.Button(buttonName))
            {
                foreach (var t in editor.targets)
                {
                    method.Invoke(t, null);
                }
            }

            if (buttonAttribute.Spacing.HasFlag(ButtonSpacing.After))
                GUILayout.Space(10);

            GUI.enabled = wasEnabled;
        }

        private static void DrawButtonWithParams(Editor editor, MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var buttonName = GetButtonName(method, buttonAttribute);
            GUILayout.Button(buttonName);
        }

        private static string GetButtonName(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            return string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(method.Name)
                : buttonAttribute.Name;
        }
    }
}
