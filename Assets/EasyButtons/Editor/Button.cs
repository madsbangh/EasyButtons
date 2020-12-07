namespace EasyButtons.Editor
{
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using Utils;
    using Object = UnityEngine.Object;

    /// <summary>
    /// A class that holds information about a button and can draw it in the inspector.
    /// </summary>
    public abstract class Button
    {
        /// <summary> Display name of the button. </summary>
        public readonly string Name;

        /// <summary> MethodInfo object the button is attached to. </summary>
        public readonly MethodInfo Method;

        private readonly ButtonSpacing _spacing;
        private readonly bool _enabled;

        protected Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            Name = string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(method.Name)
                : buttonAttribute.Name;

            Method = method;

            _spacing = buttonAttribute.Spacing;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

            _enabled = buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode;
        }

        public void Draw(IEnumerable<Object> targets)
        {
            DrawUtility.DrawWithEnabledGUI(_enabled, () =>
            {
                DrawUtility.DrawWithSpacing(_spacing.HasFlag(ButtonSpacing.Before), _spacing.HasFlag(ButtonSpacing.After), () =>
                {
                    DrawInternal(targets);
                });
            });
        }

        internal static Button Create(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                return new ButtonWithoutParams(method, buttonAttribute);
            }
            else
            {
                return new ButtonWithParams(method, buttonAttribute, parameters);
            }
        }

        protected abstract void DrawInternal(IEnumerable<Object> targets);
    }
}