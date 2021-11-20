using UnityEngine;

public class CameraService
{
    public Camera editCamera;


    public CameraService(Camera editCamera)
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
