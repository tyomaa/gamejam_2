using UnityEngine;

public class TimedTapStrategy : IBubbleTapBehaviourStrategy
{
    private readonly float _timeToTap;

    public TimedTapStrategy(float timeToTap)
    {
        _timeToTap = timeToTap;
    }

    public ActionResult HandleAction(float timePassed)
    {
        var d = timePassed - _timeToTap;
        var diff = Mathf.Abs(d);
        if (diff < 0.1f)
        {
            Debug.LogError("PERFECT!!! " + d);
            return new ActionResult()
            {
                successGrade = ActionSuccessGrade.Perfect,
                points = 200
            };
        }
        else if (diff < 0.3f)
        {
            Debug.LogWarning("GOOD!! " + d);
            return new ActionResult()
            {
                successGrade = ActionSuccessGrade.Good,
                points = 100
            };
        }
        else if (diff < 0.5f)
        {
            Debug.LogWarning("NICE " + d);
            return new ActionResult()
            {
                successGrade = ActionSuccessGrade.Ok,
                points = 50
            };
        }
        else
        {
            Debug.Log("OOPS!!!" + d);
            return new ActionResult()
            {
                successGrade = ActionSuccessGrade.Bad,
                points = 10
            };
        }
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