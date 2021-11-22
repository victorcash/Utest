using System.Collections.Generic;
using UnityEngine;

//kinda ranning out of time for the test here,
//so it's a very stupid vfx system, but it works :)
public class VFXController : MonoBehaviour
{
    public Transform VFXRoot;
    public List<GameObject> rainGos;
    public List<GameObject> snowGos;
    public List<GameObject> fogGos;
    public List<GameObject> thunderGos;
    public ThunderControl thunderControl;

    public void Init()
    {
        Services.GameStates.AddOnGameModeChangedListener(OnGameModeChanged);
        Services.EnvironmentController.OnFogChanged += SetFog;
        Services.EnvironmentController.OnRainChanged += SetRain;
        Services.EnvironmentController.OnSnowChanged += SetSnow;
        Services.EnvironmentController.OnThunderChanged += SetThunder;
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        if (gameMode == GameMode.Play)
        { 
        
        }
        else if (gameMode == GameMode.Edit)
        { 
        
        }
    }

    public void SetRain(float val)
    {
        ToggleObjectsByNormalizedValue(val, rainGos);
    }

    public void SetSnow(float val)
    {
        ToggleObjectsByNormalizedValue(val, snowGos);
    }

    public void SetFog(float val)
    {
        ToggleObjectsByNormalizedValue(val, fogGos);
    }
    public void SetThunder(float val)
    {
        thunderControl.SetThunder(val);
    }
    void ToggleObjectsByNormalizedValue(float val, List<GameObject> list)
    {
        var count = Mathf.CeilToInt(val * list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(i < count);
        }
    }
}
