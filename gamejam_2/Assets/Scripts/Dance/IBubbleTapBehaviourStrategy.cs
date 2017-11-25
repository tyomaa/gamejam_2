using System;

public interface IBubbleTapBehaviourStrategy
{
    ActionResult HandleAction(float timePassed);
}

public enum ActionSuccessGrade
{
    Fail,
    Bad,
    Ok,
    Good,
    Perfect
}

public class ActionResult
{
    public int points;
    public ActionSuccessGrade successGrade;
}
