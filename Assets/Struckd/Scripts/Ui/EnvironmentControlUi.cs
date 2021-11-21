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
    public Slider snowSlider;
    public RectTransform cityContent;
    public TMP_Text jsonDisplay;
    public TMP_Text timeText;
    public TMP_Text snowText;
    public TMP_Text rainText;
    public TMP_Text fogText;
    public TMP_Text thunderText;
    public Button closeBtn;

    private EnvironmentController controller;
    public void ToggleVisibility(bool val)
    {
        content.gameObject.SetActive(val);
    }
    public void Init()
    {
        controller = Services.EnvironmentController;
        rainSlider.onValueChanged.AddListener(OnSetRainIntensity);
        fogSlider.onValueChanged.AddListener(OnSetFogIntensity);
        thunderSlider.onValueChanged.AddListener(OnSetThunderIntensity);
        snowSlider.onValueChanged.AddListener(OnSetSnowIntensity);
        timeSlider.onValueChanged.AddListener(OnTimeSlider);
        closeBtn.onClick.AddListener(()=> { Services.Ui.ToggleWeatherPanel(false); });
        ToggleVisibility(false);
        CreateCityCards();
    }
    private void OnSetRainIntensity(float val)
    {
        controller.SetRainIntensity(val);
        rainText.text = val.ToString();
    }
    private void OnSetFogIntensity(float val)
    {
        controller.SetFogIntensity(val);
        fogText.text = val.ToString();
    }
    private void OnSetThunderIntensity(float val)
    {
        controller.SetThunderIntensity(val);
        thunderText.text = val.ToString();
    }
    private void OnSetSnowIntensity(float val)
    {
        controller.SetSnowIntensity(val);
        snowText.text = val.ToString();
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
        timeSlider.SetValueWithoutNotify(controller.time);

        rainSlider.SetValueWithoutNotify(controller.rain);
        fogSlider.SetValueWithoutNotify(controller.fog);
        thunderSlider.SetValueWithoutNotify(controller.thunder);
        snowSlider.SetValueWithoutNotify(controller.snow);
        rainText.text = controller.rain.ToString();
        snowText.text = controller.snow.ToString();
        thunderText.text = controller.thunder.ToString();
        fogText.text = controller.fog.ToString();
    }
}
