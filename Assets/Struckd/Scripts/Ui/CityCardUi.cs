using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityCardUi : MonoBehaviour
{
    private string cityName;
    public Button button;
    public TMP_Text cityDisplay;
    private EnvironmentControlUi control;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetAsCurrentCity);
    }
    public void SetupCard(string cityName, EnvironmentControlUi control)
    {
        this.control = control;
        this.cityName = cityName;
        cityDisplay.text = cityName;
    }
    private void SetAsCurrentCity()
    {
        control.SetCurrentCity(cityName);
    }
}
