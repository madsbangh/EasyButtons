namespace EasyButtons.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    internal class Button
    {
        private readonly string _name;
        private readonly ButtonSpacing _spacing;
        private readonly bool _enabled;
        private readonly bool _hasParams;
        private readonly MethodInfo _method;
        private readonly ParamInfo[] _parameters;

        private bool _expanded;

        public Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            _name = string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(method.Name)
                : buttonAttribute.Name;

            _spacing = buttonAttribute.Spacing;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

            _enabled = buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;

            var parameters = method.GetParameters();
            _hasParams = parameters.Length != 0;
            _parameters = parameters.Select(parameter => new ParamInfo(parameter)).ToArray();
            _method = method;
        }

        public void Draw(IEnumerable<Object> targets)
        {
            DrawWithEnabledGUI(_enabled, () =>
            {
                DrawWithSpacing(_spacing.HasFlag(ButtonSpacing.Before), _spacing.HasFlag(ButtonSpacing.After), () =>
                {
                    if (_hasParams)
                    {
                        (Rect foldoutRect, Rect buttonRect) = GetFoldoutAndButtonRects(_name);

                        _expanded = DrawInFoldout(foldoutRect, _expanded, _name, () =>
                        {
                            foreach (ParamInfo param in _parameters)
                            {
                                param.Draw();
                            }
                        });

                        if (GUI.Button(buttonRect, "Invoke"))
                            Invoke(targets);
                    }
                    else
                    {
                        if (GUILayout.Button(_name))
                            Invoke(targets);
                    }
                });
            });
        }

        private static bool DrawInFoldout(Rect foldoutRect, bool expanded, string header, Action drawStuff)
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

        private static (Rect, Rect) GetFoldoutAndButtonRects(string header)
        {
            const float buttonWidth = 60f;

            var foldoutWithoutButton = GUILayoutUtility.GetRect(new GUIContent(header), EditorStyles.foldoutHeader); // TODO: replace new GUIContent

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

        private void Invoke(IEnumerable<Object> objects)
        {
            var paramValues = _hasParams ? _parameters.Select(param => param.Value).ToArray() : null;

            foreach (Object obj in objects)
            {
                _method.Invoke(obj, paramValues);
            }
        }

        private static void DrawWithSpacing(bool before, bool after, Action drawStuff)
        {
            const float spacingHeight = 10f;

            if (before)
                GUILayout.Space(spacingHeight);

            drawStuff();

            if (after)
                GUILayout.Space(spacingHeight);
        }

        private static void DrawWithEnabledGUI(bool enabled, Action drawStuff)
        {
            bool previousValue = GUI.enabled;
            GUI.enabled = enabled;
            drawStuff();
            GUI.enabled = previousValue;
        }

        private readonly struct ParamInfo
        {
            private readonly FieldInfo _fieldInfo;
            private readonly ScriptableObject _scriptableObj;
            private readonly Editor _editor;

            public ParamInfo(ParameterInfo paramInfo)
            {
                Type generatedType = ScriptableObjectCache.GetClass(paramInfo.Name, paramInfo.ParameterType);
                _scriptableObj = ScriptableObject.CreateInstance(generatedType);
                _fieldInfo = generatedType.GetField(paramInfo.Name);
                _editor = Editor.CreateEditor(_scriptableObj, typeof(NoScriptFieldEditor));
            }

            public object Value => _fieldInfo.GetValue(_scriptableObj);

            public void Draw()
            {
                _editor.OnInspectorGUI();
            }
        }
    }
}