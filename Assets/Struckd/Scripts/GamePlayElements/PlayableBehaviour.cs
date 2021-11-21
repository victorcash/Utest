using StarterAssets;
using UnityEngine;

public class PlayableBehaviour : KillableBehaviour, IPlayable
{
    private bool isActivePlayable;
    public StarterAssetsInputs input;
    private void Awake()
    {
        input = GetComponentInChildren<StarterAssetsInputs>();
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
        SetAsActivePlayable(bool.Parse(entry.GetValue(CsvColumn.IsActivePlayable)));
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
}
