using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GamePlayElementService
{
    public int currentElementIndex = 0;
    public List<IKillable> IKillables = new List<IKillable>();
    private List<GamePlayElementBehaviour> gamePlayElements = new List<GamePlayElementBehaviour>();
    private Action onPlay;
    private Action onEdit;

    public void Init(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        element.SetElementInstanceId(currentElementIndex);
        gamePlayElements.Add(element);
        currentElementIndex++;
        this.onPlay += onPlay;
        this.onEdit += onEdit;
        if (element is IKillable) IKillables.Add((IKillable)element);
    }

    public void CleanUp(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        gamePlayElements.Remove(element);
        this.onPlay -= onPlay;
        this.onEdit -= onEdit;
        if (element is IKillable) IKillables.Remove((IKillable)element);
    }

    public void SaveMapData()
    {
        var csv = new StringBuilder();

        foreach (var element in gamePlayElements)
        {
            var newLine = element.Serialize().ToSingleLine();
            csv.AppendLine(newLine);
        }

#if UNITY_EDITOR
        string filePath = "Assets/" + "test.csv";
#else
        string filePath = Application.dataPath + "/" + csvPath + ".csv";
#endif
        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(csv);
        outStream.Close();
    }
}

