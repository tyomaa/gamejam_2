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

    public static Vector2 ConvertInputPos(Vector2 inputPos)
    {
        return new Vector2(
            inputPos.x * (1280.0f / Screen.width),
            inputPos.y * (800.0f / Screen.height));
    }
}
