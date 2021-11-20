
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}

public static class ExtensionDirection
{
    public static Vector3Int ToV3Int(this Direction dir)
    {
        var vInt3 = new Vector3Int();

        switch (dir)
        {
            case (Direction.North):
            {
                vInt3 = new Vector3Int(0, 0, 1);
            }
                break;
            case (Direction.East):
            {
                vInt3 = new Vector3Int(1, 0, 0);
            }
                break;
            case (Direction.South):
            {
                vInt3 = new Vector3Int(0, 0, -1);
            }
                break;
            case (Direction.West):
            {
                vInt3 = new Vector3Int(-1, 0, 0);
            }
                break;
        }

        return vInt3;
    }

    public static Vector3 ToV3(this Direction dir) => dir.ToV3Int();

    public static Direction ToDirection(this Vector3Int dir)
    {
        Direction direction = Direction.North;

        if (dir == new Vector3(0, 0, 1))
        {
            direction = Direction.North;
        }
        else if (dir == new Vector3(1, 0, 0))
        {
            direction = Direction.East;
        }
        else if (dir == new Vector3(0, 0, -1))
        {
            direction = Direction.South;
        }
        else if (dir == new Vector3(-1, 0, 0))
        {
            direction = Direction.West;
        }

        return direction;
    }
}

public static class ExtensionVector3List
{
    public static List<Vector3> Simplify(this List<Vector3> path)
    {
        if (path.Count <= 2) return path;
        List<Vector3> simplified = new List<Vector3>();
        var lastDir = Vector3Int.zero;
        for (int i = 0; i < path.Count - 1; i++)
        {
            var samePos = path[i + 1] == path[i];
            var thisDir = (path[i + 1] - path[i]).normalized.RoundToInt3();
            if (thisDir != lastDir && !samePos)
            {
                lastDir = thisDir;
                simplified.Add(path[i]);
            }
        }

        simplified.Add(path[path.Count - 1]);
        return simplified;
    }

    public static List<Vector3> Flatten(this List<Vector3> path, float height = 0f)
    {
        var flatten = new List<Vector3>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            flatten.Add(new Vector3(path[i].x, height, path[i].z));
        }

        return flatten;
    }

    public static List<Vector3> Shift(this List<Vector3> path, Vector3 offset)
    {
        var shifted = new List<Vector3>();
        if (path.Count < 1) return path;
        for (int i = 0; i < path.Count; i++)
        {
            shifted.Add(path[i] + offset);
        }

        return shifted;
    }

    public static float PathLength(this List<Vector3> path)
    {
        var pathLength = 0f;

        for (int i = 0; i < path.Count - 1; i++)
        {
            pathLength += Vector3.Distance(path[i], path[i + 1]);
        }

        return pathLength;
    }
}

public static class ExtensionVector2
{
    public static Vector2Int RoundToInt2(this Vector2 v2) => new Vector2Int {x = Mathf.RoundToInt(v2.x), y = Mathf.RoundToInt(v2.y)};
}

public static class ExtensionVector3
{
    public static Vector3Int RoundToInt3(this Vector3 v3) => new Vector3Int {x = Mathf.RoundToInt(v3.x), y = Mathf.RoundToInt(v3.y), z = Mathf.RoundToInt(v3.z)};
    public static Vector3Int FloorToInt3(this Vector3 v3) => new Vector3Int {x = Mathf.FloorToInt(v3.x), y = Mathf.FloorToInt(v3.y), z = Mathf.FloorToInt(v3.z)};
    public static Vector3Int CeilToInt3(this Vector3 v3) => new Vector3Int {x = Mathf.CeilToInt(v3.x), y = Mathf.CeilToInt(v3.y), z = Mathf.CeilToInt(v3.z)};
    public static Vector3Int Flip(this Vector3Int v3) => new Vector3Int {x = v3.x * -1, y = v3.y * -1, z = v3.z * -1};
    public static Vector3 Flip(this Vector3 v3) => new Vector3 {x = v3.x * -1f, y = v3.y * -1f, z = v3.z * -1f};
    public static Vector3 Flatten(this Vector3 v3) => new Vector3 {x = v3.x, y = 0f, z = v3.z};
    public static Vector2 FlattenTo2D(this Vector3 v3) => new Vector2 {x = v3.x, y = v3.z};
    public static Vector3 InflateTo3D(this Vector2 v2) => new Vector3 {x = v2.x, y = 0f, z = v2.y};
    public static Vector3 Empty(this Vector3 v3) => new Vector3(-1000f, -1000f, -1000f);
    public static float Distance(this Vector3 v3, Vector3 other) => Vector3.Distance(v3, other);

