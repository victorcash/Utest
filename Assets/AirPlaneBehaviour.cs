using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;

public class AirPlaneBehaviour : KillableBehaviour, IPlayable
{
    public AeroplaneUserControl2Axis aeroplaneUserControl2Axis;
    public AeroplaneController aeroplaneController;
    public Transform cameraFollowRoot;
    public GameObject elementCamera;
    bool isActivePlable;
    private void Awake()
    {
        aeroplaneUserControl2Axis = GetComponentInChildren<AeroplaneUserControl2Axis>();
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
        aeroplaneUserControl2Axis.OnInputL(val);
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
        if (gameMode == GameMode.Play && isActivePlable)
        {
            aeroplaneUserControl2Axis.enabled = true;
            aeroplaneController.enabled = true;
            elementCamera.SetActive(true);
        }
        else
        {
            aeroplaneUserControl2Axis.enabled = false;
            aeroplaneController.enabled = false;
            elementCamera.SetActive(false);
        }
    }
}
