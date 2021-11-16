using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : GamePlayElementBehaviour, IKillable
{
    public float hp;
    public float hpMax;

    public float GetHp() => hp;
    public float GetHpMax() => hpMax;

    public void ModifyHp(float val)
    {
        throw new System.NotImplementedException();
    }
}
