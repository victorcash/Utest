using UnityEngine;
using UnityEngine.InputSystem;

public class GameElementEditor : MonoBehaviour
{
    public GameElementBehaviour currentElement;
    private Camera editCamT => Services.SceneReferences.editCamera;
    private Vector2 screenPos => Input.mousePosition;

    public bool IsEditing => currentElement != null;
    float mouseDownTimeStamp;

    void Update()
    {
        if (Services.GameStates.GetGameMode() == GameMode.Play)
        {
            currentElement = null;
            return;
        }
        var ray = editCamT.ScreenPointToRay(screenPos);
        var hits = Physics.RaycastAll(ray, Mathf.Infinity, Services.Config.ElementLayer);

        if (Input.GetMouseButtonDown(0) && !ExtensionUI.IsPointerOverUIObject())
        {
            mouseDownTimeStamp = Time.realtimeSinceStartup;

            foreach (var hit in hits)
            {
                var elemet = hit.collider.gameObject.GetComponent<GameElementBehaviour>();
                if (elemet != null)
                {
                    OnElementSelected(elemet);
                    break;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            bool hasElement = false;

            foreach (var hit in hits)
            {
                var elemet = hit.collider.gameObject.GetComponent<GameElementBehaviour>();
                if (elemet != null)
                {
                    hasElement = true;
                    OnElementSelected(elemet);
                    break;
                }
            }

            if ((Time.realtimeSinceStartup - mouseDownTimeStamp) < Services.Config.durationCountAsHold && !hasElement)
            {
                OnElementDeselected();
            }
        }
        if (currentElement == null) Services.Ui.elementEditPanel.ToggleVisibility(false);
    }

    private void OnElementDeselected()
    {
        currentElement = null;
        Services.Ui.ToggleElementList(true);
        Services.Ui.ToggleElementEditPanel(false);
        Services.Ui.elementEditPanel.RemoveTarget();
    }

    private void OnElementSelected(GameElementBehaviour elemet)
    {
        if (currentElement == elemet) return;
        var contentRt = Services.Ui.elementEditPanel.content;
        Services.Ui.elementEditPanel.SetTarget(elemet);
        contentRt.DestroyAllChildren();
        currentElement = elemet;
        Services.Ui.ToggleElementList(false);
        Services.Ui.ToggleElementEditPanel(true);
        Services.Ui.CreateElementInterfaceEditUi(elemet, contentRt);
    }
}