    public static Vector3 NormalizeWithScreenSize(this Vector3 v3)
    {
        return new Vector3(v3.x / Screen.width, v3.y / Screen.height, v3.z);
    }

    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    public static int LeftRightTest(Vector3 forward, Vector3 other)
    {
        Vector3 perp = Vector3.Cross(forward, other);
        float dir = Vector3.Dot(perp, Vector3.up);
        if (dir > 0f)
        {
            //right
            return 1;
        }
        else if (dir < 0f)
        {
            //left
            return -1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// clockwise 0-360
    /// </summary>
    public static float Angle(Vector3 from, Vector3 to)
    {
        //get unsigned angle
        var dot = Vector3.Dot(from, to);
        dot = dot / (from.magnitude * to.magnitude);
        var acos = Mathf.Acos(dot);
        var angle = acos * 180 / Mathf.PI;

        //get sign
        Vector3 perp = Vector3.Cross(from, to);
        float dir = Vector3.Dot(perp, Vector3.up);
        if (dir > 0f)
        {
            //right
            return angle;
        }
        else if (dir < 0f)
        {
            //left
            return 360 - angle;
        }
        else
        {
            return angle;
        }
    }

    /// <summary>
    /// clockwise 0-360 on the plan
    /// </summary>
    public static float Angle2D(Vector3 from, Vector3 to)
    {
        from = new Vector3(from.x, 0f, from.z);
        to = new Vector3(to.x, 0f, to.z);
        //get unsigned angle
        var dot = Vector3.Dot(from, to);
        dot = dot / (from.magnitude * to.magnitude);
        var acos = Mathf.Acos(dot);
        var angle = acos * 180 / Mathf.PI;

        //get sign
        Vector3 perp = Vector3.Cross(from, to);
        float dir = Vector3.Dot(perp, Vector3.up);
        if (dir > 0f)
        {
            //right
            return angle;
        }
        else if (dir < 0f)
        {
            //left
            return 360 - angle;
        }
        else
        {
            return angle;
        }
    }
}

public static class ExtensionTime
{
    public static DateTime RoundToDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day);
}

public static class ExtensionUI
{
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    #if UNITY_EDITOR
            eventDataCurrentPosition.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
    #else
            eventDataCurrentPosition.position = UnityEngine.InputSystem.Touchscreen.current.primaryTouch.position.ReadValue();
    #endif
#else
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#endif
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static int PointerOverUIObjectsCount()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

    #if UNITY_EDITOR
            eventDataCurrentPosition.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
    #else
                eventDataCurrentPosition.position = UnityEngine.InputSystem.Touchscreen.current.primaryTouch.position.ReadValue();
    #endif
#else
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#endif
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count;
    }

    public static void FadeGroup(this CanvasGroup cg, float alpha)
    {
        cg.alpha = alpha;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public static void HideGroup(this CanvasGroup cg)
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public static void ShowGroup(this CanvasGroup cg)
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public static void Sprite(this Button button, Sprite sprite)
    {
        var img = button.gameObject.GetComponent<Image>();
        img.sprite = sprite;
    }
}

public static class ExtensionInput
{
    public static Vector3 MousePosOnPlane(Vector3 planePoint, Vector3 planeNormal, Camera camera)
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Math_3D.LinePlaneIntersection(out var intersection, ray.origin, ray.direction, planeNormal, planePoint);
        return intersection;
    }

