using System.Text;
using UnityEngine;

public static partial class StringExtensions
{
    public static string Red(this string s) => s.Colorize(Color.red);
    public static string Yellow(this string s) => s.Colorize(Color.yellow);
    public static string Green(this string s) => s.Colorize(Color.green);
    public static string Magenta(this string s) => s.Colorize(Color.magenta);
    public static string Blue(this string s) => s.Colorize(Color.blue);
    public static string Cyan(this string s) => s.Colorize(Color.cyan);
    public static string White(this string s) => s.Colorize(Color.white);
    public static string Gray(this string s) => s.Colorize(Color.gray);
    public static string Black(this string s) => s.Colorize(Color.black);

    public static string Colorize(this string s, Color color) => $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{s}</color>";

    public static string CamelCaseToUnderscoreCase(this string s)
    {
        var sb = new StringBuilder();
        var start = true;

        foreach (var c in s)
        {
            if (!start && char.IsUpper(c))
                sb.Append("_");

            sb.Append(char.ToLower(c));
            start = char.IsWhiteSpace(c);
        }

        return sb.ToString();
    }
}
