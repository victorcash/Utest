using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnvironmentControlUi : MonoBehaviour
{
    public RectTransform content;
    public Slider timeSlider;
    public Slider rainSlider;
    public Slider fogSlider;
    public Slider thunderSlider;
    public Slider dustSlider;
    public Slider snowSlider;
    public RectTransform cityContent;
    public TMP_Text jsonDisplay;
    public TMP_Text timeText;
    public Button closeBtn;

    private EnvironmentController controller;
    public void ToggleVisibility(bool val)
    {
        content.gameObject.SetActive(val);
    }
    public void Init()
    {
        controller = Services.EnvironmentController;
        rainSlider.onValueChanged.AddListener(controller.SetRainIntensity);
        fogSlider.onValueChanged.AddListener(controller.SetFogIntensity);
        thunderSlider.onValueChanged.AddListener(controller.SetThunderIntensity);
        dustSlider.onValueChanged.AddListener(controller.SetDustIntensity);
        snowSlider.onValueChanged.AddListener(controller.SetSnowIntensity);
        timeSlider.onValueChanged.AddListener(OnTimeSlider);
        closeBtn.onClick.AddListener(()=> { Services.Ui.ToggleWeatherPanel(false); });
        ToggleVisibility(false);
        CreateCityCards();
    }

    private void OnTimeSlider(float val)
    {
        controller.SetTime(val);
        timeText.text = Services.EnvironmentController.SecondsToTimeString(val);
    }

    public void SetCurrentCity(string cityName)
    {
        Services.EnvironmentController.GetWeatherData(cityName, OnGetWeathData);
    }

    private void OnGetWeathData(string json)
    {
        jsonDisplay.text = json;
    }

    private void CreateCityCards()
    {
        var cities = Services.Config.cities;
        var cityCardPrefab = Services.Ui.cityCardPrefab;
        foreach (var city in cities)
        {
            var cityCard = Instantiate(cityCardPrefab, cityContent);
            cityCard.SetupCard(city, this);
        }
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
