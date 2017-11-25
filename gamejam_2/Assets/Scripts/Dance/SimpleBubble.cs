using System;
using UnityEngine;

public class QuickTapStrategy : IBubbleTapBehaviourStrategy
{
    public ActionResult HandleAction(float timePassed)
    {
        if (timePassed < 0.2f)
        {
            Debug.LogError("PERFECT!!! " + timePassed);
            return new ActionResult
            {
                successGrade = ActionSuccessGrade.Perfect,
                points = 150
            };
        }
        else if (timePassed < 0.5f)
        {
            Debug.LogWarning("GOOD!! " + timePassed);
            return new ActionResult
            {
                successGrade = ActionSuccessGrade.Good,
                points = 100
            };
        }
        else if (timePassed < 0.8f)
        {
            Debug.LogWarning("NICE " + timePassed);
            return new ActionResult
            {
                successGrade = ActionSuccessGrade.Ok,
                points = 50
            };
        }
        else
        {
            Debug.Log("OOPS!!! " + timePassed);
            return new ActionResult
            {
                successGrade = ActionSuccessGrade.Bad,
                points = 10
            };
        }
    }
}

public class SimpleBubble : BubbleBase
{
    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return new QuickTapStrategy();
    }
}
