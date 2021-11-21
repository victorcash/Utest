using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ElementCardUi : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public int testElementId = 2;
    public Image elementIcon;
    public TMP_Text elementName;
    public int elementId;
    private ScrollRect scrollRect;
    private void Awake()
    {
        scrollRect = GetComponentInParent<ScrollRect>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Services.GameElementPlacer.QueueElement(elementId);
        EventSystem.current.SetSelectedGameObject(scrollRect.gameObject);
        scrollRect.OnBeginDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData) 
    {
        eventData.pointerDrag = scrollRect.gameObject;
        EventSystem.current.SetSelectedGameObject(scrollRect.gameObject);
        scrollRect.OnDrag(eventData);
    }
    public void SetUpCard(GamePlayElement element)
    {
        elementIcon.sprite = element.elementIcon;
        elementName.text = element.elementName;
        elementId = element.elementID;
    }
}