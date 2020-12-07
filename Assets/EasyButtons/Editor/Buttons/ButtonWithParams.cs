namespace EasyButtons.Editor.Buttons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Utils;
    using Object = UnityEngine.Object;

    internal class ButtonWithParams : Button
    {
        private readonly Parameter[] _parameters;
        private bool _expanded;

        public ButtonWithParams(MethodInfo method, ButtonAttribute buttonAttribute, ParameterInfo[] parameters)
            : base(method, buttonAttribute)
        {
            _parameters = parameters.Select(paramInfo => new Parameter(paramInfo)).ToArray();
        }

        protected override void DrawInternal(IEnumerable<Object> targets)
        {
            (Rect foldoutRect, Rect buttonRect) = DrawUtility.GetFoldoutAndButtonRects(Name);

            _expanded = DrawUtility.DrawInFoldout(foldoutRect, _expanded, Name, () =>
            {
                foreach (Parameter param in _parameters)
                {
                    param.Draw();
                }
            });

            if ( ! GUI.Button(buttonRect, "Invoke"))
                return;

            var paramValues = _parameters.Select(param => param.Value).ToArray();

            foreach (Object obj in targets)
            {
                Method.Invoke(obj, paramValues);
            }
        }

        private readonly struct Parameter
        {
            private readonly FieldInfo _fieldInfo;
            private readonly ScriptableObject _scriptableObj;
            private readonly Editor _editor;

            public Parameter(ParameterInfo paramInfo)
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