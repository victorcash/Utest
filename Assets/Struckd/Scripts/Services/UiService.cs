﻿
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Struckd/UiService")]
public class UiService : ScriptableObject
{
    public InterfaceUiDictionary interfaceEditUiLookup = new InterfaceUiDictionary();
    public ElementCardUi elementCardPrefab;
    public ElementListUi elementListUiPrefab;
    public ElementEditPanel elementEditPanelPrefab;
    public EnvironmentControlUi environmentControlUiPrefab;
    public CityCardUi cityCardPrefab;
    public NotificationTxtUi notificationTxtUiPrefab;
    public SaveLoadPanelUi saveLoadPanelUiPrefab;

    [NonSerialized] public ElementListUi elementListUi;
    [NonSerialized] public ElementEditPanel elementEditPanel;
    [NonSerialized] public EnvironmentControlUi environmentControlUi;
    [NonSerialized] public SaveLoadPanelUi saveLoadPanelUi;
    public Canvas editCanvas => Services.SceneReferences.editCanvas;
    private Canvas appCanvas => Services.SceneReferences.appCanvas;
    private Canvas playCanvas => Services.SceneReferences.playCanvas;

    public void Init()
    {
        Services.GameStates.AddOnGameModeChangedListener(OnGameModeChanged);
        elementListUi = Instantiate(elementListUiPrefab, editCanvas.transform);
        elementEditPanel = Instantiate(elementEditPanelPrefab, editCanvas.transform);
        saveLoadPanelUi = Instantiate(saveLoadPanelUiPrefab, editCanvas.transform);
        saveLoadPanelUi.Init();
        environmentControlUi = Instantiate(environmentControlUiPrefab, editCanvas.transform);
        environmentControlUi.Init();
    }

    public void FloatingNotification(string v, float duration)
    {
        var notif = Instantiate(notificationTxtUiPrefab, appCanvas.transform);
        notif.SetContent(v, duration);
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        ToggleUIMode(gameMode);
    }

    public void ToggleUIMode(GameMode gameMode)
    {
        if (gameMode == GameMode.Edit)
        {
            editCanvas.gameObject.SetActive(true);
            playCanvas.gameObject.SetActive(false);
        }
        else if (gameMode == GameMode.Play)
        {
            editCanvas.gameObject.SetActive(false);
            playCanvas.gameObject.SetActive(true);
        }
    }
    public void ToggleWeatherPanel(bool v)
    {
        environmentControlUi.ToggleVisibility(v);
    }
    public void ToggleSaveLoadPanel(bool v)
    {
        saveLoadPanelUi.ToggleVisibility(v);
    }

    public void ToggleElementList(bool val)
    {
        elementListUi?.ToggleVisibility(val);
    }
    public void ToggleElementEditPanel(bool val)
    {
        elementEditPanel?.ToggleVisibility(val);
    }

    private List<ElementInterfaceUi> GetElementInterfaceUiPrefabs(GameElementBehaviour element)
    {
        List<ElementInterfaceUi> result = new List<ElementInterfaceUi>();

        var interfaces = element.GetType().GetInterfaces();
        foreach (var thisInterface in interfaces)
        {
            var interfaceName = thisInterface.ToString();
            var hasUi = Services.Ui.interfaceEditUiLookup.TryGetValue(interfaceName, out var thisUiPrefab);
            if (hasUi)
            { 
                result.Add(thisUiPrefab);
            }
        }
        return result;
    }

    public void CreateElementInterfaceEditUi(GameElementBehaviour element, RectTransform rt)
    {
        var uiList = GetElementInterfaceUiPrefabs(element);
        foreach (var ui in uiList)
        {
            var interfaceUi = Instantiate(ui, rt);
            interfaceUi.SetTarget(element);
        }
    }

}

[Serializable]
public class InterfaceUiDictionary : SerializableDictionary<string, ElementInterfaceUi> { }

