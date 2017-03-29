using UnityEngine;

[CreateAssetMenu(fileName = "Example.asset", menuName = "New Example ScriptableObject")]
public class ScriptableObjectExample : ScriptableObject
{
    [EasyButtons.Button]
    public void SayHello()
    {
        Debug.Log("Hello");
    }
	
	[EasyButtons.Button(EasyButtons.ShowMode.NotShowOnRuntime)]
    public void SayHelloEditor()
    {
        Debug.Log("Hello only On Editor");
    }
	
	[EasyButtons.Button(EasyButtons.ShowMode.OnlyShowOnRuntime)]
    public void SayHelloInRuntime()
    {
        Debug.Log("Hello only in runtime");
    }
}
