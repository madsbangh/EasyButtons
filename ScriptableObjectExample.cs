using UnityEngine;
using EasyButtons;

[CreateAssetMenu(fileName = "Example.asset", menuName = "New Example ScriptableObject")]
public class ScriptableObjectExample : ScriptableObject
{
    [Button]
    public void SayHello()
    {
        Debug.Log("Hello");
    }

    [Button(ShowMode.HideInPlayMode)]
    public void SayHelloEditor()
    {
        Debug.Log("Hello from edit mode");
    }

    [Button(ShowMode.PlayModeOnly)]
    public void SayHelloPlayMode()
    {
        Debug.Log("Hello from play mode");
    }
}
