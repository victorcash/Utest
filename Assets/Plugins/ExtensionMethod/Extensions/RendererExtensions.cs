using System.Collections.Generic;
using UnityEngine;

public static class RendererExtensions
{
    public static void SetPropertyBlock(this IEnumerable<Renderer> renderers, MaterialPropertyBlock mpb)
    {
        foreach (var renderer in renderers)
        {
            renderer.SetPropertyBlock(mpb);
        }
    }
}