    public static Vector3Int MousePosOnSnapPlane(Vector3 planePoint, Vector3 planeNormal, Camera camera)
    {
        return MousePosOnPlane(planePoint, planeNormal, camera).RoundToInt3();
    }

    public static Vector3 MousePosOnSnapPlane(Vector3 planePoint, Vector3 planeNormal, Camera camera, float gridScale = 1f)
    {
        var ratio = 1f / gridScale;
        var pos = MousePosOnPlane(planePoint, planeNormal, camera);
        if (gridScale == 0) return pos;
        pos *= ratio;
        pos = pos.Round();
        pos /= ratio;
        return pos;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
    

public static class ExtensionArray
{
    public static T First<T>(this T[] arr) where T : class
    {
        if (arr.Length <= 0) return null;
        return arr[0];
    }

    public static void Populate<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = value;
        }
    }

    public static void Populate<T>(this T[,] arr, T value)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                arr[i, j] = value;
            }
        }
    }

    public static T[] Merge<T>(T[] array1, T[] array2)
    {
        T[] newArray = new T[array1.Length + array2.Length];
        Array.Copy(array1, newArray, array1.Length);
        Array.Copy(array2, 0, newArray, array1.Length, array2.Length);
        return newArray;
    }

    public static T[,] Trim2DArray<T>(int rowToRemove, int columnToRemove, T[,] originalArray)
    {
        T[,] result = new T[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

        for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
        {
            if (i == rowToRemove)
                continue;

            for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
            {
                if (k == columnToRemove)
                    continue;

                result[j, u] = originalArray[i, k];
                u++;
            }

            j++;
        }

        return result;
    }

    public static T[,] Trim2DArrayColumn<T>(int colToRemove, T[,] originalArray)
    {
        T[,] result = new T[originalArray.GetLength(0) - 1, originalArray.GetLength(1)];

        for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
        {
            if (i == colToRemove)
                continue;

            for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
            {
                result[j, u] = originalArray[i, k];
                u++;
            }

            j++;
        }

        return result;
    }

    public static T[,] Trim2DArrayRow<T>(int rowToRemove, T[,] originalArray)
    {
        T[,] result = new T[originalArray.GetLength(0), originalArray.GetLength(1) - 1];

        for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
        {
            for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
            {
                if (k == rowToRemove)
                    continue;

                result[j, u] = originalArray[i, k];
                u++;
            }

            j++;
        }

        return result;
    }

    public static T[] Trim1DArrayRow<T>(int row2Remove, T[] originArray)
    {
        T[] result = new T[originArray.Length - 1];
        for (int o = 0, n = 0; o < result.Length; o++)
        {
            if (o == row2Remove) continue;
            result[n] = originArray[o];
            n++;
        }

        return result;
    }
}

public static class ExtensionRectTransform
{
    public static void SetHeight(this RectTransform rt, float height)
    {
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
    }

    public static void SetWidth(this RectTransform rt, float width)
    {
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }

    public static void StretchToParent(this RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.pivot = Vector2.one * 0.5f;
    }

    public static void SetX(this RectTransform rt, float x)
    {
        rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);
    }

    public static void SetY(this RectTransform rt, float y)
    {
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, y);
    }

    public static Vector3 GetUIPositionByScreenPoint(this RectTransform rt)
    {
        var parentRt = rt.transform.parent.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRt, Input.mousePosition, null, out var uiPos2D);
        return uiPos2D;
    }
    public static Vector3 GetScreenPoint(this RectTransform rt)
    {
        var root = rt.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        var screenPoint = RectTransformUtility.CalculateRelativeRectTransformBounds(root, rt).center;
        return screenPoint;
    }
    public static void SetUIPositionByMouseInput(this Graphic graphic, Vector3 offset = default)
    {
        graphic.rectTransform.localPosition = graphic.rectTransform.GetUIPositionByScreenPoint() + offset;
    }
}

