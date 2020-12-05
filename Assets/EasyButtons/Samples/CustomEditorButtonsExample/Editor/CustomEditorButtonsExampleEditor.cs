using UnityEditor;

namespace EasyButtons
{
    [CustomEditor(typeof(CustomEditorButtonsExample))]
    public class CustomEditorButtonsExampleEditor : ObjectEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawEasyButtons();
        }
    }
}
