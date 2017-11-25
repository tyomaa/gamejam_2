using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlyingText : MonoBehaviour
{
    public GameObject innerObject;
    public CanvasGroup canvasGroup;

    public AnimationCurve alphaCurve;
    public AnimationCurve positionCurve;

    private float _startTime;

    void Start()
	{
	    _startTime = Time.time;
	    var maxTime = Mathf.Max(
	        alphaCurve.keys.Last().time,
	        positionCurve.keys.Last().time);

        StartCoroutine(UpdateAlpha());
	    StartCoroutine(UpdatePosition());
	    StartCoroutine(Die(maxTime));
	}

    public void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private IEnumerator UpdatePosition()
    {
        while (true)
        {
            var rect = innerObject.transform as RectTransform;
            var pos = rect.anchoredPosition;
            rect.anchoredPosition = new Vector3(pos.x, positionCurve.Evaluate(Time.time - _startTime));
            yield return null;
        }
    }

    private IEnumerator UpdateAlpha()
    {
        while (true)
        {
            canvasGroup.alpha = alphaCurve.Evaluate(Time.time - _startTime);
            yield return null;
        }
    }

    public static FlyingText Spawn(Vector2 pos, float delay = 0)
    {
        var prf = Resources.Load("Prefabs/FlyingText");
        var go = Instantiate(prf) as GameObject;
        go.transform.SetParent(DanceManager.Instance.MainCanvas);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.GetRectTransform().anchoredPosition = pos;

        var ft = go.GetComponent<FlyingText>();
        if (delay > 0)
        {
            ft.Delay(delay);
        }
        return ft;
    }

    public void Delay(float delay)
    {
        gameObject.SetActive(false);
        DanceManager.Instance.StartCoroutine(EnableIn(delay));
    }

    private IEnumerator EnableIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
    }
}
