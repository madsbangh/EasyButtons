namespace EasyButtons
{
    using UnityEditor;

    public class NoScriptFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");
        }
    }
}