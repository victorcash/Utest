using System;
using UnityEngine.UI;

public class IFactionEditUi : ElementInterfaceUi
{
    public Button button;
    public TMPro.TMP_Text factionTxt;
    public IFaction iFaction => (IFaction)target;

    private void Awake()
    {
        button.onClick.AddListener(ToggleFaction);
    }

    protected override void Init()
    {
        base.Init();
        factionTxt.text = iFaction.GetFaction().ToString();
    }

    private void ToggleFaction()
    {
        var current = iFaction.GetFaction();
        var type = current.GetType();
        var count = Enum.GetValues(type).Length;
        if ((int)current + 1 >= count)
        {
            current = 0;
        }
        else
        {
            current++;
        }
        iFaction.SetFaction(current);
        factionTxt.text = current.ToString();
    }
}
