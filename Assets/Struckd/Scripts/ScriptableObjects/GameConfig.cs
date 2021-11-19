using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Struckd/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int testVal;
    public float testFval;
    public int CsvWidth;
    [NonSerialized]
    public Transform elementsRoot;
}
