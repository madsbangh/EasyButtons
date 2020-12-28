namespace EasyButtons.Editor
{
    using System.Collections.Generic;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Custom inspector for <see cref="UnityEngine.Object"/> including derived classes.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    // Hides a warning that the class has virtual members but no inheritors.
    // The class may be inherited by plugin users.
    [UsedImplicitly]
    public class ObjectEditor : Editor
    {
        /// <summary>
        /// A list of all the buttons available in the target class.
        /// </summary>
        [PublicAPI("The buttons can be accessed from a custom editor derived from ObjectEditor and be drawn individually")]
        protected readonly List<Button> Buttons = new List<Button>();

        protected virtual void OnEnable()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;

                Buttons.Add(Button.Create(method, buttonAttribute));
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
        [PublicAPI("The method can be used in custom editors derived from ObjectEditor")]
        protected void DrawEasyButtons()
        {
            foreach (Button button in Buttons)
            {
                button.Draw(targets);
            }
        }
    }
}
