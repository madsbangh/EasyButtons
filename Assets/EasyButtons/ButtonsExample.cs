using UnityEngine;
using EasyButtons;

public class ButtonsExample : MonoBehaviour
{
    // Example use of the ButtonAttribute
    [Button]
    public void TestButton1()
    {
        Debug.Log("Yes! Please do it again :D");
    }

    [Button]
    public void TestButton2()
    {
        Debug.Log("Ouch! Stop clicking me :(");
    }
}
