using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPanelUi : MonoBehaviour
{
    public RectTransform content;
    public Button closeBtn;
    public void Init()
    {
        closeBtn.onClick.AddListener(()=> { Services.Ui.ToggleSaveLoadPanel(false); });
        ToggleVisibility(false);
    }
    public void ToggleVisibility(bool val)
    {
        content.gameObject.SetActive(val);
    }
}
