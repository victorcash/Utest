using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestUiObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public int testElementId = 2;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Services.GameElementPlacer.QueueElement(1);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private void Update()
    {
    }
}
