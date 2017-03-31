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

    [Button(ShowMode.NotShowOnRuntime)]
    public void SayHelloEditor()
    {
        Debug.Log("Hello only On Editor");
    }

    [Button(ShowMode.OnlyShowOnRuntime)]
    public void SayHelloInRuntime()
    {
        Debug.Log("Hello only in runtime");
    }
}
