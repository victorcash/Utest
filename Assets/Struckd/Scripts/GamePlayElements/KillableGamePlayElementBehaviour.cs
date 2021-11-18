using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableGamePlayElementBehaviour : GamePlayElementBehaviour, IKillable
{
    protected float hp = 100f;
    protected float hpMax = 100f;
    public float GetHp() => hp;
    public float GetHpMax() => hpMax;
    public void SetHpMax(float val)
    {
        hpMax = val;
    }
    public void SetHp(float val)
    {
        hp = val;
    }
    public override string[] Serialize()
    {
        var entry = base.Serialize();
        entry.SetValue(GetHp(), CsvColumn.Hp);
        return entry;
    }
    public override void Deserialize(string[] entry)
    {
        SetHp(float.Parse(entry.GetValue(CsvColumn.Hp)));
        SetHpMax(float.Parse(entry.GetValue(CsvColumn.HpMax)));
    }
}
