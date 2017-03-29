using System;

namespace EasyButtons
{
    public enum ShowMode
    {
        AlwaysShow,
        OnlyShowOnRuntime,
        NotShowOnRuntime
    }
    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must be public and have no arguments.
    /// </summary>
    /// <example>
    /// [Button]
    /// public void MyMethod()
    /// {
    ///     Debug.Log("Clicked!");
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        public ShowMode showMode;
        public ButtonAttribute(ShowMode showMode = ShowMode.AlwaysShow)
        {
            this.showMode = showMode;
        }
    }
}
