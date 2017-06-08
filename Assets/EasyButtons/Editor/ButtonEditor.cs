using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace EasyButtons
{
    /// <summary>
    /// Custom inspector for Object including derived classes.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class ObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Loop through all methods with no parameters
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);
            foreach (var method in methods)
            {
                // Get the ButtonAttribute on the method (if any)
                var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

                if (ba != null)
                {
                    // Determine whether the button should be enabled based on its mode
                    GUI.enabled = ba.mode == ButtonMode.AlwaysEnabled
                        || (EditorApplication.isPlaying ? ba.mode == ButtonMode.EnabledInPlayMode : ba.mode == ButtonMode.DisabledInPlayMode);

                    // Draw a button which invokes the method
                    if (GUILayout.Button(ObjectNames.NicifyVariableName(method.Name)))
                    {
                        foreach (var t in targets)
                        {
                            method.Invoke(t, null);
                        }
                    }

                    GUI.enabled = true;
                }
            }

            // Draw the rest of the inspector as usual
            DrawDefaultInspector();
        }
    }
}
