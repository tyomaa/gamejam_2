using System.Collections;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyTimer;

    void Start()
    {
        StartCoroutine(DestroyIn(destroyTimer));
    }

    private IEnumerator DestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        Object.Destroy(this.gameObject);
    }
}
