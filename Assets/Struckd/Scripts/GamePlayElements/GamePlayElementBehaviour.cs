using System;
using UnityEngine;

public class GamePlayElementBehaviour : MonoBehaviour
{
    protected int elementInstanceId;
    public virtual Transform GetRootTransform() => transform;
    protected virtual void Awake()
    {
        
    }
    protected virtual void OnPlay()
    { 
    
    }
    protected virtual void OnEdit()
    { 
    
    }
    public void Init()
    {
        Services.GamePlayElement.Init(this, OnPlay, OnEdit);
    }
    public void InjectData(string entry)
    {
        Deserialize(entry.Split(','));
    }
    protected virtual void OnDestroy()
    {
        Services.GamePlayElement.CleanUp(this, OnPlay, OnEdit);
    }
    public virtual void Remove()
    {
        Destroy(gameObject);
    }
    public void SetElementInstanceId(int id)
    {
        elementInstanceId = id;
    }
    public int GetPlacableId() => elementInstanceId;
    public virtual void Deserialize(string[] entry)
    {
        transform.position = new Vector3(
            float.Parse(entry.GetValue(CsvColumn.PosX)),
            float.Parse(entry.GetValue(CsvColumn.PosY)),
            float.Parse(entry.GetValue(CsvColumn.PosZ))
            );
        transform.rotation = Quaternion.Euler(new Vector3(
            float.Parse(entry.GetValue(CsvColumn.RotX)),
            float.Parse(entry.GetValue(CsvColumn.RotY)),
            float.Parse(entry.GetValue(CsvColumn.RotZ))
            ));
        transform.localScale = new Vector3(
            float.Parse(entry.GetValue(CsvColumn.ScalX)),
            float.Parse(entry.GetValue(CsvColumn.ScalY)),
            float.Parse(entry.GetValue(CsvColumn.ScalZ))
            );
    }
    public virtual string[] Serialize()
    {
        var entry = CSVHelper.CreateEmptyEntry();
        entry.SetValue(CsvColumn.PlacableId, elementInstanceId);
        entry.SetValue(CsvColumn.PosX, transform.position.x);
        entry.SetValue(CsvColumn.PosY, transform.position.y);
        entry.SetValue(CsvColumn.PosZ, transform.position.z);
        entry.SetValue(CsvColumn.RotX, transform.rotation.eulerAngles.x);
        entry.SetValue(CsvColumn.RotY, transform.rotation.eulerAngles.y);
        entry.SetValue(CsvColumn.RotZ, transform.rotation.eulerAngles.z);
        entry.SetValue(CsvColumn.ScalX, transform.localScale.x);
        entry.SetValue(CsvColumn.ScalY, transform.localScale.y);
        entry.SetValue(CsvColumn.ScalZ, transform.localScale.z);
        return entry;
    }
}
public static class ExtensionStringArray
{
    public static void SetValue<T>(this string[] entry, CsvColumn csvColumn, T value) where T : IConvertible
    {
        entry[(int)csvColumn] = value.ToString();
    }
    public static string GetValue(this string[] entry, CsvColumn csvColumn)
    {
        return entry[(int)csvColumn];
    }
}

public enum CsvColumn
{ 
    PlacableId = 0,
    PosX = 1,
    PosY = 2,
    PosZ = 3,
    RotX = 4,
    RotY = 5,
    RotZ = 6,
    ScalX = 7,
    ScalY = 8,
    ScalZ = 9,
    Hp = 10,
    HpMax = 11
}

public static class CSVHelper
{
    public static string[] CreateEmptyEntry()
    {
        return new string[Services.Config.CsvWidth];
    }
}
