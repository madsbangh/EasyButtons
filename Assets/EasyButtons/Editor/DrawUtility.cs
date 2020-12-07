namespace EasyButtons.Editor
{
    using System;
    using UnityEditor;
    using UnityEngine;

    internal static class DrawUtility
    {
        private static readonly GUIContent _tempContent = new GUIContent();

        public static void DrawWithSpacing(bool before, bool after, Action drawStuff)
        {
            const float spacingHeight = 10f;

            if (before)
                GUILayout.Space(spacingHeight);

            drawStuff();

            if (after)
                GUILayout.Space(spacingHeight);
        }

        public static void DrawWithEnabledGUI(bool enabled, Action drawStuff)
        {
            bool previousValue = GUI.enabled;
            GUI.enabled = enabled;
            drawStuff();
            GUI.enabled = previousValue;
        }

        public static bool DrawInFoldout(Rect foldoutRect, bool expanded, string header, Action drawStuff)
        {
            expanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, expanded, header);

            if (expanded)
            {
                EditorGUI.indentLevel++;
                drawStuff();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return expanded;
        }

        public static (Rect, Rect) GetFoldoutAndButtonRects(string header)
        {
            const float buttonWidth = 60f;

            Rect foldoutWithoutButton = GUILayoutUtility.GetRect(TempContent(header), EditorStyles.foldoutHeader);

            var foldoutRect = new Rect(
                foldoutWithoutButton.x,
                foldoutWithoutButton.y,
                foldoutWithoutButton.width - buttonWidth,
                foldoutWithoutButton.height);

            var buttonRect = new Rect(
                foldoutWithoutButton.xMax - buttonWidth,
                foldoutWithoutButton.y,
                buttonWidth,
                foldoutWithoutButton.height);

            return (foldoutRect, buttonRect);
        }

        private static GUIContent TempContent(string text)
        {
            _tempContent.text = text;
            return _tempContent;
        }
    }
}