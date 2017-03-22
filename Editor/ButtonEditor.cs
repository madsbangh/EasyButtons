using System.Linq;
using UnityEngine;
using UnityEditor;

namespace EasyButtons
{
    /// <summary>
    /// Base class for making EasyButtons work
    /// </summary>
    public abstract class ButtonEditorBase : Editor
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
                    method.Invoke(target, null);
                }
            }
            // Draw the rest of the inspector as usual
            DrawDefaultInspector();
        }
    }

    /// <summary>
    /// Custom inspector for MonoBehaviour including derived classes.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : ButtonEditorBase { }

    /// <summary>
    /// Custom inspector for ScriptableObject including derived classes.
    /// </summary>
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectEditor : ButtonEditorBase { }
}
