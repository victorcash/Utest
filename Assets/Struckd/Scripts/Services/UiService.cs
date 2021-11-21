
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
    [NonSerialized] public ElementListUi elementListUi;
    [NonSerialized] public Canvas editCanvas;
    [NonSerialized] public ElementEditPanel elementEditPanel;
    [NonSerialized] public EnvironmentControlUi environmentControlUi;

    public void Init(Canvas editCanvas)
    {
        this.editCanvas = editCanvas;
        elementListUi = Instantiate(elementListUiPrefab, editCanvas.transform);
        elementEditPanel = Instantiate(elementEditPanelPrefab, editCanvas.transform);
        environmentControlUi = Instantiate(environmentControlUiPrefab, editCanvas.transform);
        environmentControlUi.Init();
    }
    public void ToggleUIMode(GameMode gameMode)
    {

    }
    public void ToggleWeatherPanel(bool v)
    {
        environmentControlUi.ToggleVisibility(v);
    }

    public void GetUiPrefab(Type type)
    { 
    
    }

    public void ToggleElementList(bool val)
    {
        elementListUi?.ToggleVisibility(val);
    }
    public void ToggleElementEditPanel(bool val)
    {
        elementEditPanel?.ToggleVisibility(val);
    }

    private List<ElementInterfaceUi> GetElementInterfaceUiPrefabs(GamePlayElementBehaviour element)
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

    public void CreateElementInterfaceEditUi(GamePlayElementBehaviour element, RectTransform rt)
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

