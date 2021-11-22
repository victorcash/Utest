using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnvironmentControlUi : MonoBehaviour
{
    public RectTransform content;
    public Slider timeSlider;
    public Slider rainSlider;
    public Slider fogSlider;
    public Slider thunderSlider;
    public Slider snowSlider;
    public RectTransform cityContent;
    public TMP_Text infoDisplay;
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
        Services.Ui.FloatingNotification("Please wait...", 2f);
        Services.EnvironmentController.GetWeatherData(cityName, OnGetWeathData);
    }

    private void OnGetWeathData(WeatherData wd)
    {
        var cityName = wd.data.getCityByName.name;
        var weather = wd.data.getCityByName.weather.summary.title;
        var timestamp = wd.data.getCityByName.weather.timestamp;
        var jsonFormatted = wd.jsonFormatted;

        DateTime lastUpdateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        lastUpdateTime = lastUpdateTime.AddSeconds(timestamp).ToLocalTime();

        var result = $"City: {cityName}\n Weather: {weather}\n LastUpdateTime: {lastUpdateTime}";
        infoDisplay.text = result;
        jsonDisplay.text = $"JsonData: \n{jsonFormatted}";

        UpdateEnviromentBasedOnData(wd);
    }

    private void UpdateEnviromentBasedOnData(WeatherData wd)
    {
        var weather = wd.data.getCityByName.weather.summary.title;

        switch (weather)
        {
            case ("Thunderstorm"):
                {
                    controller.SetThunderIntensity(1f);
                    controller.SetRainIntensity(1f);
                    controller.SetFogIntensity(0f);
                    controller.SetSnowIntensity(0f);
                }
                break;
            case ("Drizzle"):
            case ("Mist"):
            case ("Smoke"):
            case ("Dust"):
            case ("Fog"):
            case ("Sand"):
            case ("Ash"):
            case ("Tornado"):
                {
                    controller.SetThunderIntensity(0f);
                    controller.SetRainIntensity(0f);
                    controller.SetFogIntensity(1f);
                    controller.SetSnowIntensity(0f);
                }
                break;
            case ("Rain"):
                {
                    controller.SetThunderIntensity(0f);
                    controller.SetRainIntensity(0.5f);
                    controller.SetFogIntensity(0f);
                    controller.SetSnowIntensity(0f);
                }
                break;
            case ("Snow"):
                {
                    controller.SetThunderIntensity(0f);
                    controller.SetRainIntensity(0f);
                    controller.SetFogIntensity(0f);
                    controller.SetSnowIntensity(1f);
                }
                break;
            case ("Clear"):
            case ("Squall"):
            case ("Clouds"):
                {
                    controller.SetThunderIntensity(0f);
                    controller.SetRainIntensity(0f);
                    controller.SetFogIntensity(0f);
                    controller.SetSnowIntensity(0f);
                }
                break;
        }
        UpdateUIByData();
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

    public void UpdateUIByData()
    {
        timeSlider.SetValueWithoutNotify(controller.time/Services.Config.SecondsInDay);

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
