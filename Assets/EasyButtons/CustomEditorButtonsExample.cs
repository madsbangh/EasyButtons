using UnityEngine;

namespace EasyButtons
{
    public class CustomEditorButtonsExample : MonoBehaviour
    {
        [Button("Custom Editor Example")]
        private void SayHello()
        {
            Debug.Log("Hello from custom editor");
        }
    }
}
