using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void ModifyHp(float val);
    float GetHp();
    float GetHpMax();

}

public static class IKillableExtensions
{
    public static void Kill(this IKillable IKillable)
    {
    }
}
