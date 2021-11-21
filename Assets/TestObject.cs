using UnityEngine;

public class TestObject : MonoBehaviour
{
    [ContextMenu(nameof(Save))]
    public void Save()
    {
        Services.GameElement.SaveElementsToMapData();
    }

    [ContextMenu(nameof(Load))]
    public void Load()
    {
        Services.GameElement.LoadElementsFromMapData();
    }

    [ContextMenu(nameof(Clear))]
    public void Clear()
    {
        Services.GameElement.RemoveAllElements();
    }

    [ContextMenu(nameof(DragElement))]
    public void DragElement()
    {
        Services.GameElementPlacer.QueueElement(2);
    }

    [ContextMenu(nameof(MapDataHasActivePlayable))]
    public void MapDataHasActivePlayable()
    {
        Services.GameElement.MapDataHasActivePlayable();
    }
}


