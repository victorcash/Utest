using System;
using UnityEngine;

public class ElementEditPanel : MonoBehaviour
{
    private CanvasGroup cg;
    public RectTransform content;
    private GameElementBehaviour elemet;
    private RectTransform rt;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        rt = GetComponent<RectTransform>();
    }
    public void ToggleVisibility(bool val)
    {
        cg.alpha = val ? 1f : 0f;
        cg.blocksRaycasts = val;
        cg.interactable = val;
    }

    public void SetTarget(GameElementBehaviour elemet)
    {
        this.elemet = elemet;
    }

    public void RemoveTarget()
    {
        elemet = null;
    }

    private void Update()
    {
        if (elemet != null)
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Services.SceneReferences.editCamera, elemet.GetPos());
            var parentRt = transform.parent.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRt, screenPoint , null, out var uiPos);
            rt.anchoredPosition = uiPos;
        }
    }
}
