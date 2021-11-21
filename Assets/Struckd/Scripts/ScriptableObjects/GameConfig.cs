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
    public List<string> cities;
    public float SecondsInDay = 86400f;
    public float TapSpeed = 0.15f;
}
