using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleBehaviourStrategy
{

}

public class SimpleTapBubble : MonoBehaviour, IPointerDownHandler
{
    private BubbleBehaviourStrategy _strategy;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("1asdasd");
    }
}
