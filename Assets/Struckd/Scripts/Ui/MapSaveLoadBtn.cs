using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSaveLoadBtn : MonoBehaviour
{
    public int index;
    public bool isSaveBtn;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (isSaveBtn)
        {
            Services.GameElement.SaveElementsToMapData(index);
        }
        else
        {
            Services.GameElement.LoadElementsFromMapData(index);
        }
    }
}
