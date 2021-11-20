using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameElementEditor : MonoBehaviour
{
    public GamePlayElementBehaviour currentElement;
    private Camera editCamT => Services.Camera.editCamera.cameraComp;
#if UNITY_EDITOR
    private Vector2 screenPos => Mouse.current.position.ReadValue();
#else
    private Vector2 screenPos => Touchscreen.current.primaryTouch.position.ReadValue();
#endif
    public bool IsEditing => currentElement != null;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ExtensionUI.IsPointerOverUIObject())
        {
            var ray = editCamT.ScreenPointToRay(screenPos);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);
            bool hasElement = false;
            foreach (var hit in hits)
            {
                var elemet = hit.collider.gameObject.GetComponent<GamePlayElementBehaviour>();
                if (elemet != null)
                {
                    hasElement = true;
                    OnElementSelected(elemet);
                    break;
                }
            }
            if (!hasElement)
            {
                OnElementDeselected();
            }
        }
    }

    private void OnElementDeselected()
    {
        currentElement = null;
        Services.Ui.ToggleElementList(true);
        Services.Ui.ToggleElementEditPanel(false);
    }

    private void OnElementSelected(GamePlayElementBehaviour elemet)
    {
        if (currentElement == elemet) return;
        var contentRt = Services.Ui.elementEditPanel.content;
        contentRt.DestroyAllChildren();
        currentElement = elemet;
        Services.Ui.ToggleElementList(false);
        Services.Ui.ToggleElementEditPanel(true);
        Services.Ui.CreateElementInterfaceEditUi(elemet, contentRt);
    }
}
