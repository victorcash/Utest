using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Struckd/GameStates")]
public class GameStates : ScriptableObject
{
    public Action<GameMode> onGameModeChanged = (_)=> { };
    public GameMode currentGameMode;



    public void SwitchGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        onGameModeChanged(currentGameMode);
    }













}
