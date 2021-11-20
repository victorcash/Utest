using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class UIVirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [Header("Output")]
    public UnityEvent<bool> buttonStateOutputEvent = new BoolEvent();
    public UnityEvent buttonClickOutputEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OutputButtonStateValue(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputButtonStateValue(false);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OutputButtonClickEvent();
    }

    void OutputButtonStateValue(bool buttonState)
    {
        buttonStateOutputEvent.Invoke(buttonState);
    }

    void OutputButtonClickEvent()
    {
        buttonClickOutputEvent.Invoke();
    }
    [Serializable] public sealed class BoolEvent : UnityEvent<bool> { }
}
