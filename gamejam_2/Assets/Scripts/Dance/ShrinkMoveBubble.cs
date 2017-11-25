using System.Collections;
using UnityEngine;

public class ShrinkMoveBubble : BubbleBase
{
    private float _timeToPop = 1.0f; // TODO: pass time

    private Vector2 _startPos;
    private Vector2 _destPos;
    private float _moveTime;

    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return new TimedTapStrategy(_timeToPop);
    }

    public override void Start()
    {
        base.Start();
        GetComponentInChildren<OutlineAnimation>().Init(_timeToPop);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        _startPos = gameObject.GetRectTransform().anchoredPosition;
        while (true)
        {
            var rect = gameObject.GetRectTransform();
            Debug.Log(Time.deltaTime);
            rect.anchoredPosition = Vector2.Lerp(
                _startPos,
                _destPos,
                (Time.time - _startTime) / _moveTime);
            yield return null;
        }
    }

    public void SetMoveData(Vector2 dest, float moveTime)
    {
        _destPos = dest;
        _moveTime = moveTime;
    }
}