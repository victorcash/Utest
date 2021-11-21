using System;
using System.Text;

public static class ExtensionStringArray
{
    public static void SetValue<T>(this string[] entry, T value, CsvColumn csvColumn) where T : IConvertible
    {
        entry[(int)csvColumn] = value.ToString();
    }
    public static string GetValue(this string[] entry, CsvColumn csvColumn)
    {
        return entry[(int)csvColumn];
    }
    public static string ToSingleLine(this string[] entry)
    {
        var line = new StringBuilder();
        for (int i = 0; i < entry.Length; i++)
        {
            if (i != 0) line.Append(",");
            line.Append(entry[i]);
        }
        return line.ToString();
    }
}

public enum CsvColumn
{
    ElementId = 0,
    PosX = 1,
    PosY = 2,
    PosZ = 3,
    RotX = 4,
    RotY = 5,
    RotZ = 6,
    ScalX = 7,
    ScalY = 8,
    ScalZ = 9,
    Hp = 10,
    HpMax = 11,
    Faction = 12,
    IsActivePlayable = 13
}

public static class CSVHelper
{
    public static string[] CreateEmptyEntry()
    {
        return new string[Services.Config.CsvWidth];
    }
}