using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IKillableEditUi : ElementInterfaceUi
{
    public Slider hpSlider;
    public Slider hpMaxSlider;
    public TMP_Text hpText;
    public TMP_Text hpMaxText;
    private IKillable killable => (IKillable)target;

    protected override void Init()
    {
        base.Init();
        hpSlider.onValueChanged.AddListener(SetHp);
        hpMaxSlider.onValueChanged.AddListener(SetHpMax);

        hpSlider.SetValueWithoutNotify(killable.GetHp() / Services.Config.HpRange);
        hpMaxSlider.SetValueWithoutNotify(killable.GetHpMax() / Services.Config.HpRange);
        hpText.text = Mathf.CeilToInt(killable.GetHp()).ToString();
        hpMaxText.text = Mathf.CeilToInt(killable.GetHpMax()).ToString();
    }

    private void SetHpMax(float val)
    {
        var hpMax = val * Services.Config.HpRange;
        hpMaxText.text = Mathf.CeilToInt(hpMax).ToString();
        killable.SetHpMax(hpMax);
    }
    private void SetHp(float val)
    {
        var hp = val * Services.Config.HpRange;
        hpText.text = Mathf.CeilToInt(hp).ToString();
        killable.SetHp(hp);
    }
}