//UnityEngine.UI.Graphic
public static class ExtensionGraphic
{
    //this is very weird! why does ScreenPointToWorldPointInRectangle give you pixel position???
    public static Vector3 ScreenPointInPixel(this Graphic graphic)
    {
        var parentRT = graphic.transform.parent.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(parentRT, graphic.rectTransform.position, null, out var pixelPoint);
        return pixelPoint;
    }

    public static Vector3 ScreenPointInPixelNormalized(this Graphic graphic)
    {
        var pixelPoint = graphic.ScreenPointInPixel();
        Vector3 result = pixelPoint.NormalizeWithScreenSize();
        return result;
    }
}

public static class ExtensionTransform
{
    public static void Reset(this Transform transform, bool resetScale = false)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (resetScale) transform.localScale = Vector3.one;
    }

    //obj origin at bottom center
    public static void SquashSideCapUp(this Transform transform, Vector3 rootPos, Vector3 dir)
    {
        var vol = Vector3.one - dir.Abs();
        vol.y = 1f / (vol.x * vol.z);
        transform.localScale = vol;
        transform.localPosition = rootPos + dir * 0.5f;
    }

    //obj origin at bottom center
    public static void SquashDownSideExt(this Transform transform, float newWidth, float extraVolume = 0f)
    {
        transform.localScale = Vector3.one.KeepVolumeExpand(newWidth, extraVolume);
    }

    public static Vector3 KeepVolumeExpand(this Vector3 v3, float newWidth, float extraVolume = 0f)
    {
        var h = (1f + extraVolume) / (newWidth * newWidth);
        return new Vector3(newWidth, h, newWidth);
    }

    //Breadth-first search
    public static Transform FindChildBFS(this Transform parent, string name)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(parent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == name)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }

        return null;
    }

    //Depth-first search
    public static Transform FindChildDFS(this Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            var result = child.FindChildDFS(name);
            if (result != null)
                return result;
        }

        return null;
    }

    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void DestroyAllChildrenImmediate(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public static int EnabledChildCount(this Transform transform)
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                count += 1;
            }
        }

        return count;
    }

    //Returns the rotated Vector3 using a Quaternion
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Quaternion angle)
    {
        return angle * (point - pivot) + pivot;
    }

    //Returns the rotated Vector3 using Euler
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 euler)
    {
        return RotateAroundPivot(point, pivot, Quaternion.Euler(euler));
    }

    //Rotates the Transform's position using a Quaternion
    public static void RotateAroundPivot(this Transform me, Vector3 pivot, Quaternion angle)
    {
        me.position = me.position.RotateAroundPivot(pivot, angle);
    }

    //Rotates the Transform's position using Euler
    public static void RotateAroundPivot(this Transform me, Vector3 pivot, Vector3 euler)
    {
        me.position = me.position.RotateAroundPivot(pivot, Quaternion.Euler(euler));
    }

    //Rotates the Transform's position using Euler
    public static void RotateAroundAxisByDegree(this Transform transform, Vector3 axisPoint, Vector3 eulerRotation, Vector3 rootPos, float extraHeight = 0f)
    {
        transform.position = rootPos.RotateAroundPivot(axisPoint, Quaternion.Euler(eulerRotation));
        transform.position += new Vector3(0f, extraHeight, 0f);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }

    public static Vector2 Object2CanvasPos(this Transform tsf, RectTransform parentRect)
    {
        Vector2 posCanvas;
        var pos3d = tsf.position;
        Vector3 offsetPos = pos3d;
        Vector2 pos2d = Camera.main.WorldToScreenPoint(offsetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, pos2d, null, out posCanvas);
        return posCanvas;
    }
}

public static class ExtensionGridLayoutGroup
{
    public enum UpdateType
    {
        Vertical,
        Horizontal,
        Both
    }

    public static float GetCurrentHeight(this GridLayoutGroup glg, int space)
    {
        var padding = glg.padding.top + glg.padding.bottom;
        var gridSize = glg.cellSize.y * (glg.gameObject.transform.EnabledChildCount() + space);
        var spacing = glg.spacing.y * (glg.gameObject.transform.EnabledChildCount() + space);
        return padding + gridSize + spacing;
    }

