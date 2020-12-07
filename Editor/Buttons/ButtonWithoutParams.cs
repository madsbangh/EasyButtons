namespace EasyButtons.Editor.Buttons
{
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    internal class ButtonWithoutParams : Button
    {
        public ButtonWithoutParams(MethodInfo method, ButtonAttribute buttonAttribute)
            : base(method, buttonAttribute) { }

        protected override void DrawInternal(IEnumerable<Object> targets)
        {
            if ( ! GUILayout.Button(Name))
                return;

            foreach (Object obj in targets)
            {
                Method.Invoke(obj, null);
            }
        }
    }
}