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
            // Draw the rest of the inspector as usual
            DrawDefaultInspector();

            this.DrawEasyButtons();
        }
    }
}
