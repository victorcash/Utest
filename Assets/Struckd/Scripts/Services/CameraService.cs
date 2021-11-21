using Cinemachine;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public void Init(CinemachineVirtualCamera virtualCamera)
    {
        Services.GameStates.AddOnGameModeChangedListener(OnGameModeChanged);
        this.virtualCamera = virtualCamera;
    }
    private void OnGameModeChanged(GameMode gameMode)
    {
        SetCameraMode(gameMode);
    }

    public void SetCameraMode(GameMode gameMode)
    {
        if (gameMode == GameMode.Edit)
        {
            Services.SceneReferences.editCamera.gameObject.SetActive(true);
            Services.SceneReferences.playCamera.gameObject.SetActive(false);
        }
        else if(gameMode == GameMode.Play)
        {
            Services.SceneReferences.editCamera.gameObject.SetActive(false);
            Services.SceneReferences.playCamera.gameObject.SetActive(true);
            var target = Services.Element.GetActivePlayable();
            Services.Camera.virtualCamera.Follow = target.CameraFollowRoot();
        }
    }
}
