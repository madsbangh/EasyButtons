namespace EasyButtons.Editor
{
    using System.Reflection;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ButtonWithoutParamsAsync : ButtonWithoutParams
    {
        public ButtonWithoutParamsAsync(MethodInfo method, ButtonAttribute buttonAttribute)
            : base(method, buttonAttribute) { }

        protected async override void DrawInternal(IEnumerable<object> targets)
        {
            if ( ! GUILayout.Button(DisplayName))
                return;

            foreach (object obj in targets)
            {
                Task task = (Task)Method.Invoke(obj, null);
                await task;
            }
        }
    }
}