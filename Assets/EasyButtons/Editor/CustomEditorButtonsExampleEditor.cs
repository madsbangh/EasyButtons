using UnityEditor;

namespace EasyButtons
{
    [CustomEditor(typeof(CustomEditorButtonsExample))]
    public class CustomEditorButtonsExampleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawEasyButtons();
            base.OnInspectorGUI();
        }
    }
}
