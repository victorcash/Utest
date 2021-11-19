﻿using System;
using System.Text;
using UnityEngine;

public abstract class GamePlayElementBehaviour : MonoBehaviour
{
    public int elementID;
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
            Services.GamePlayElement.InitElement(this, OnPlay, OnEdit);
        }
    }
    public void InjectData(string[] entry)
    {
        Deserialize(entry);
    }
    protected virtual void OnDestroy()
    {
        Services.GamePlayElement.CleanUpElement(this, OnPlay, OnEdit);
    }
    public virtual void Remove()
    {
        Destroy(gameObject);
    }
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
        entry.SetValue(elementID, CsvColumn.ElementId);
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