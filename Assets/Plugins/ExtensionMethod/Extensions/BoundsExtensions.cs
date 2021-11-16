using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtensions
{
    public static Bounds GetBounds(this IEnumerable<Bounds> collection)
    {
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = -min;

        foreach (var bounds in collection)
        {
            var bMin = bounds.min;
            var bMax = bounds.max;

            if (bMin.x < min.x) min.x = bMin.x;
            if (bMin.y < min.y) min.y = bMin.y;
            if (bMin.z < min.z) min.z = bMin.z;

            if (bMax.x > max.x) max.x = bMax.x;
            if (bMax.y > max.y) max.y = bMax.y;
            if (bMax.z > max.z) max.z = bMax.z;
        }

        return new Bounds {min = min, max = max};
    }

    public static Rect ToRectXZ(this Bounds bounds)
    {
        return new Rect((bounds.center - bounds.extents).XZ(), bounds.size.XZ());
    }
}
