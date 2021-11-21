using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Struckd/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int testVal;
    public float testFval;
    public int CsvWidth;
    [NonSerialized]
    public Transform elementsRoot;
    public LayerMask ElementLayer;
    public List<string> cities;

    public void Init(Transform elementsRoot)
    {
        this.elementsRoot = elementsRoot;
    }
}
