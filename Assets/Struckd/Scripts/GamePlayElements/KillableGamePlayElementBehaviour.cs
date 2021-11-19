using System;
using UnityEngine;

public abstract class KillableGamePlayElementBehaviour : GamePlayElementBehaviour, IKillable, IFaction
{
    [SerializeField] protected float hp = 100f;
    [SerializeField] protected float hpMax = 100f;
    [SerializeField] protected Faction faction;

    public override string[] Serialize()
    {
        var entry = base.Serialize();
        entry.SetValue(GetHp(), CsvColumn.Hp);
        entry.SetValue(GetHpMax(), CsvColumn.HpMax);
        entry.SetValue(GetFaction(), CsvColumn.Faction);
        return entry;
    }
    public override void Deserialize(string[] entry)
    {
        base.Deserialize(entry);
        SetHp(float.Parse(entry.GetValue(CsvColumn.Hp)));
        SetHpMax(float.Parse(entry.GetValue(CsvColumn.HpMax)));
        SetFaction((Faction)Enum.Parse(typeof(Faction), entry.GetValue(CsvColumn.Faction)));
    }
    public float GetHp() => hp;
    public float GetHpMax() => hpMax;
    public Faction GetFaction() => faction;

    public virtual void SetHpMax(float val)
    {
        hpMax = val;
    }
    public virtual void SetHp(float val)
    {
        hp = val;
    }
    public virtual void SetFaction(Faction val)
    {
        faction = val;
    }
}
