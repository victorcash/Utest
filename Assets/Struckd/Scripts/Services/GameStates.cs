using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    private Action<GameMode> onGameModeChanged = (_)=> { };
    private GameMode currentGameMode;
    public void SetGameMode(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.Edit:
                SetGameAsEditMode();
                break;
            case GameMode.Play:
                SetGameAsPlayMode();
                break;
            default:
                break;
        }
    }
    private void SetGameAsEditMode()
    {
        Services.Element.RemoveAllElements();
        Services.Element.LoadMapData();
        currentGameMode = GameMode.Edit;
        onGameModeChanged(currentGameMode);
    }
    private void SetGameAsPlayMode()
    {
        var iPlayable = Services.Element.GetActivePlayable();
        if (iPlayable != null)
        {
            currentGameMode = GameMode.Play;
            onGameModeChanged(currentGameMode);
        }
        else
        {
            Services.Ui.FloatingNotification("You need to set a playable character first!");
        }
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
