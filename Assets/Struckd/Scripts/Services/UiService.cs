
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Struckd/UiService")]
public class UiService : ScriptableObject
{
    public InterfaceUiDictionary interfaceEditUiLookup = new InterfaceUiDictionary();
    public ElementCardUi elementCardPrefab;
    public ElementListUi elementListUiPrefab;
    [NonSerialized] public ElementListUi elementListUi;
    [NonSerialized] public Canvas editCanvas;


    public void Init(Canvas editCanvas)
    {
        this.editCanvas = editCanvas;
        elementListUi = Instantiate(elementListUiPrefab, editCanvas.transform);
    }

    public void ToggleUIMode(GameMode gameMode)
    {

    }


    public void GetUiPrefab(Type type)
    { 
    
    }

    public void ToggleElementList(bool val)
    {
        elementListUi?.ToggleVisibility(val);
    }


    public List<ElementInterfaceUi> GetElementEditUiPrefabs(GamePlayElementBehaviour element)
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

    public void CreateElementEditUi(GamePlayElementBehaviour element, RectTransform rt)
    {
        var uiList = GetElementEditUiPrefabs(element);
        foreach (var ui in uiList)
        {
            var interfaceUi = Instantiate(ui, rt);
            interfaceUi.SetTarget(element);
        }
    }

}

[Serializable]
public class InterfaceUiDictionary : SerializableDictionary<string, ElementInterfaceUi> { }

