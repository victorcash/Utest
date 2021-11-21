using GraphQlClient.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour
{
    public float rain { private set; get; }
    public float fog { private set; get; }
    public float thunder { private set; get; }
    public float dust { private set; get; }
    public float snow { private set; get; }
    public float time { private set; get; }
    public Action<float> OnRainChanged = (_) => { };
    public Action<float> OnFogChanged = (_) => { };
    public Action<float> OnThunderChanged = (_) => { };
    public Action<float> OnDustChanged = (_) => { };
    public Action<float> OnSnowChanged = (_) => { };
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

