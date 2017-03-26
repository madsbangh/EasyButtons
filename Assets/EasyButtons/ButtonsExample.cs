using UnityEngine;
using EasyButtons;

public class ButtonsExample : MonoBehaviour
{
    // Example use of the ButtonAttribute
    [Button]
    public void SayMyName()
    {
        Debug.Log(name);
    }
}
