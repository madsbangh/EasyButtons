using System.Linq;
using UnityEngine;
using UnityEditor;

namespace EasyButtons
{
    /// <summary>
    /// Custom inspector for MonoBehaviour including derived classes.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Loop through all methods with the Button attribute and no arguments
            foreach (var method in target.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0)
                .Where(m => m.GetParameters().Length == 0))
            {
                // Draw a button which invokes the method
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, new object[0]);
                }
            }
            // Draw the rest of the inspector as usual
            DrawDefaultInspector();
        }
    }
}
