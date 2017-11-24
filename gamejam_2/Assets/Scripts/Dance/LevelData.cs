using System;

[Serializable]
public enum BubbleType
{
    Simple,
    Shrink
}

[Serializable]
public class BubbleData
{
    public float time;
    public float x;
    public float y;
    public BubbleType type;
}

[Serializable]
public class LevelData
{
    public string music;
    public BubbleData[] bubbles;
}
