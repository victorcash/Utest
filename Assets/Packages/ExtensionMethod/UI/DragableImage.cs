using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableImage : Image, IDragHandler, IBeginDragHandler
{
    Vector3 _dragOffset;
    public void OnDrag(PointerEventData eventData) => this.SetUIPositionByMouseInput(_dragOffset);
    public void OnBeginDrag(PointerEventData eventData) => _dragOffset = this.ScreenPointInPixel() - Input.mousePosition;
}