    public static float GetCurrentWidth(this GridLayoutGroup glg, int space)
    {
        var padding = glg.padding.left + glg.padding.right;
        var gridSize = glg.cellSize.x * (glg.gameObject.transform.EnabledChildCount() + space);
        var spacing = glg.spacing.x * (glg.gameObject.transform.EnabledChildCount() + space);
        return padding + gridSize + spacing;
    }

    public static void UpdateSize(this GridLayoutGroup glg, UpdateType updateType, int space)
    {
        var rt = glg.gameObject.GetComponent<RectTransform>();
        switch (updateType)
        {
            case (UpdateType.Vertical):
            {
                rt.SetHeight(glg.GetCurrentHeight(space));
            }
                break;
            case (UpdateType.Horizontal):
            {
                rt.SetWidth(glg.GetCurrentWidth(space));
            }
                break;
            case (UpdateType.Both):
            {
                rt.SetHeight(glg.GetCurrentHeight(space));
                rt.SetWidth(glg.GetCurrentWidth(space));
            }
                break;
        }
    }
}

public static class ExtensionGamePlay
{
    public static float GetDistance(this Transform myTransform, GameObject targetObj)
    {
        Vector3 myPos = myTransform.position;
        Vector3 tarPos = targetObj.transform.position;
        float distance = Vector3.Distance(myPos, tarPos);
        return distance;
    }

    public static float GetDistance(this Transform myTransform, Transform targetTrans)
    {
        return Vector3.Distance(myTransform.position, targetTrans.position);
    }

    public enum WarpType
    {
        Loop,
        Clamp
    }

    /// <summary>
    /// increase index safely within list bounds
    /// </summary>
    public static int IncreasedIndex<T>(this int index, List<T> list, WarpType warpType = WarpType.Clamp)
    {
        if (warpType == WarpType.Loop)
        {
            if (index < list.Count - 1)
                index++;
            else
                index = 0;
        }
        else if (warpType == WarpType.Clamp)
        {
            if (index < list.Count - 1)
                index++;
            else
                index = list.Count - 1;
        }

        return index;
    }

    /// <summary>
    /// decrease index safely within list bounds
    /// </summary>
    public static int DecreasedIndex<T>(this int index, List<T> list, WarpType warpType = WarpType.Clamp)
    {
        if (warpType == WarpType.Loop)
        {
            if (index <= 0)
                index = list.Count - 1;
            else
                index--;
        }
        else if (warpType == WarpType.Clamp)
        {
            if (index <= 0)
                index = 0;
            else
                index--;
        }

        return index;
    }

    public static void SetChild(this GameObject parentObj, GameObject childObj, bool worldSpace = true)
    {
        parentObj.transform.SetChild(childObj.transform);
    }

    public static void SetChild(this Transform parent, Transform child, bool worldSpace = true)
    {
        child.transform.SetParent(parent.transform, worldSpace);
    }

    public static float V2W(this float v, float r)
    {
        //velocity to angular velocity
        return 360f * v / (2f * Mathf.PI * r);
    }

    public static float W2V(this float w, float r)
    {
        //angular velocity to velocity
        return (w * (2f * Mathf.PI * r)) / 360f;
    }
}

public static class ColorExt
{
    public static Color RandomRange(Color color1, Color color2)
    {
        float t = UnityEngine.Random.value;
        return Color.Lerp(color1, color2, t);
    }

    public static Color ColorGradientRGB(float value)
    {
        float r = 0, g = 0, b = 0;
        float mappedValue = value * 3f;
        if (mappedValue <= 1f)
        {
            r = 1f;
            b = 0f;
            g = mappedValue;
        }

        if (mappedValue > 1f && mappedValue <= 2f)
        {
            g = 1f;
            b = 0f;
            r = 1f - (mappedValue - 1f);
        }

        if (mappedValue > 2f)
        {
            r = 0f;
            g = 1f;
            b = mappedValue - 2f;
        }

        return new Color(r, g, b);
    }
}

