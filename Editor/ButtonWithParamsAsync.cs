namespace EasyButtons.Editor
{
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Utils;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ButtonWithParamsAsync : ButtonWithParams
    {
        public ButtonWithParamsAsync(MethodInfo method, ButtonAttribute buttonAttribute, ParameterInfo[] parameters)
            : base(method, buttonAttribute, parameters) { }

        protected async override void DrawInternal(IEnumerable<object> targets)
        {
            (Rect foldoutRect, Rect buttonRect) = DrawUtility.GetFoldoutAndButtonRects(DisplayName);

            _expanded = DrawUtility.DrawInFoldout(foldoutRect, _expanded, DisplayName, () =>
            {
                foreach (Parameter param in _parameters)
                {
                    param.Draw();
                }
            });

            if ( ! GUI.Button(buttonRect, "Invoke"))
                return;

            var paramValues = _parameters.Select(param => param.Value).ToArray();

            foreach (object obj in targets)
            {
                Task task = (Task)Method.Invoke(obj, paramValues);
                await task;
            }
        }
    }
}