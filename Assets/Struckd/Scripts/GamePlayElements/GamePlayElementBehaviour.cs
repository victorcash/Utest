using System;
using System.Text;
using UnityEngine;

public class GamePlayElementBehaviour : MonoBehaviour
{
    protected int elementInstanceId;
    private bool isInit;
    public virtual Transform GetRootTransform() => transform;
    protected virtual void Start()
    {
        Init();
    }
    protected virtual void OnPlay()
    { 
    
    }
    protected virtual void OnEdit()
    { 
    
    }
    public void Init()
    {
        if (!isInit)
        {
            isInit = true;
            Services.GamePlayElement.Init(this, OnPlay, OnEdit);
        }
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
        entry.SetValue(elementInstanceId, CsvColumn.PlacableId);
        entry.SetValue(transform.position.x, CsvColumn.PosX);
        entry.SetValue(transform.position.y, CsvColumn.PosY);
        entry.SetValue(transform.position.z, CsvColumn.PosZ);
        entry.SetValue(transform.rotation.eulerAngles.x, CsvColumn.RotX);
        entry.SetValue(transform.rotation.eulerAngles.y, CsvColumn.RotY);
        entry.SetValue(transform.rotation.eulerAngles.z, CsvColumn.RotZ);
        entry.SetValue(transform.localScale.x, CsvColumn.ScalX);
        entry.SetValue(transform.localScale.y, CsvColumn.ScalY);
        entry.SetValue(transform.localScale.z, CsvColumn.ScalZ);
        return entry;
    }
}
public static class ExtensionStringArray
{
    public static void SetValue<T>(this string[] entry, T value, CsvColumn csvColumn) where T : IConvertible
    {
        entry[(int)csvColumn] = value.ToString();
    }
    public static string GetValue(this string[] entry, CsvColumn csvColumn)
    {
        return entry[(int)csvColumn];
    }
    public static string ToSingleLine(this string[] entry)
    {
        var line = new StringBuilder();
        for (int i = 0; i < entry.Length; i++)
        {
            line.Append(entry[i]);
            if (i < entry.Length - 1)
            { 
                line.Append(",");
            }
        }
        return line.ToString();
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
