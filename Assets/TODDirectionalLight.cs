using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TODDirectionalLight : MonoBehaviour
{
    public Gradient lightColors;
    public AnimationCurve lightIntensities;
    public AnimationCurve atmosphereIntensities;
    public AnimationCurve atmosphereThickness;
    private Light dLight;
    public float degreeMap = 180f;
    public float offset = 180f;
    public Material skybox;

    [Range(0f, 86400f)]
    public float testVal;

    private void Awake()
    {
        dLight = GetComponent<Light>();
    }
    private void Update()
    {
        SetTOD(testVal);
    }
    public void SetTOD(float val)
    {
        var sid = Services.Config.SecondsInDay;
        var nVal = val/ sid;
        var lightColor = lightColors.Evaluate(nVal);
        var intensity = lightIntensities.Evaluate(nVal);
        RenderSettings.ambientLight = lightColor * intensity;
        dLight.color = lightColor;
        dLight.intensity = intensity;
        transform.rotation = Quaternion.Euler(Mathf.Lerp(-degreeMap, degreeMap, nVal) - offset, 0, 0);
        skybox.SetFloat("_Exposure", atmosphereIntensities.Evaluate(nVal));
        skybox.SetFloat("_AtmosphereThickness", atmosphereThickness.Evaluate(nVal));
    }
}
