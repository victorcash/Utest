using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class GamePlayElementService
{
    public List<IKillable> IKillables = new List<IKillable>();
    private List<GamePlayElementBehaviour> gamePlayElements = new List<GamePlayElementBehaviour>();
    private Action onPlay;
    private Action onEdit;

    public GamePlayElementBehaviour CreateGamePlayElement(int elementId)
    {
        var element = Services.Database.GetElement(elementId);
        var elementsRoot = Services.Config.elementsRoot;
        var prefab = element.prefab;
        var elementBehaviour = UnityEngine.Object.Instantiate(prefab, elementsRoot);
        return elementBehaviour;
    }

    public GamePlayElementBehaviour CreateGamePlayElement(string entryString)
    {
        var entry = entryString.Split(',');
        var elementID = int.Parse(entry[(int)CsvColumn.ElementId]);
        var elementBehaviour = CreateGamePlayElement(elementID);
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

        string filePath = Application.persistentDataPath + "/Maps/temp.csv";
        var file = new FileInfo(filePath);
        file.Directory.Create();
        StreamWriter writer = File.CreateText(filePath);
        writer.WriteLine(csv);
        writer.Close();
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
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
        string filePath = Application.persistentDataPath + "/Maps/temp.csv";
        StreamReader reader = new StreamReader(filePath);
        var csv = reader.ReadToEnd();
        reader.Close();
        string[] entries = csv.Split(new char[] { '\n' });
        //remove header, remove last 2 lines
        entries = entries.RemoveAt(0);
        entries = entries.RemoveAt(entries.Length -1);
        entries = entries.RemoveAt(entries.Length -1);

        foreach (var entry in entries)
        {
            CreateGamePlayElement(entry);
        }
    }
}

