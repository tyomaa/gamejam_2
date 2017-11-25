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
        DanceManager.Instance.HandleAction(new ActionResult
        {
            successGrade = ActionSuccessGrade.Fail,
            points = 0
        });
        Die();
    }

    private void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var prf = Resources.Load("Prefabs/FlyingText");
        var go = Instantiate(prf) as GameObject;
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.transform.SetParent(transform.parent);

        var diff = Mathf.Abs(Time.time - _startTime);
        var res = TapBehaviour.HandleAction(diff);
        go.GetComponent<FlyingText>().SetText("+" + res.points);
        DanceManager.Instance.HandleAction(res);
        Die();
    }
}