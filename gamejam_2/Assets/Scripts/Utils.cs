using UnityEngine;

public static class Utils
{
    public static RectTransform GetRectTransform(this GameObject go)
    {
        return (RectTransform) go.transform;
    }

    public static RectTransform GetRectTransform(this MonoBehaviour mb)
    {
        return (RectTransform) mb.transform;
    }
}
