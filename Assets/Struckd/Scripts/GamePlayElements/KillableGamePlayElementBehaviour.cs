public abstract class KillableGamePlayElementBehaviour : GamePlayElementBehaviour, IKillable, IFaction
{
    [UnityEngine.SerializeField]
    protected float hp = 100f;
    [UnityEngine.SerializeField]
    protected float hpMax = 100f;
    protected Faction faction;
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
        entry.SetValue(GetHpMax(), CsvColumn.HpMax);
        return entry;
    }
    public override void Deserialize(string[] entry)
    {
        base.Deserialize(entry);
        SetHp(float.Parse(entry.GetValue(CsvColumn.Hp)));
        SetHpMax(float.Parse(entry.GetValue(CsvColumn.HpMax)));
    }

    public void SetFaction(Faction faction)
    {
        this.faction = faction;
    }

    public Faction GetFaction() => faction;
}
