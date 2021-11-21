using GraphQlClient.Core;
using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironmentController : MonoBehaviour
{
    public float rain { private set; get; }
    public float fog { private set; get; }
    public float thunder { private set; get; }
    public float snow { private set; get; }
    public float time { private set; get; }
    public Action<float> OnRainChanged = (_) => { };
    public Action<float> OnFogChanged = (_) => { };
    public Action<float> OnThunderChanged = (_) => { };
    public Action<float> OnSnowChanged = (_) => { };
    public Action<float> OnTimeChanged = (_) => { };

    public string SecondsToTimeString(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"hh\:mm");
        return str;
    }
    public float SecondsToNormalized(float seconds) => seconds / Services.Config.SecondsInDay;
    public float NormalizedToSeconds(float nval) => nval * Services.Config.SecondsInDay;
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

    public void SetSnowIntensity(float val)
    {
        OnSnowChanged(val);
    }
    public async void GetWeatherData(string cityName, Action<string> OnHaveData)
    {
        var graph = Services.GraphApi;
        GraphApi.Query query = graph.GetQueryByName("GetCityByName", GraphApi.Query.Type.Query);
        query.SetArgs(new { name = cityName });
        UnityWebRequest request = await graph.Post(query);
        var json = request.downloadHandler.text;
        var text = HttpHandler.FormatJson(json);
        OnHaveData(text);
        var wd = JsonConvert.DeserializeObject<WeatherData>(json);
        Debug.Log(wd.data.getCityByName.name);
        Debug.Log(wd.data.getCityByName.id);
    }
}

