using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;
using UnityStandardAssets.Vehicles.Car;

public class VehicleBehaviour : KillableBehaviour, IPlayable
{
    public CarUserControl carUserControl;
    public CarController carController;
    public Transform cameraFollowRoot;
    public GameObject elementCamera;
    bool isActivePlable;
    private void Awake()
    {
        carUserControl = GetComponentInChildren<CarUserControl>();
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
        carUserControl.OnInput(val);
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
            carUserControl.enabled = true;
            carController.enabled = true;
            elementCamera.SetActive(true);
            body.isKinematic = false;
        }
        else
        {
            carUserControl.enabled = false;
            carController.enabled = false;
            elementCamera.SetActive(false);
            body.isKinematic = true;
        }
    }
}
