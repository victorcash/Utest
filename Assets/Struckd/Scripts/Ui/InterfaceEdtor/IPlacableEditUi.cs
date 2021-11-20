using System;
using UnityEngine.UI;
using TMPro;

public class IPlacableEditUi : ElementInterfaceUi
{
    public Button button;
    public IPlacable iPlacable => (IPlacable)target;
    public Slider sliderHeight;
    public Slider sliderScale;
    public Slider sliderRotX;
    public Slider sliderRotY;

    public TMP_Text height;
    public TMP_Text scale;
    public TMP_Text rotX;
    public TMP_Text rotY;

    protected override void Init()
    {
        base.Init();
        sliderHeight.SetValueWithoutNotify(iPlacable.GetHeight()); 
        height.text = iPlacable.GetHeight().ToString();
        sliderScale.SetValueWithoutNotify(iPlacable.GetScale());
        scale.text = iPlacable.GetScale().ToString();
        sliderRotX.SetValueWithoutNotify(iPlacable.GetRotation().eulerAngles.x);
        rotX.text = iPlacable.GetRotation().eulerAngles.x.ToString();
        sliderRotY.SetValueWithoutNotify(iPlacable.GetRotation().eulerAngles.y);
        rotY.text = iPlacable.GetRotation().eulerAngles.y.ToString();

        button.onClick.AddListener(((IPlacable)target).Remove);
        sliderHeight.onValueChanged.AddListener(SetHeight);
        sliderScale.onValueChanged.AddListener(SetScale);
        sliderRotX.onValueChanged.AddListener(SetRotationX);
        sliderRotY.onValueChanged.AddListener(SetRotationY);
    }

    private void SetHeight(float v)
    {
        ((IPlacable)target).SetHeight(v);
        height.text = v.ToString();
    }
    private void SetScale(float v)
    {
        ((IPlacable)target).SetScale(v);
        scale.text = v.ToString();
    }
    private void SetRotationX(float v)
    {
        ((IPlacable)target).SetRotationX(v);
        rotX.text = v.ToString();
    }
    private void SetRotationY(float v)
    {
        ((IPlacable)target).SetRotationY(v);
        rotY.text = v.ToString();
    }
}
