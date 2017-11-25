using System;

[Serializable]
public enum BubbleType
{
    Simple,
    Shrink,
    SimpleMove,
    ShrinkMove,
    Swipe
}

[Serializable]
public class BubbleData
{
    public float time;
    public float x;
    public float y;
    public BubbleType type;
    public float toX;
    public float toY;
    public float moveTime;
}

[Serializable]
public class LevelData
{
    public string music;
    public BubbleData[] bubbles;
}
