namespace EasyButtons.Editor
{
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using Object = UnityEngine.Object;

    internal abstract class Button
    {
        protected readonly string Name;
        protected readonly MethodInfo Method;

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

        public static Button Create(MethodInfo method, ButtonAttribute buttonAttribute)
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

        protected abstract void DrawInternal(IEnumerable<Object> targets);
    }
}