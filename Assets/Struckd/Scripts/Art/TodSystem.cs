using UnityEngine;
public class TodSystem : MonoBehaviour
{
    public Gradient lightColors;
    public AnimationCurve lightIntensities;
    public AnimationCurve atmosphereIntensities;
    public AnimationCurve atmosphereThickness;
    public Light dLight;
    public float degreeMap = 180f;
    public float offset = 180f;
    public Material skybox;

    [Range(0f, 86400f)]
    public float sec;
    private void Start()
    {
        Services.EnvironmentController.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(float val)
    {
        SetTODSec(val);
    }

    private void Update()
    {
        SetTODSec(sec);
    }
    public void SetTODSec(float val)
    {
        sec = val;
        var sid = Services.Config.SecondsInDay;
        var nVal = val/ sid;
        var lightColor = lightColors.Evaluate(nVal);
        var intensity = lightIntensities.Evaluate(nVal);
        RenderSettings.ambientLight = lightColor * intensity;
        dLight.color = lightColor;
        dLight.intensity = intensity;
        dLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(-degreeMap, degreeMap, nVal) - offset, 0f, 0f);
        skybox.SetFloat("_Exposure", atmosphereIntensities.Evaluate(nVal));
        skybox.SetFloat("_AtmosphereThickness", atmosphereThickness.Evaluate(nVal));
    }
}
