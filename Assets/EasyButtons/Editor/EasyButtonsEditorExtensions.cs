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
                if (method.GetParameters().Length == 0)
                {
                    DrawButtonWithoutParams(editor, method);
                }
                else
                {
                    DrawButtonWithParams(editor, method);
                }
            }
        }

        private static void DrawButtonWithoutParams(Editor editor, MethodInfo method)
        {
            // Get the ButtonAttribute on the method (if any)
            var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

            if (ba == null)
                return;

            // Determine whether the button should be enabled based on its mode
            var wasEnabled = GUI.enabled;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? ba.Mode == ButtonMode.EnabledInPlayMode
                : ba.Mode == ButtonMode.DisabledInPlayMode;

            GUI.enabled = ba.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;


            if (((int)ba.Spacing & (int)ButtonSpacing.Before) != 0)
                GUILayout.Space(10);

            // Draw a button which invokes the method
            var buttonName = string.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;

            if (GUILayout.Button(buttonName))
            {
                foreach (var t in editor.targets)
                {
                    method.Invoke(t, null);
                }
            }

            if (((int)ba.Spacing & (int)ButtonSpacing.After) != 0)
                GUILayout.Space(10);

            GUI.enabled = wasEnabled;
        }

        private static void DrawButtonWithParams(Editor editor, MethodInfo method)
        {

        }
    }
}
