namespace EasyButtons.Editor
{
    using UnityEditor;

    internal class NoScriptFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");
        }

        public void ApplyModifiedProperties()
        {
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}