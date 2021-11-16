using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool ContainsLayer(this LayerMask layerMask, int layer) => (layerMask.value & (1 << layer)) > 0;
}
