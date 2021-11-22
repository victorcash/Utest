using System;
using UnityEngine;

public class EditCamera : MonoBehaviour
{
    private void Start()
    {
        Services.GameStates.AddOnGameModeChangedListener(OnGameModeChanged);
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        if (gameMode == GameMode.Edit)
        {
            //layze reset, should done it proper
            transform.position = new Vector3(0f, 12f, 0f);
        }
    }
}
