namespace EasyButtons.Editor
{
    using System.Reflection;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ButtonWithoutParamsAsync : ButtonWithoutParams
    {
        public ButtonWithoutParamsAsync(MethodInfo method, ButtonAttribute buttonAttribute)
            : base(method, buttonAttribute) { }

        protected async override void InvokeMethod(IEnumerable<object> targets)
        {
            foreach (object obj in targets) {
                Task task = (Task)Method.Invoke(obj, null);
                await task;
            }
        }
    }
}