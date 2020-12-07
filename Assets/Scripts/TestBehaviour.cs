using EasyButtons;
using UnityEngine;

public class TestBehaviour : MonoBehaviour
{
    [Button]
    public void NoParams()
    {
        Debug.Log("No Params");
    }

    [Button]
    public void OneParam(string message)
    {
        Debug.Log(message);
    }

    [Button]
    public void TwoParams(string message, int num)
    {
        Debug.Log($"message: {message}, num: {num}");
    }

    [Button]
    public void NonSerializedParam(NonSerializedClass testClass)
    {

    }

    [Button]
    public void NonSerializedParamAndString(NonSerializedClass testClass, string test)
    {

    }
}

public class NonSerializedClass
{
    public string Field;
}