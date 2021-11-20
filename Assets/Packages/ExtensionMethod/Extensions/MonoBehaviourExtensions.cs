using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void SetEnabled<T>(this IEnumerable<T> monoBehaviours, bool enabled) where T : MonoBehaviour
    {
        foreach (var monoBehaviour in monoBehaviours)
        {
            monoBehaviour.enabled = enabled;
        }
    }
}