public static class ExtensionVector3Int
{
    public static Vector3 Round(this Vector3 v3)
    {
        return new Vector3(Mathf.Round(v3.x), Mathf.Round(v3.y), Mathf.Round(v3.z));
    }

    public static Vector3 Snap(this Vector3 v3, float gridScale = 1f)
    {
        var ratio = 1f / gridScale;
        var pos = v3;
        pos *= ratio;
        pos = pos.Round();
        pos /= ratio;
        return pos;
    }

    public static float Distance2D(this Vector3 v1, Vector3 v2)
    {
        Vector2 v12d = new Vector2(v1.x, v1.z);
        Vector2 v22d = new Vector2(v2.x, v2.z);

        return Vector2.Distance(v12d, v22d);
    }

    public static Vector3Int front => new Vector3Int(0, 0, 1);
    public static Vector3Int back => new Vector3Int(0, 0, -1);
    public static Vector3Int right => Vector3Int.right;
    public static Vector3Int left => Vector3Int.left;
    public static Vector3Int up => Vector3Int.up;
    public static Vector3Int down => Vector3Int.down;
    public static List<Vector3Int> direction4 = new List<Vector3Int>() {front, back, right, left};

    public static List<Vector3Int> GetFlanks(this Vector3Int v3i)
    {
        if (v3i == front || v3i == back)
        {
            return new List<Vector3Int>() {right, left};
        }

        if (v3i == right || v3i == left)
        {
            return new List<Vector3Int>() {front, back};
        }

        return null;
    }

    /// <summary>
    /// Rotate a V3I clockwise in the step of 90 degrees in 2D space of X,Z
    /// </summary>
    public static Vector3Int Rotate(this Vector3Int dir, float degree)
    {
        degree = Mathf.Deg2Rad * degree * -1; //make it clockwise
        var x = dir.x * Mathf.Cos(degree) - dir.z * Mathf.Sin(degree);
        var z = dir.x * Mathf.Sin(degree) + dir.z * Mathf.Cos(degree);
        var v3 = new Vector3(x, 0f, z).normalized;
        return new Vector3Int(Mathf.RoundToInt(v3.x), 0, Mathf.RoundToInt(v3.z));
    }
}

public static class ExtensionRenderTexture
{
    public static Texture2D toTexture2D(this RenderTexture rTex)
    {
        var width = rTex.width;
        var height = rTex.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}

public static class ExtensionList
{
    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list.Count <= 0)
        {
            return default(T);
        }

        return list[(int)UnityEngine.Random.Range(0, list.Count)];
    }

    public static void SetActive<T>(this List<T> list, bool v) where T : Component
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(v);
        }
    }

    public static T GetElementWarped<T>(this List<T> list, ref int index) where T : class
    {
        if (list == null || list.Count == 0) return null;
        while (index >= list.Count)
        {
            index -= list.Count;
        }

        while (index < 0)
        {
            index += list.Count;
        }

        return list[index];
    }

    public static T GetElementClamped<T>(this List<T> list, ref int index) where T : class
    {
        if (list == null || list.Count == 0) return null;
        if (index >= list.Count)
        {
            index = list.Count - 1;
        }

        if (index < 0)
        {
            index = 0;
        }

        return list[index];
    }
}

public static class ExtensionCoroutine
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}

public static class ExtensionString
{
    public static string SetValue(this string text, string val)
    {
        return text.Replace("#", val);
    }
}

public static class ExtensionRandom
{
    public struct Range
    {
        public int min;
        public int max;

        public int range => max - min + 1;

        public Range(int aMin, int aMax)
        {
            min = aMin;
            max = aMax;
        }
    }

    public static int RandomValueFromRanges(params Range[] ranges)
    {
        if (ranges.Length == 0)
            return 0;
        int count = 0;
        foreach (Range r in ranges)
            count += r.range;
        int sel = UnityEngine.Random.Range(0, count);
        foreach (Range r in ranges)
        {
            if (sel < r.range)
            {
                return r.min + sel;
            }

            sel -= r.range;
        }

        throw new Exception("This should never happen");
    }
}

