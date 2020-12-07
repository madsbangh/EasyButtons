namespace EasyButtons
{
    using Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(CustomEditorButtonsExample))]
    public class CustomEditorButtonsExampleEditor : ObjectEditor
    {
        // Make sure to override OnEnable instead of creating a new private one.
        protected override void OnEnable()
        {
            base.OnEnable();
            Debug.Log("Custom OnEnable called.");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Message from the custom editor.", MessageType.Info);
            DrawEasyButtons();
        }
    }
}
