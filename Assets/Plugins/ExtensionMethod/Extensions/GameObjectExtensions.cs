using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static Rect GetBounds(this GameObject go)
    {
        var min = new Vector2(float.MaxValue, float.MaxValue);
        var max = new Vector2(-float.MaxValue, -float.MaxValue);

        var colliders = go.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            var bounds = collider.bounds;
            var boundsMin = bounds.min;
            var boundsMax = bounds.max;

            min.x = Mathf.Min(boundsMin.x, min.x);
            min.y = Mathf.Min(boundsMin.z, min.y);

            max.x = Mathf.Max(boundsMax.x, max.x);
            max.y = Mathf.Max(boundsMax.z, max.y);
        }

        return new Rect
        {
            min = min,
            max = max
        };
    }

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        var c = go.GetComponent<T>();

        if (c == null)
            c = go.AddComponent<T>();

        return c;
    }

    public static void SetActive(this IEnumerable<GameObject> collection, bool value)
    {
        foreach (var go in collection)
            go.SetActive(value);
    }
}
