using System;
using System.Linq;
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

            // Loop through all methods with the Button attribute and no arguments
            foreach (var method in target.GetType().GetMethods()
                .Where(m => m.GetParameters().Length == 0))
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
                        foreach (var target in targets)
                        {
                            method.Invoke(target, null);
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
