using System;

namespace EasyButtons
{
    public enum ButtonMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }
    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must have no arguments.
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
        private ButtonMode mode = ButtonMode.AlwaysEnabled;

        public ButtonMode Mode { get { return mode; } }

        public ButtonAttribute(ButtonMode mode = ButtonMode.AlwaysEnabled)
        {
            this.mode = mode;
        }
    }
}
