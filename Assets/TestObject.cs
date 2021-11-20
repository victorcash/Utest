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

    [ContextMenu(nameof(Clear))]
    public void Clear()
    {
        Services.GamePlayElement.RemoveAllElements();
    }
    [ContextMenu(nameof(CreateUI))]
    public void CreateUI()
    {
        TankBehaviour element = new TankBehaviour();

        GamePlayElementBehaviour bb = element;
        var interfaces = bb.GetType().GetInterfaces();

        foreach (var xx in interfaces)
        {
            UnityEngine.Debug.Log(xx);
        }
    }

    [ContextMenu(nameof(DragElement))]
    public void DragElement()
    {
        Services.ElementPlacer.PlaceElement(2);
    }
}


interface root
{ 

}


interface sub : root
{ 

}

//DO HP
//DO HP UI
//DO SOME COMBAT