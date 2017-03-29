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
                .Where(m => Attribute.IsDefined(m, typeof(ButtonAttribute), true))
                .Where(m => m.GetParameters().Length == 0))
            {

                ShowMode show = ((ButtonAttribute)method.GetCustomAttributes(typeof(ButtonAttribute), true)[0]).showMode;
                if (!(show == ShowMode.NotShowOnRuntime && Application.isPlaying) && !(show == ShowMode.OnlyShowOnRuntime && !Application.isPlaying))
                {
                    // Draw a button which invokes the method
                    if (GUILayout.Button(ObjectNames.NicifyVariableName(method.Name)))
                    {
                        foreach (var target in targets)
                        {
                            method.Invoke(target, null);
                        }
                    }
                }
            }
			
			   // Draw the rest of the inspector as usual
            DrawDefaultInspector();
        }
    }
}
