using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayElementService
{
    public int currentElementIndex = 0;
    public List<IKillable> IKillables = new List<IKillable>();
    private Action onPlay;
    private Action onEdit;

    public void Init(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        element.SetElementInstanceId(currentElementIndex);
        currentElementIndex++;
        this.onPlay += onPlay;
        this.onEdit += onEdit;
        if (element is IKillable) IKillables.Add((IKillable)element);
    }

    public void CleanUp(GamePlayElementBehaviour element, Action onPlay, Action onEdit)
    {
        this.onPlay -= onPlay;
        this.onEdit -= onEdit;
        if (element is IKillable) IKillables.Remove((IKillable)element);
    }
}
