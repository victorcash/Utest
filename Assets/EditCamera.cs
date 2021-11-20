using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditCamera : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector2 screenPos => Mouse.current.position.ReadValue();
#else
    private Vector2 screenPos => Touchscreen.current.primaryTouch.position.ReadValue();
#endif
    public Camera camera => GetComponent<Camera>();
    private Vector3 PointerPostion()
    {
        if (camera == null) return Vector3.zero;
        Math_3D.LinePlaneIntersection(
            out var pos,
            camera.transform.position,
            camera.ScreenPointToRay(screenPos).direction,
            Vector3.up,
            Vector3.zero);
        return pos;
    }

    private Vector3 startPos;
    private Vector3 camStartPos;
    void Update()
    {
        if (!Services.ElementPlacer.IsPlacing())
        {

        }

        //if (Input.GetMouseButtonUp(0))
        //{
        //    currentElement = null;
        //    queueId = null;
        //    Services.Ui.ToggleElementList(true);
        //}
        //if (currentElement == null)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        var ray = editCamT.ScreenPointToRay(screenPos);
        //        var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);
        //        foreach (var hit in hits)
        //        {
        //            var elemet = hit.collider.gameObject.GetComponent<GamePlayElementBehaviour>();
        //            if (elemet != null)
        //            {
        //                currentElement = elemet;
        //                break;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    currentElement.transform.position = PointerPostion(Vector3.up, Vector3.zero);
        //}
        //if (queueId != null && ExtensionUI.PointerOverUIObjectsCount() == 0)
        //{
        //    currentElement = Services.GamePlayElement.CreateGamePlayElement((int)queueId);
        //    queueId = null;
        //    Services.Ui.ToggleElementList(false);
        //}
    }

}
