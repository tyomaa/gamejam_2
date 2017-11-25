using UnityEngine;

public static class Utils
{
    public static RectTransform GetRectTransform(this GameObject go)
    {
        return (RectTransform) go.transform;
    }
}
