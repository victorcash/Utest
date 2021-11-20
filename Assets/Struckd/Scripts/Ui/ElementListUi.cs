using UnityEngine;
using UnityEngine.UI;

public class ElementListUi : MonoBehaviour
{
    private CanvasGroup cg;
    public RectTransform content;
    public ScrollRect sr;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        sr = GetComponent<ScrollRect>();
    }
    private void Start() => FillContent();
    public void FillContent()
    {
        var allElements = Services.Database.allGamePlayElements;
        var cardPrefab = Services.Ui.elementCardPrefab;
        foreach (var element in allElements)
        {
            var ui = Instantiate(cardPrefab, content);
            ui.SetUpCard(element);
        }
    }
    public void ToggleVisibility(bool val)
    {
        cg.alpha = val ? 1f : 0f;
        cg.blocksRaycasts = val;
        cg.interactable = val;
        sr.enabled = val;
    }
}
