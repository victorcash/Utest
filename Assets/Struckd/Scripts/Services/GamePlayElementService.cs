using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GamePlayElementService
{
    public List<IKillable> IKillables = new List<IKillable>();
    private List<GamePlayElementBehaviour> gamePlayElements = new List<GamePlayElementBehaviour>();
    private Action onPlay;
    private Action onEdit;

    public GamePlayElementBehaviour CreateGamePlayElement(string entryString)
    {
        var entry = entryString.Split(',');
        var elementID = int.Parse(entry[(int)CsvColumn.ElementId]);
        var element = Services.Database.GetElement(elementID);
        var elementsRoot = Services.Config.elementsRoot;
        var prefab = element.prefab;
        var elementBehaviour = UnityEngine.Object.Instantiate(prefab, elementsRoot);
        elementBehaviour.InjectData(entry);
        return elementBehaviour;
    }

    public void InitElement(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        gamePlayElements.Add(element);
        this.onPlay += onPlay;
        this.onEdit += onEdit;
        if (element is IKillable) IKillables.Add((IKillable)element);
    }

    public void CleanUpElement(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        gamePlayElements.Remove(element);
        this.onPlay -= onPlay;
        this.onEdit -= onEdit;
        if (element is IKillable) IKillables.Remove((IKillable)element);
    }

    public void RemoveAllElements()
    {
        foreach (var element in gamePlayElements)
        {
            element.Remove();
        }
    }

    public void SaveMapData()
    {
        var csv = new StringBuilder();

        var header = BuildHeader(new CsvColumn());
        
        csv.AppendLine(header);
        foreach (var element in gamePlayElements)
        {
            var newEntry = element.Serialize().ToSingleLine();
            csv.AppendLine(newEntry);
        }

#if UNITY_EDITOR
        string filePath = "Assets/Resources/" + "test.csv";
#else
        string filePath = Application.dataPath + "/" + csvPath + ".csv";
#endif
        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(csv);
        outStream.Close();
        UnityEditor.AssetDatabase.Refresh();
    }

    public string BuildHeader(Enum enumType)
    {
        var result = string.Empty;
        var headerEnum = Enum.GetValues(enumType.GetType());
        for (int i = 0; i < headerEnum.Length; i++)
        {
            if(i != 0) result += ",";
            result += headerEnum.GetValue(i).ToString();
        }
        return result;
    }

    public void LoadMapData()
    {
        Services.GamePlayElement.RemoveAllElements();
        var csv = Resources.Load("test") as TextAsset;
        string[] entries = csv.text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        //remove header and last 2 extra empty lines
        entries = entries.RemoveAt(0);
        entries = entries.RemoveAt(entries.Length -1);
        entries = entries.RemoveAt(entries.Length -1);

        foreach (var entry in entries)
        {
            CreateGamePlayElement(entry);
        }
    }
}

