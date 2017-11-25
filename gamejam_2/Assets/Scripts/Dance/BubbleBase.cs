using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BubbleBase : MonoBehaviour, IPointerDownHandler
{
    private IBubbleTapBehaviourStrategy _tapBehaviour;

    private float _timeToDie = 2.0f;
    protected float _startTime;

    protected abstract IBubbleTapBehaviourStrategy CreateTapBehaviour();

    protected IBubbleTapBehaviourStrategy TapBehaviour
    {
        get
        {
            if (_tapBehaviour == null)
                _tapBehaviour = CreateTapBehaviour();
            return _tapBehaviour;
        }
    }

    public virtual void Start()
    {
        _startTime = Time.time;
        StartCoroutine(DieRoutined());
    }

    private IEnumerator DieRoutined()
    {
        yield return new WaitForSeconds(_timeToDie);
        DanceManager.Instance.ProcessAction(new ActionResult
        {
            successGrade = ActionSuccessGrade.Fail,
            points = 0
        },
        gameObject.GetRectTransform().anchoredPosition);
        Die();
    }

    protected void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        var diff = Mathf.Abs(Time.time - _startTime);
        var res = TapBehaviour.HandleAction(diff);
        DanceManager.Instance.ProcessAction(res, gameObject.GetRectTransform().anchoredPosition);
        Die();
    }
}