namespace EasyButtons.Editor
{
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ButtonWithParamsAsync : ButtonWithParams
    {
        public ButtonWithParamsAsync(MethodInfo method, ButtonAttribute buttonAttribute, ParameterInfo[] parameters)
            : base(method, buttonAttribute, parameters) { }

        protected async override void InvokeMethod(IEnumerable<object> targets) {
            var paramValues = _parameters.Select(param => param.Value).ToArray();

            foreach (object obj in targets) {
                Task task = (Task)Method.Invoke(obj, paramValues);
                await task;
            }
        }
    }
}