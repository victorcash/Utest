using UnityEngine;

public class CameraService
{
    public EditCamera editCamera;


    public CameraService(EditCamera editCamera)
    {
        this.editCamera = editCamera;
    }

    public void SetCameraMode(GameMode gameMode)
    {
        if (gameMode == GameMode.Edit)
        {

        }
        else
        {

        }
    }
}
