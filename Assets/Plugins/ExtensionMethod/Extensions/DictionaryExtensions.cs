using System.Collections.Generic;

public static partial class DictionaryExtensions
{
    public static Dictionary<TKey, TValue> Combine<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> other)
    {
        var result = new Dictionary<TKey, TValue>();
        foreach (var kvp in dictionary)
            result.Add(kvp.Key, kvp.Value);
        foreach (var kvp in other)
            result.Add(kvp.Key, kvp.Value);

        return result;
    }
}
