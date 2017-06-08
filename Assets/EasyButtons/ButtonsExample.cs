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
    }
}
