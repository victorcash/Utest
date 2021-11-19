public abstract class KillableGamePlayElementBehaviour : GamePlayElementBehaviour, IKillable, IFaction
{
    protected float hp = 100f;
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
        return entry;
    }
    public override void Deserialize(string[] entry)
    {
        SetHp(float.Parse(entry.GetValue(CsvColumn.Hp)));
        SetHpMax(float.Parse(entry.GetValue(CsvColumn.HpMax)));
    }

    public void SetFaction(Faction faction)
    {
        this.faction = faction;
    }

    public Faction GetFaction() => faction;
}
