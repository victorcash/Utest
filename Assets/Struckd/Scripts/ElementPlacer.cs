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
    private PlayerInput input => Services.PlayerInput;
    private int? queueId;
    private void Awake()
    {
        input.onActionTriggered += InputTriggered;
    }

    private void InputTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "LeftRelease")
        {
            currentElement = null;
            queueId = null;
            Services.Ui.ToggleElementList(true);
        }
    }

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
        if (currentElement != null)
        {
            currentElement.transform.position = PointerPostion(Vector3.up, Vector3.zero);
        }
        if (queueId != null && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            currentElement = Services.GamePlayElement.CreateGamePlayElement((int)queueId);
            queueId = null;
            //Services.Ui.ToggleElementList(false);
        }
    }
}
