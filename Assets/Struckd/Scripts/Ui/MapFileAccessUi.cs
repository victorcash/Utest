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
    }

    private void ClearMapBtn()
    {
        Services.GamePlayElement.RemoveAllElements();
    }

    private void SaveMapToTemp()
    {
        Services.GamePlayElement.SaveMapData();
    }

    private void LoadMapFromTemp()
    {
        Services.GamePlayElement.LoadMapData();
    }
}
