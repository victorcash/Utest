using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [ContextMenu(nameof(Test))]
    public void Test()
    {
        Services.GamePlayElement.SaveMapData();
    }
}