public static class ExtensionGeneric
{
    //https://stackoverflow.com/questions/212198/what-is-the-c-sharp-using-block-and-why-should-i-use-it
    public static T DeepClone<T>(this T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            BinaryFormatter formatter = SuperSerializer.GetFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }
}

public static class ExtensionGizmos
{
    public static void DrawLine(Vector3 p1, Vector3 p2, float width, Color color, Vector3 offset)
    {
        var oldColor = Gizmos.color;
        Gizmos.color = color;
        p1 = p1 + offset;
        p2 = p2 + offset;
        int count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
        if (count == 1)
        {
            Gizmos.DrawLine(p1, p2);
        }
        else
        {
            Camera c = Camera.current;
            if (c == null)
            {
                Debug.LogError("Camera.current is null");
                return;
            }

            var scp1 = c.WorldToScreenPoint(p1);
            var scp2 = c.WorldToScreenPoint(p2);

            Vector3 v1 = (scp2 - scp1).normalized; // line direction
            Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

            for (int i = 0; i < count; i++)
            {
                Vector3 o = 0.99f * n * width * ((float)i / (count - 1) - 0.5f);
                Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                Gizmos.DrawLine(origin, destiny);
            }
        }

        Gizmos.color = oldColor;
    }
}

public static class ExtensionMath
{
    public static Vector3[] LineCircleIntersection(Vector3 p1, Vector3 p2, Vector3 center, float radius)
    {
        Vector3 dp = new Vector3();
        Vector3[] sect;
        float a, b, c;
        float bb4ac;
        float mu1;
        float mu2;

        //  get the distance between X and Z on the segment
        dp.x = p2.x - p1.x;
        dp.z = p2.z - p1.z;
        //   I don't get the math here
        a = dp.x * dp.x + dp.z * dp.z;
        b = 2 * (dp.x * (p1.x - center.x) + dp.z * (p1.z - center.z));
        c = center.x * center.x + center.z * center.z;
        c += p1.x * p1.x + p1.z * p1.z;
        c -= 2 * (center.x * p1.x + center.z * p1.z);
        c -= radius * radius;
        bb4ac = b * b - 4 * a * c;
        if (Mathf.Abs(a) < float.Epsilon || bb4ac < 0)
        {
            //  line does not intersect
            return new Vector3[] { };
        }

        mu1 = (-b + Mathf.Sqrt(bb4ac)) / (2 * a);
        mu2 = (-b - Mathf.Sqrt(bb4ac)) / (2 * a);
        sect = new Vector3[2];
        sect[0] = new Vector3(p1.x + mu1 * (p2.x - p1.x), 0, p1.z + mu1 * (p2.z - p1.z));
        sect[1] = new Vector3(p1.x + mu2 * (p2.x - p1.x), 0, p1.z + mu2 * (p2.z - p1.z));

        return sect;
    }

    // public static Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
    // {
    //     direction.Normalize();
    //     Vector2 lhs = point - origin;
    //
    //     float dotP = Vector2.Dot(lhs, direction);
    //     return origin + direction * dotP;
    // }
    public static Vector2 FindNearestPointOnLine2D(Vector2 start, Vector2 end, Vector2 point)
    {
        //Get heading
        Vector2 heading = (end - start);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        //Do projection from the point but clamp it
        Vector2 lhs = point - start;
        float dotP = Vector2.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return start + heading * dotP;
    }

    public static Vector3 FindNearestPointOnLineTopDown(Vector3 start3D, Vector3 end3D, Vector3 point3D)
    {
        var start2D = start3D.FlattenTo2D();
        var end2D = end3D.FlattenTo2D();
        var point2D = point3D.FlattenTo2D();

        var closest2DPoint = FindNearestPointOnLine2D(start2D, end2D, point2D);
        return closest2DPoint.InflateTo3D();
    }

