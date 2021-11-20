using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Struckd/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int testVal;
    public float testFval;
    public int CsvWidth;
    public float HpRange;
    [NonSerialized]
    public Transform elementsRoot;
    public LayerMask ElementLayer;

    public void Init(Transform elementsRoot)
    {
        this.elementsRoot = elementsRoot;
    }
}
