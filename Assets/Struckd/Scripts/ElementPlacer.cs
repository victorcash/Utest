using UnityEngine;
using UnityEngine.InputSystem;

public class ElementPlacer : MonoBehaviour
{
    private GamePlayElementBehaviour currentElement;
    private GameMode gameMode => Services.GameStates.gameMode;
    private Camera editCamT => Services.Camera?.editCamera;
#if UNITY_EDITOR
    private Vector2 screenPos => Mouse.current.position.ReadValue();
#else
    private Vector2 screenPos => Touchscreen.current.primaryTouch.position.ReadValue();
#endif
    private int? queueId;

    private Vector3 PointerPostion(Vector3 planeNormal, Vector3 planePoint)
    {
        if (editCamT == null) return Vector3.zero;
        Math_3D.LinePlaneIntersection(
            out var pos,
            editCamT.transform.position,
            editCamT.ScreenPointToRay(screenPos).direction,
            planeNormal,
            planePoint);
        return pos;
    }
    public void QueueElement(int elementId)
    {
        queueId = elementId;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentElement = null;
            queueId = null;
            Services.Ui.ToggleElementList(true);
        }
        if (currentElement == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = editCamT.ScreenPointToRay(screenPos);
                var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);
                foreach (var hit in hits)
                {
                    var elemet = hit.collider.gameObject.GetComponent<GamePlayElementBehaviour>();
                    if (elemet != null)
                    {
                        currentElement = elemet;
                        break;
                    }
                }
            }
        }
        else
        {
            currentElement.transform.position = PointerPostion(Vector3.up, Vector3.zero);
        }
        if (queueId != null && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            currentElement = Services.GamePlayElement.CreateGamePlayElement((int)queueId);
            queueId = null;
            Services.Ui.ToggleElementList(false);
        }
    }
}