    public static float FindNearestDistanceOnLine3DP2D(Vector2 start, Vector2 end, Vector2 point)
    {
        var pointOnLine = FindNearestPointOnLine2D(start, end, point);
        return Vector2.Distance(point, pointOnLine);
    }

    public static float FindNearestDistanceOnLine3DP2D(Vector3 start, Vector3 end, Vector3 point)
    {
        var start2D = start.FlattenTo2D();
        var end2D = end.FlattenTo2D();
        var point2D = point.FlattenTo2D();
        return FindNearestDistanceOnLine3DP2D(start2D, end2D, point2D);
    }

    public static bool FasterLineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        Vector2 a = p2 - p1;
        Vector2 b = p3 - p4;
        Vector2 c = p1 - p3;

        float alphaNumerator = b.y * c.x - b.x * c.y;
        float alphaDenominator = a.y * b.x - a.x * b.y;
        float betaNumerator = a.x * c.y - a.y * c.x;
        float betaDenominator = a.y * b.x - a.x * b.y;

        bool doIntersect = true;

        if (alphaDenominator == 0 || betaDenominator == 0)
        {
            doIntersect = false;
        }
        else
        {
            if (alphaDenominator > 0)
            {
                if (alphaNumerator < 0 || alphaNumerator > alphaDenominator)
                {
                    doIntersect = false;
                }
            }
            else if (alphaNumerator > 0 || alphaNumerator < alphaDenominator)
            {
                doIntersect = false;
            }

            if (doIntersect && betaDenominator > 0)
            {
                if (betaNumerator < 0 || betaNumerator > betaDenominator)
                {
                    doIntersect = false;
                }
            }
            else if (betaNumerator > 0 || betaNumerator < betaDenominator)
            {
                doIntersect = false;
            }
        }

        return doIntersect;
    }

    public static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 intersection)
    {
        float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num /*,offset*/;
        float x1lo, x1hi, y1lo, y1hi;
        Ax = p2.x - p1.x;
        Bx = p3.x - p4.x;
        // X bound box test/
        if (Ax < 0)
        {
            x1lo = p2.x;
            x1hi = p1.x;
        }
        else
        {
            x1hi = p2.x;
            x1lo = p1.x;
        }

        if (Bx > 0)
        {
            if (x1hi < p4.x || p3.x < x1lo) return false;
        }
        else
        {
            if (x1hi < p3.x || p4.x < x1lo) return false;
        }

        Ay = p2.y - p1.y;
        By = p3.y - p4.y;
        // Y bound box test//
        if (Ay < 0)
        {
            y1lo = p2.y;
            y1hi = p1.y;
        }
        else
        {
            y1hi = p2.y;
            y1lo = p1.y;
        }

        if (By > 0)
        {
            if (y1hi < p4.y || p3.y < y1lo) return false;
        }
        else
        {
            if (y1hi < p3.y || p4.y < y1lo) return false;
        }

        Cx = p1.x - p3.x;
        Cy = p1.y - p3.y;
        d = By * Cx - Bx * Cy; // alpha numerator//
        f = Ay * Bx - Ax * By; // both denominator//
        // alpha tests//

        if (f > 0)
        {
            if (d < 0 || d > f) return false;
        }
        else
        {
            if (d > 0 || d < f) return false;
        }

        e = Ax * Cy - Ay * Cx; // beta numerator//
        // beta tests //
        if (f > 0)
        {
            if (e < 0 || e > f) return false;
        }
        else
        {
            if (e > 0 || e < f) return false;
        }

        // check if they are parallel
        if (f == 0) return false;
        // compute intersection coordinates //
        num = d * Ax; // numerator //
//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;   // round direction //
//    intersection.x = p1.x + (num+offset) / f;
        intersection.x = p1.x + num / f;
        num = d * Ay;
//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;
//    intersection.y = p1.y + (num+offset) / f;
        intersection.y = p1.y + num / f;
        return true;
    }
}

public static class ExtensionFloat
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
