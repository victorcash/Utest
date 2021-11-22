﻿//Generated by https://json2csharp.com/
//json: {"data":{"getCityByName":{"id":"1796236","name":"Shanghai","country":"CN","coord":{"lon":121.4581,"lat":31.2222},"weather":{"summary":{"title":"Rain"},"temperature":{"actual":287.28,"feelsLike":287.2,"min":286.97,"max":288.08},"wind":{"speed":0.45,"deg":117},"clouds":{"all":75,"visibility":5000,"humidity":94},"timestamp":1637500701}}}}
// Root myDeserializedClass = JsonConvert.DeserializeObject<WeatherData>(myJsonResponse); 

public class WeatherData
{
    public string jsonRaw;
    public string jsonFormatted;
    public Data data { get; set; }
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Summary
    {
        public string title { get; set; }
    }

    public class Temperature
    {
        public double actual { get; set; }
        public double feelsLike { get; set; }
        public double min { get; set; }
        public double max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
        public int visibility { get; set; }
        public int humidity { get; set; }
    }

    public class Weather
    {
        public Summary summary { get; set; }
        public Temperature temperature { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int timestamp { get; set; }
    }

    public class GetCityByName
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
        public Weather weather { get; set; }
    }

    public class Data
    {
        public GetCityByName getCityByName { get; set; }
    }
}

