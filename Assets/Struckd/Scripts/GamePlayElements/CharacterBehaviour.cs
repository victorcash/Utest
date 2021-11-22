using StarterAssets;
using UnityEngine;

public class CharacterBehaviour : KillableBehaviour, IPlayable
{
    private bool isActivePlayable;
    public StarterAssetsInputs input;
    public Transform cameraFollowRoot;
    private void Awake()
    {
        input = GetComponentInChildren<StarterAssetsInputs>();
    }
    protected override void OnGameModeChanged(GameMode gameMode)
    {
        base.OnGameModeChanged(gameMode);
        if (gameMode == GameMode.Play)
        {
            var characterController = GetComponentInChildren<CharacterController>();
            if (characterController) characterController.enabled = true;
        }
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
    public bool IsActivePlayable()
    {
        return isActivePlayable;
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

    public void SetAsActivePlayable(bool val)
    {
        isActivePlayable = val;
    }

    public void SetFree()
    {
        throw new System.NotImplementedException();
    }

    public Transform CameraFollowRoot()
    {
        return cameraFollowRoot;
    }
}
