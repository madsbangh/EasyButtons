namespace EasyButtons.Editor
{
    using System.Collections.Generic;
    using System.Reflection;
    using Buttons;
    using UnityEditor;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Custom inspector for <see cref="UnityEngine.Object"/> including derived classes.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class ObjectEditor : Editor
    {
        private readonly List<Button> _buttons = new List<Button>();

        protected virtual void OnEnable()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;

                _buttons.Add(Button.Create(method, buttonAttribute));
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawEasyButtons();
        }

        /// <summary>
        /// Draws all the methods marked with <see cref="ButtonAttribute"/>.
        /// </summary>
        protected void DrawEasyButtons()
        {
            foreach (Button button in _buttons)
            {
                button.Draw(targets);
            }
        }
    }
}
