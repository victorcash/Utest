using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
public class ElementService
{
    public List<IKillable> IKillables = new List<IKillable>();
    public List<IPlayable> IPlayables = new List<IPlayable>();
    public List<IPlacable> IPlacables = new List<IPlacable>();
    private List<GameElementBehaviour> gamePlayElements = new List<GameElementBehaviour>();
    private GamePlayElementDatabase database => Services.Database;

    public GameElementBehaviour CreateGamePlayElement(int elementId)
    {
        var element = database.GetElement(elementId);
        var elementsRoot = Services.SceneReferences.elementRoot;
        var prefab = element.prefab;
        var elementBehaviour = UnityEngine.Object.Instantiate(prefab, elementsRoot);
        return elementBehaviour;
    }

    public GameElementBehaviour CreateGamePlayElement(string entryString)
    {
        var entry = entryString.Split(',');
        var elementID = int.Parse(entry[(int)CsvColumn.ElementId]);
        var elementBehaviour = CreateGamePlayElement(elementID);
        elementBehaviour.InjectData(entry);
        return elementBehaviour;
    }

    public void InitElement(GameElementBehaviour element, Action<GameMode> onGameModeChanged)
    {
        gamePlayElements.Add(element);
        Services.GameStates.AddOnGameModeChangedListener(onGameModeChanged);
        if (element is IKillable) IKillables.Add((IKillable)element);
        if (element is IPlayable) IPlayables.Add((IPlayable)element);
        if (element is IPlacable) IPlacables.Add((IPlacable)element);
    }

    public void CleanUpElement(GameElementBehaviour element, Action<GameMode> onGameModeChanged)
    {
        gamePlayElements.Remove(element);
        Services.GameStates.RemoveOnGameModeChangedListener(onGameModeChanged);
        if (element is IKillable) IKillables.Remove((IKillable)element);
        if (element is IPlayable) IPlayables.Remove((IPlayable)element);
        if (element is IPlacable) IPlacables.Remove((IPlacable)element);
    }

    public void ClearAllActiveIPlayable()
    {
        foreach (var p in IPlayables)
        {
            p.SetAsActivePlayable(false);
        }
    }

    public void RemoveAllElements()
    {
        foreach (var iPlacable in IPlacables)
        {
            iPlacable.Remove();
        }
    }

    public IPlayable GetActivePlayable()
    {
        foreach (var playable in IPlayables)
        {
            if (playable.IsActivePlayable()) return playable;
        }
        return null;
    }

    public void SaveElementsToMapData(int id)
    {
        var csv = new StringBuilder();

        var header = BuildHeader(new CsvColumn());
        
        csv.AppendLine(header);
        foreach (var element in gamePlayElements)
        {
            var newEntry = element.Serialize().ToSingleLine();
            csv.AppendLine(newEntry);
        }

        string filePath = Application.persistentDataPath + $"/Maps/{id}.csv";
        var file = new FileInfo(filePath);
        file.Directory.Create();
        StreamWriter writer = File.CreateText(filePath);
        writer.WriteLine(csv);
        writer.Close();
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    private string BuildHeader(Enum enumType)
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

    public void LoadElementsFromMapData(int id, string filePath = "")
    {
        Services.GameElement.RemoveAllElements();
        filePath = Application.persistentDataPath + $"/Maps/{id}.csv";
        if (!File.Exists(filePath)) return;
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

    public bool MapDataHasActivePlayable(string filePath = "")
    {
        filePath = Application.persistentDataPath + "/Maps/temp.csv";
        StreamReader reader = new StreamReader(filePath);
        var csv = reader.ReadToEnd();
        reader.Close();
        string[] entries = csv.Split(new char[] { '\n' });
        //remove header, remove last 2 lines
        entries = entries.RemoveAt(0);
        entries = entries.RemoveAt(entries.Length - 1);
        entries = entries.RemoveAt(entries.Length - 1);

        bool hasActive = false;
        foreach (var entry in entries)
        {
            var succes = bool.TryParse(entry.Split(',')[(int)CsvColumn.IsActivePlayable], out var entryResult);
            if (succes) hasActive |= entryResult;
        }
        Debug.Log("hasActive " + hasActive);
        return hasActive;
    }
}

