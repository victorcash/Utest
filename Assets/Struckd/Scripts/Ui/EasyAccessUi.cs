using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EasyAccessUi : MonoBehaviour
{
    public Button OpenWeather;
    public Button OpenFile;
    public Button RemoveAll;
    public Button test;

    private void Awake()
    {
        OpenWeather.onClick.AddListener(OnOpenWeather);
        OpenFile.onClick.AddListener(OnOpenFile);
        RemoveAll.onClick.AddListener(ClearMapBtn);
        test.onClick.AddListener(Test);
    }

    private void Test()
    {
    }

    private void ClearMapBtn()
    {
        Services.GameElement.RemoveAllElements();
    }

    private void OnOpenWeather()
    {
        Services.Ui.ToggleWeatherPanel(true);
    }

    private void OnOpenFile()
    {
        Services.Ui.ToggleSaveLoadPanel(true);
    }
}
