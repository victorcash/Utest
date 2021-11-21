using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentControlUi : MonoBehaviour
{
    public Slider rainSlider;
    public Slider fogSlider;
    public Slider thunderSlider;
    public Slider dustSlider;
    public Slider timeSlider;
    private EnvironmentController controller;
    private void Awake()
    {
        controller = Services.EnvironmentController;
        rainSlider.onValueChanged.AddListener(controller.SetRainIntensity);
        fogSlider.onValueChanged.AddListener(controller.SetFogIntensity);
        thunderSlider.onValueChanged.AddListener(controller.SetThunderIntensity);
        dustSlider.onValueChanged.AddListener(controller.SetDustIntensity);
        timeSlider.onValueChanged.AddListener(controller.SetTime);
    }

    private void UpdateUI()
    {
        rainSlider.SetValueWithoutNotify(controller.rain);
        fogSlider.SetValueWithoutNotify(controller.fog);
        thunderSlider.SetValueWithoutNotify(controller.thunder);
        dustSlider.SetValueWithoutNotify(controller.dust);
        timeSlider.SetValueWithoutNotify(controller.time);
    }
}
