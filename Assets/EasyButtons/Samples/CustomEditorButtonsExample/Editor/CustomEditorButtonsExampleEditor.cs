using UnityEditor;

namespace EasyButtons
{
    using Editor;

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
