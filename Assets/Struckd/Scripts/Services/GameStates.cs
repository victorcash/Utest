using System;
using UnityEngine;

public class GameStates
{
    public Action<GameMode> onGameModeChanged = (_)=> { };
    public GameMode currentGameMode;



    public void SwitchGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        onGameModeChanged(currentGameMode);
    }













}
