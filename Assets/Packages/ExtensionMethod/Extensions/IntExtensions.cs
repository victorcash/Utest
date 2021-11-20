using UnityEngine;

public static partial class IntExtensions
{
    public static int Mod(this int x, int m)
    {
        if (m == 0)
            return 0;

        var r = x % m;
        return r < 0 ? r + m : r;
    }

    public static bool IsInIntervalSequence(this int n, int start, int interval)
    {
        return n >= start && ((n - start) % interval) == 0;
    }

    public static T GetAtIndexClamped<T>(this T[] array, int index)
    {
        return array.Length == 0
            ? default
            : array[Mathf.Clamp(index, 0, array.Length - 1)];
    }

    public static int GetIndexInIntervalSequence(this int n, int start, int interval)
    {
        return n.IsInIntervalSequence(start, interval)
            ? (n - start) / interval
            : -1;
    }
}
