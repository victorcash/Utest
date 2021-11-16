using System;

public static class ArrayExtension
{
    public static T[] Add<T>(this T[] source, T element)
    {
        Array.Resize(ref source, source.Length + 1);
        source[source.Length - 1] = element;
        return source;
    }

    public static T[] Remove<T>(this T[] source, T element)
    {
        var index = Array.IndexOf(source, element);
        return index < 0 ? source : source.RemoveAt(index);
    }

    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        var dest = new T[source.Length - 1];
        if (index > 0)
            Array.Copy(source, 0, dest, 0, index);

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
    }
}
