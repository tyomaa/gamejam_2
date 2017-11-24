using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleBehaviourStrategy
{

}

public class SimpleTapBubble : MonoBehaviour, IPointerDownHandler
{
    private float _timeToPop = 1.0f;
    private float _timeToDie = 2.0f;

    private float _startTime;

    public void Start()
    {
        _startTime = Time.time;
        //StartCoroutine(DieRoutined());
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
        var diff = Mathf.Abs(Time.time - _startTime);

        var prf = Resources.Load("Prefabs/FlyingText");
        var go = Object.Instantiate(prf) as GameObject;
        go.GetComponent<FlyingText>().SetText("+99");
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.transform.SetParent(transform.parent);
    }
}
