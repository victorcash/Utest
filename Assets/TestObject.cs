using UnityEngine;

public class TestObject : MonoBehaviour
{
    [ContextMenu(nameof(Save))]
    public void Save()
    {
        Services.Element.SaveMapData();
    }

    [ContextMenu(nameof(Load))]
    public void Load()
    {
        Services.Element.LoadMapData();
    }

    [ContextMenu(nameof(Clear))]
    public void Clear()
    {
        Services.Element.RemoveAllElements();
    }

    [ContextMenu(nameof(DragElement))]
    public void DragElement()
    {
        Services.ElementPlacer.QueueElement(2);
    }

    [ContextMenu(nameof(MapDataHasActivePlayable))]
    public void MapDataHasActivePlayable()
    {
        Services.Element.MapDataHasActivePlayable();
    }
}


