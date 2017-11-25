using System;
using System.Collections;
using UnityEngine;

public class SimpleMoveBubble : BubbleBase
{
    private Vector2 _startPos;
    private Vector2 _destPos;
    private float _moveTime;

    public override void Start()
    {
        base.Start();
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

    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return new QuickTapStrategy();
    }

    public void SetMoveData(Vector2 dest, float moveTime)
    {
        _destPos = dest;
        _moveTime = moveTime;
    }
}
