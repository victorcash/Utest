using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEditPanel : MonoBehaviour
{
    private CanvasGroup cg;
    public RectTransform content;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }
    public void ToggleVisibility(bool val)
    {
        cg.alpha = val ? 1f : 0f;
        cg.blocksRaycasts = val;
        cg.interactable = val;
    }
}
