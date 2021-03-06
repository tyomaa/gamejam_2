﻿using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeBubble : BubbleBase, IDragHandler, IEndDragHandler
{
    public GameObject arrowObject;
    private Vector2 _dirvect;

    public void SetDir(Vector2 direction)
    {
        _dirvect = direction.normalized * 150.0f;
        var angle = GetAngle(new Vector2(10, 0), _dirvect);
        arrowObject.GetRectTransform().rotation = Quaternion.Euler(0, 0, angle);
    }

    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }

    protected override IBubbleTapBehaviourStrategy CreateTapBehaviour()
    {
        return null;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        var point = Utils.ConvertInputPos(eventData.position);
        var diff = point - gameObject.GetRectTransform().anchoredPosition;
        var angle = Vector2.Angle(_dirvect, diff);
        var badOffset = diff.magnitude * Mathf.Sin(angle * Mathf.PI / 180);
        var dot = Vector2.Dot(diff, _dirvect);
        if (diff.magnitude >= 120 && badOffset < 80 && dot > 0)
        {
            DanceManager.Instance.ProcessAction(
                new ActionResult
                {
                    successGrade = ActionSuccessGrade.Perfect,
                    points = 100
                },
                point);
            Die();
            return;
        }

        if (badOffset > 70 || (diff.magnitude >= 50 && dot < 0))
        {
            DanceManager.Instance.ProcessAction(
                new ActionResult
                {
                    successGrade = ActionSuccessGrade.Fail,
                    points = 0
                },
                point);
            Die();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DanceManager.Instance.ProcessAction(
            new ActionResult
            {
                successGrade = ActionSuccessGrade.Fail,
                points = 0
            },
            Utils.ConvertInputPos(eventData.position));
        Die();
    }
}
