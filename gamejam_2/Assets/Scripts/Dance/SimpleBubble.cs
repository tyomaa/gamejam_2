using System;
using UnityEngine;

public class QuickTapStrategy : IBubbleTapBehaviourStrategy
{
    public int HandleTap(float timePassed)
    {
        var basePoints = 100;
        var points = basePoints;
        if (timePassed < 0.2f)
        {
            points = (int)(basePoints * 1.5f);
            Debug.LogError("PERFECT!!! " + timePassed);
        }
        else if (timePassed < 0.5f)
        {
            Debug.LogWarning("GOOD!! " + timePassed);
        }
        else if (timePassed < 0.8f)
        {
            points = (int)(basePoints * 0.5f);
            Debug.LogWarning("NICE " + timePassed);
        }
        else
        {
            points = 0;
            Debug.Log("OOPS!!! " + timePassed);
        }
        return points;
    }
}

public class SimpleBubble : BubbleBase
{
    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return new QuickTapStrategy();
    }
}
