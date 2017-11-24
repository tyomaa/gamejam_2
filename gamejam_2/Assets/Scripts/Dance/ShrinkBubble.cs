using UnityEngine;

public class TimedTapStrategy : IBubbleTapBehaviourStrategy
{
    private readonly float _timeToTap;

    public TimedTapStrategy(float timeToTap)
    {
        _timeToTap = timeToTap;
    }

    public int HandleTap(float timePassed)
    {
        var basePoints = 200;
        var points = basePoints;

        var d = timePassed - _timeToTap;
        var diff = Mathf.Abs(d);
        if (diff < 0.1f)
        {
            Debug.LogError("PERFECT!!! " + d);
            points = (int)(basePoints * 1.2f);
        }
        else if (diff < 0.3f)
        {
            Debug.LogWarning("GOOD!! " + d);
        }
        else if (diff < 0.5f)
        {
            Debug.LogWarning("NICE " + d);
            points = (int)(basePoints * 0.5f);
        }
        else
        {
            Debug.Log("OOPS!!!" + d);
            points = 0;
        }
        return points;
    }
}

public class ShrinkBubble : BubbleBase
{
    private float _timeToPop = 1.0f; // TODO: pass time

    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return new TimedTapStrategy(_timeToPop);
    }

    public override void Start()
    {
        base.Start();
        GetComponentInChildren<OutlineAnimation>().Init(_timeToPop);
    }
}