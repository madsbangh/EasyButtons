namespace EasyButtons.Example
{
    using System.Linq;
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

            // You can draw all buttons at once.
            DrawEasyButtons();

            // As well as a specific button in the wanted place.
            Buttons.First(button => button.Name == "Custom Editor Example").Draw(targets);
        }
    }
}
