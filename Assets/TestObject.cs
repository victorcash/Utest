using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [ContextMenu(nameof(Save))]
    public void Save()
    {
        Services.GamePlayElement.SaveMapData();
    }

    [ContextMenu(nameof(Load))]
    public void Load()
    {
        Services.GamePlayElement.LoadMapData();
    }
}
