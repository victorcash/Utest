using UnityEngine;
using UnityEngine.InputSystem;

public class GameElementPlacer : MonoBehaviour
{
    private GameElementBehaviour currentElement;
    private Camera editCamT => Services.SceneReferences.editCamera;
    private Vector2 screenPos => Input.mousePosition;

    private int? queueId;

    public bool IsPlacing()
    {
        return queueId != null || currentElement != null;
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
    Vector3 offset = Vector3.zero;
    void Update()
    {
        if (Services.GameStates.GetGameMode() == GameMode.Play)
        { 
            currentElement = null;
            queueId = null;
            return;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            currentElement = null;
            queueId = null;
            offset = Vector3.zero;
            if (!Services.GameElementEditor.IsEditing)
            {
                Services.Ui.ToggleElementList(true);
            }
        }
        if (Input.GetMouseButtonDown(0) && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            currentElement = null;
            var ray = editCamT.ScreenPointToRay(screenPos);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);
            foreach (var hit in hits)
            {
                var elemet = hit.collider.gameObject.GetComponent<GameElementBehaviour>();
                if (elemet != null)
                {
                    currentElement = elemet;
                    offset = PointerPostion(Vector3.up, Vector3.zero) - hit.collider.gameObject.transform.position;
                    break;
                }
            }
        }
        if (Input.GetMouseButton(0) &&currentElement != null)
        {
            var targetPos = PointerPostion(Vector3.up, Vector3.zero) - offset;
            currentElement.SetPos(targetPos);
        }
        if (queueId != null && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            currentElement = Services.GameElement.CreateGamePlayElement((int)queueId);
            queueId = null;
            Services.Ui.ToggleElementList(false);
        }
    }
}
