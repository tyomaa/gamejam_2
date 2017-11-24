using System.Collections;
using UnityEngine;

public class OutlineAnimation : MonoBehaviour
{
    private float _startScale;
    private float _startTime;

    public void Init(float shrinkTime)
    {
        _startScale = transform.localScale.x;
        StartCoroutine(Shrink(shrinkTime));
    }

    IEnumerator Shrink(float shrinkTime)
    {
        _startTime = Time.time;
        while (true)
        {
            var timeDiff = Time.time - _startTime;
            var scale = Mathf.Lerp(_startScale, 1, timeDiff / shrinkTime);
            transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
    }
}
