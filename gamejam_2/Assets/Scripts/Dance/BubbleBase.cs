using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BubbleBase : MonoBehaviour, IPointerDownHandler
{
    protected abstract IBubbleTapBehaviourStrategy CreateTapBehaviour();

    private IBubbleTapBehaviourStrategy _tapBehaviour;
    protected IBubbleTapBehaviourStrategy TapBehaviour
    {
        get
        {
            if (_tapBehaviour == null)
                _tapBehaviour = CreateTapBehaviour();
            return _tapBehaviour;
        }
    }

    private float _timeToDie = 2.0f;
    private float _startTime;

    public virtual void Start()
    {
        _startTime = Time.time;
        StartCoroutine(DieRoutined());
    }

    private IEnumerator DieRoutined()
    {
        yield return new WaitForSeconds(_timeToDie);
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
        var points = TapBehaviour.HandleTap(diff);
        go.GetComponent<FlyingText>().SetText("+" + points);
        Die();
    }
}