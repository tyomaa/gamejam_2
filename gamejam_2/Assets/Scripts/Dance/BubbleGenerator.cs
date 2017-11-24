using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class BubbleGenerator : MonoBehaviour
{
    private float _startTime;

    private Stack<BubbleData> _bubbles;

    public void Init(IList<BubbleData> bubbles)
    {
        foreach (var bubbleData in bubbles)
        {
            Debug.Log(bubbleData.time + " / " + bubbleData.x + "|" + bubbleData.y);
        }

        _bubbles = new Stack<BubbleData>(bubbles.OrderBy(b => b.time));
        StartCoroutine(SpawnBubbles());
    }

    private IEnumerator SpawnBubbles()
    {
        _startTime = Time.time;
        while (true)
        {
            var timeDiff = Time.time - _startTime;
            while (_bubbles.Count > 0 && _bubbles.Peek().time <= timeDiff)
            {
                var bubble = _bubbles.Pop();
                switch (bubble.type)
                {
                    case BubbleType.Simple:
                    {
                        var prf = Resources.Load("Prefabs/Bubbles/SimpleBubble");
                        var go = Object.Instantiate(prf) as GameObject;
                        go.transform.SetParent(GameObject.Find("MainCanvas").transform);
                        var rect = (go.transform as RectTransform);
                        rect.anchoredPosition = new Vector2(bubble.x, bubble.y);
                        rect.localScale = Vector3.one;
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
        }
            yield return null;
        }
    }
}
