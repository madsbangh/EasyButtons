namespace EasyButtons
{
    using System;

    public enum ButtonMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }

    [Flags]
    public enum ButtonSpacing
    {
        None = 0,
        Before = 1,
        After = 2
    }

    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must have no arguments.
    /// </summary>
    /// <example><code>
    /// [Button]
    /// public void MyMethod()
    /// {
    ///     Debug.Log("Clicked!");
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        public readonly string Name;

        public ButtonMode Mode { get; set; } = ButtonMode.AlwaysEnabled;

        public ButtonSpacing Spacing { get; set; } = ButtonSpacing.None;

        public bool Expanded { get; set; }

        public ButtonAttribute()
        {
        }

        public ButtonAttribute(string name)
        {
            Name = name;
        }
    }
}
