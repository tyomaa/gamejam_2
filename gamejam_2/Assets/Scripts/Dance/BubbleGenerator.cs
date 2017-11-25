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

        _bubbles = new Stack<BubbleData>(bubbles.OrderByDescending(b => b.time));
        StartCoroutine(SpawnBubbles());
    }

    private IEnumerator SpawnBubbles()
    {
        _startTime = Time.time;
        while (true)
        {
            var timeDiff = Time.time - _startTime;
            //Debug.Log(timeDiff);
            while (_bubbles.Count > 0 && _bubbles.Peek().time <= timeDiff)
            {
                var bubble = _bubbles.Pop();
                Debug.Log("instantiating " + bubble.type + " at " + bubble.time);
                var go = Instantiate(bubble);
                switch (bubble.type)
                {
                    case BubbleType.SimpleMove:
                    {
                        go.GetComponent<SimpleMoveBubble>().SetMoveData(new Vector2(bubble.toX, bubble.toY), bubble.moveTime);
                        break;
                    }
                    case BubbleType.ShrinkMove:
                    {
                        go.GetComponent<ShrinkMoveBubble>().SetMoveData(new Vector2(bubble.toX, bubble.toY), bubble.moveTime);
                        break;
                    }
                    case BubbleType.Swipe:
                    {
                        go.GetComponent<SwipeBubble>().SetDir(new Vector2(bubble.toX, bubble.toY));
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
            yield return null;
        }
    }

    private GameObject Instantiate(BubbleData bubble)
    {
        var prf = Resources.Load(GetPrefabName(bubble.type));
        var go = Object.Instantiate(prf) as GameObject;
        go.transform.SetParent(GameObject.Find("MainCanvas").transform);
        var rect = (go.transform as RectTransform);
        rect.transform.localPosition = Vector3.zero;
        rect.anchoredPosition = new Vector3(bubble.x, bubble.y, 0);
        rect.localScale = Vector3.one;
        return go;
    }

    private string GetPrefabName(BubbleType type)
    {
        return string.Format("Prefabs/Bubbles/{0}Bubble", type);
    }
}
