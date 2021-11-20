using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IKillableEditUi : ElementInterfaceUi
{
    public Slider hpSlider;
    public Slider hpMaxSlider;
    public TMP_Text hpText;
    public TMP_Text hpMaxText;
    private IKillable target => (IKillable)target;

    protected override void Init()
    {
        base.Init();
        hpSlider.onValueChanged.AddListener(SetHp);
        hpSlider.onValueChanged.AddListener(SetHpMax);
    }

    private void SetHpMax(float val)
    {
        target.SetHpMax(val);
    }
    private void SetHp(float val)
    {
        target.SetHp(val);
    }
}

