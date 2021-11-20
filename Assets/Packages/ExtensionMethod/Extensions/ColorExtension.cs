using UnityEngine;

public static class ColorExtension
{
    public static Color WithR(this Color c, float r) => new Color(r, c.g, c.b, c.a);
    public static Color WithG(this Color c, float g) => new Color(c.r, g, c.b, c.a);
    public static Color WithB(this Color c, float b) => new Color(c.r, c.g, b, c.a);
    public static Color WithA(this Color c, float a) => new Color(c.r, c.g, c.b, a);

    public static Color Saturation(this Color c, float saturation)
    {
        var value = c.r * 0.3f + c.g * 0.59f + c.b * 0.11f;
        return new Color(
            value + (c.r - value) * saturation,
            value + (c.g - value) * saturation,
            value + (c.b - value) * saturation,
            c.a
        );
    }
}
