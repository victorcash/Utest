using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EthanBehaviour : KillableBehaviour, IPlayable
{
    public ThirdPersonUserControl thirdPersonUserControl;
    public ThirdPersonCharacter thirdPersonCharacter;
    public Transform cameraFollowRoot;
    public GameObject elementCamera;
    bool isActivePlable;
    private void Awake()
    {
        OnGameModeChanged(GameMode.Edit);
    }
    public Transform CameraFollowRoot() => cameraFollowRoot;

    public bool IsActivePlayable() => isActivePlable;

    public void JoyPadA()
    {
    }

    public void JoyPadB()
    {
    }

    public void JoyStickLeft(Vector2 val)
    {
        thirdPersonUserControl.OnInput(val);
    }

    public void JoyStickRight(Vector2 val)
    {
    }

    public void Possesse()
    {
    }

    public void SetAsActivePlayable(bool val)
    {
        isActivePlable = val;
    }

    public void SetFree()
    {
    }
    public override string[] Serialize()
    {
        var entry = base.Serialize();
        entry.SetValue(IsActivePlayable(), CsvColumn.IsActivePlayable);
        return entry;
    }
    public override void Deserialize(string[] entry)
    {
        base.Deserialize(entry);
        if (bool.TryParse(entry.GetValue(CsvColumn.IsActivePlayable), out var result))
        {
            SetAsActivePlayable(result);
        }
    }
    protected override void OnGameModeChanged(GameMode gameMode)
    {
        base.OnGameModeChanged(gameMode);
        var body = GetComponentInChildren<Rigidbody>();

        if (gameMode == GameMode.Play && isActivePlable)
        {
            thirdPersonUserControl.enabled = true;
            thirdPersonCharacter.enabled = true;
            elementCamera.SetActive(true);
            body.isKinematic = false;
        }
        else
        {
            thirdPersonUserControl.enabled = false;
            thirdPersonCharacter.enabled = false;
            elementCamera.SetActive(false);
            body.isKinematic = true;
        }
    }
}
