using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestUiObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public int testElementId = 2;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Services.ElementPlacer.PlaceElement(2);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private void Update()
    {
        var count = ExtensionUI.PointerOverUIObjectsCount();
    }

    //How many ui it's currently casting?






}
