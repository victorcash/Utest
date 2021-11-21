using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MapFileAccessUi : MonoBehaviour
{
    public Button saveMapToTempBtn;
    public Button loadMapFromTempBtn;
    public Button clearMapBtn;
    public Button test;

    private void Awake()
    {
        saveMapToTempBtn.onClick.AddListener(SaveMapToTemp);
        loadMapFromTempBtn.onClick.AddListener(LoadMapFromTemp);
        clearMapBtn.onClick.AddListener(ClearMapBtn);
        test.onClick.AddListener(Test);
    }

    private void Test()
    {
        Services.Ui.ToggleWeatherPanel(true);
    }

    private void ClearMapBtn()
    {
        Services.Element.RemoveAllElements();
    }

    private void SaveMapToTemp()
    {
        Services.Element.SaveMapData();
    }

    private void LoadMapFromTemp()
    {
        Services.Element.LoadMapData();
    }
}
