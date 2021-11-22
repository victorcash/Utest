using UnityEngine;
using UnityEngine.InputSystem;

public class GameElementPlacer : MonoBehaviour
{
    private GameElementBehaviour currentElement;
    private GameMode gameMode => Services.GameStates.GetGameMode();
    private Camera editCamT => Services.SceneReferences.editCamera;
#if UNITY_EDITOR
    private Vector2 screenPos => Mouse.current.position.ReadValue();
#else
    private Vector2 screenPos => Touchscreen.current.primaryTouch.position.ReadValue();
#endif
    private int? queueId;
    private float holdTime;
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
        if (queueId != null && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            currentElement = Services.GameElement.CreateGamePlayElement((int)queueId);
            queueId = null;
            Services.Ui.ToggleElementList(false);
        }

        if (Input.GetMouseButtonDown(0) && ExtensionUI.PointerOverUIObjectsCount() == 0)
        {
            var ray = editCamT.ScreenPointToRay(screenPos);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);
            if (currentElement == null)
            {
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
            else
            {
                var hasElement = false;
                foreach (var hit in hits)
                {
                    var elemet = hit.collider.gameObject.GetComponent<GameElementBehaviour>();
                    hasElement |= elemet != null;
                }
                if (!hasElement) currentElement = null;
            }
        }
        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            if (currentElement != null && holdTime > Services.Config.durationCountAsHold)
            {
                Debug.Log("SetPosition");
                var target = PointerPostion(Vector3.up, Vector3.zero) - offset;
                var current = currentElement.GetPos();
                var distance = Vector3.Distance(target, current);
                if (distance < 5f)
                {
                    currentElement.SetPos(PointerPostion(Vector3.up, Vector3.zero) - offset);
                }
                else
                {
                    currentElement = null;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)|| Input.GetMouseButtonUp(1))
        {
            holdTime = 0f;
            currentElement = null;
            queueId = null;
            offset = Vector3.zero;
            if (!Services.GameElementEditor.IsEditing)
            { 
                Services.Ui.ToggleElementList(true);
            }
        }
    }
}
