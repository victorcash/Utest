﻿using System.Collections;
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

    [ContextMenu(nameof(DragElement))]
    public void DragElement()
    {
        Services.ElementPlacer.QueueElement(2);
    }

    [ContextMenu(nameof(MapDataHasActivePlayable))]
    public void MapDataHasActivePlayable()
    {
        Services.GamePlayElement.MapDataHasActivePlayable();
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