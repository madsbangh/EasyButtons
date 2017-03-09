using System;

namespace EasyButtons
{
    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must be public and have no arguments.
    /// </summary>
    /// <example>
    /// [<see cref="ButtonAttribute"/>]
    /// public void MyMethod()
    /// {
    ///     Debug.Log("Clicked!");
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute { }
}