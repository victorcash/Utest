using System.Collections.Generic;
using UnityEngine;

public static class IListExtensions
{
    //public static void Shuffle<T>(this IList<T> list, RandomService randomService = null)
    //{
    //    if (randomService == null)
    //        randomService = RandomService.Game;

    //    var n = list.Count;
    //    while (n > 1)
    //    {
    //        n--;
    //        var k = randomService.Int(n + 1);
    //        var value = list[k];
    //        list[k] = list[n];
    //        list[n] = value;
    //    }
    //}

    public static T GetAtIndexWrapped<T>(this IList<T> collection, int index)
    {
        return collection.Count == 0
            ? default
            : collection[index.Mod(collection.Count)];
    }

    public static T GetAtIndexClamped<T>(this IList<T> collection, int index)
    {
        return collection.Count == 0
            ? default
            : collection[Mathf.Clamp(index, 0, collection.Count - 1)];
    }
}
