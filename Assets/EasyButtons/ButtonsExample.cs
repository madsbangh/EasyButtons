using UnityEngine;

namespace EasyButtons
{
    public class ButtonsExample : MonoBehaviour
    {
        // Example use of the ButtonAttribute
        [Button]
        public void SayMyName()
        {
            Debug.Log(name);
        }

        // Example use of the ButtonAttribute that is not shown in play mode
        [Button(ButtonMode.DisabledInPlayMode)]
        protected void SayHelloEditor()
        {
            Debug.Log("Hello from edit mode");
        }

        // Example use of the ButtonAttribute that is only shown in play mode
        [Button(ButtonMode.EnabledInPlayMode)]
        private void SayHelloInRuntime()
        {
            Debug.Log("Hello from play mode");
        }

        // Example use of the ButtonAttribute with custom name
        [Button("Special Name")]
        private void TestButtonName()
        {
            Debug.Log("Hello from special name button");
        }

        // Example use of the ButtonAttribute with custom name and button mode
        [Button("Special Name Editor Only", ButtonMode.DisabledInPlayMode)]
        private void TestButtonNameEditorOnly()
        {
            Debug.Log("Hello from special name button for editor only");
        }

        // Example use of the ButtonAttribute with static method
        [Button]
        private static void TestStaticMethod()
        {
            Debug.Log("Hello from static method");
        }
    }
}
