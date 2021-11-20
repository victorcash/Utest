using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableBehaviour : KillableBehaviour, IPlayable
{
    public StarterAssetsInputs input;
    private void Awake()
    {
        input = GetComponentInChildren<StarterAssetsInputs>();
    }
    public void IsSetPlayable()
    {
        throw new System.NotImplementedException();
    }

    public void JoyPadA()
    {
        input.JumpInput(true);
    }

    public void JoyPadB()
    {
        input.SprintInput(true);
    }

    public void JoyStickLeft(Vector2 val)
    {
        input.MoveInput(val);
    }

    public void JoyStickRight(Vector2 val)
    {
        input.LookInput(val);
    }

    public void Possesse()
    {
        throw new System.NotImplementedException();
    }

    public void SetAsPlayable()
    {
        throw new System.NotImplementedException();
    }

    public void SetFree()
    {
        throw new System.NotImplementedException();
    }
}
