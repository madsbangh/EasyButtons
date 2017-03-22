using UnityEngine;

[CreateAssetMenu(fileName = "Example.asset", menuName = "New Example ScriptableObject")]
public class ScriptableObjectExample : ScriptableObject
{
    [EasyButtons.Button]
    public void SayHello()
    {
        Debug.Log("Hello");
    }
}
