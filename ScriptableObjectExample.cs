using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "Example.asset", menuName = "New Example ScriptableObject")]
public class ScriptableObjectExample : ScriptableObject
{
    [EasyButtons.Button]
    public void SayHello()
    {
        Debug.Log("Hello");
    }
}
