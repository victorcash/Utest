using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    private Action<GameMode> onGameModeChanged = (_)=> { };
    private GameMode currentGameMode;
    public void SetGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        onGameModeChanged(currentGameMode);
    }
    public GameMode GetGameMode() => currentGameMode;
    public void AddOnGameModeChangedListener(Action<GameMode> callback)
    {
        onGameModeChanged += callback;
    }
    public void RemoveOnGameModeChangedListener(Action<GameMode> callback)
    {
        onGameModeChanged -= callback;
    }
}
