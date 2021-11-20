using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapFileAccessUi : MonoBehaviour
{
    public Button saveMapToTempBtn;
    public Button loadMapFromTempBtn;
    public Button clearMapBtn;

    private void Awake()
    {
        saveMapToTempBtn.onClick.AddListener(SaveMapToTemp);
        loadMapFromTempBtn.onClick.AddListener(LoadMapFromTemp);
        clearMapBtn.onClick.AddListener(ClearMapBtn);
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
