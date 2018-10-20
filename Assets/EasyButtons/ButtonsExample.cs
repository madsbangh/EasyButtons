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
        [Button("Special Name", ButtonSpacing.Before)]
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
        
        // Example use of the ButtonAttribute with ButtonSpacing, and mix two spacing together.
        [Button("Space Before and After", ButtonSpacing.Before | ButtonSpacing.After)]
        private void TestButtonSpaceBoth() {
            Debug.Log("Hello from a button surround by spaces");
        }
        
        // Placeholder to show the last button have space after it.
        [Button("Another Button")]
        private void TestButtonEndSpace() {
            Debug.Log("Hello I am here to show some spacing.");
        }
    }
}
