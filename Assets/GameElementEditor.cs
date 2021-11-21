using UnityEngine;
using UnityEngine.InputSystem;

public class GameElementEditor : MonoBehaviour
{
    public GameElementBehaviour currentElement;
    private Camera editCamT => Services.SceneReferences.editCamera;
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
                var elemet = hit.collider.gameObject.GetComponent<GameElementBehaviour>();
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
        if (currentElement == null) Services.Ui.elementEditPanel.ToggleVisibility(false);
    }

    private void OnElementDeselected()
    {
        currentElement = null;
        Services.Ui.ToggleElementList(true);
        Services.Ui.ToggleElementEditPanel(false);
    }

    private void OnElementSelected(GameElementBehaviour elemet)
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
