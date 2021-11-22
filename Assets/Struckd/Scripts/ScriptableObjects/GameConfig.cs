using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Struckd/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int testVal;
    public float testFval;
    public int CsvWidth;
    public LayerMask ElementLayer;
    public FactionColorDictionary factionColorDictionary;
    public CitiesUTCOffsetDictionary cities;
    public float SecondsInDay = 86400f;
    public float durationCountAsHold = 0.25f;
}

[Serializable]
public class CitiesUTCOffsetDictionary : SerializableDictionary<string, int> { }
[Serializable]
public class FactionColorDictionary : SerializableDictionary<Faction, Color> { }