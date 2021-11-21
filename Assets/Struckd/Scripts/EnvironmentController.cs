using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public float rain { private set; get; }
    public float fog { private set; get; }
    public float thunder { private set; get; }
    public float dust { private set; get; }
    public float time { private set; get; }
    public Action<float> OnRainChanged = (_) => { };
    public Action<float> OnFogChanged = (_) => { };
    public Action<float> OnThunderChanged = (_) => { };
    public Action<float> OnDustChanged = (_) => { };
    public Action<float> OnTimeChanged = (_) => { };

    public void SetRainIntensity(float val)
    {
        OnRainChanged(val);
    }

    public void SetFogIntensity(float val)
    {
        OnFogChanged(val);
    }

    public void SetThunderIntensity(float val)
    {
        OnThunderChanged(val);
    }

    public void SetTime(float val)
    {
        OnTimeChanged(val);
    }

    public void SetDustIntensity(float val)
    {
        OnDustChanged(val);
    }
}
