using System;
using System.Collections.Generic;

public static partial class IEnumerableExtensions
{
    public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();

        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }
}
