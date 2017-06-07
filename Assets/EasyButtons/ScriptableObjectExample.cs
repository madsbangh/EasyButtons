using UnityEngine;

namespace EasyButtons
{
    [CreateAssetMenu(fileName = "Example.asset", menuName = "New Example ScriptableObject")]
    public class ScriptableObjectExample : ScriptableObject
    {
        [Button]
        public void SayHello()
        {
            Debug.Log("Hello");
        }

        [Button(ButtonMode.DisabledInPlayMode)]
        public void SayHelloEditor()
        {
            Debug.Log("Hello from edit mode");
        }

        [Button(ButtonMode.EnabledInPlayMode)]
        public void SayHelloPlayMode()
        {
            Debug.Log("Hello from play mode");
        }
    }
}
