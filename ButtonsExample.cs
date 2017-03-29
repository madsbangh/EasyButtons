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
	
	// Example use of the ButtonAttribute that is not shown on runtime
	[EasyButtons.Button(EasyButtons.ShowMode.NotShowOnRuntime)]
    public void SayHelloEditor()
    {
        Debug.Log("Hello only On Editor");
    }
	
	// Example use of the ButtonAttribute that is only shown on runtime
	[EasyButtons.Button(EasyButtons.ShowMode.OnlyShowOnRuntime)]
    public void SayHelloInRuntime()
    {
        Debug.Log("Hello only in runtime");
    }
	
}
