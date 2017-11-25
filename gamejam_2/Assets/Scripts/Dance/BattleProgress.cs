using UnityEngine;
using UnityEngine.UI;

public class BattleProgress : MonoBehaviour
{
    public Image progressImage;

    private void Awake()
    {
        SetDiff(0);
    }

    public void SetDiff(int pointDiff)
    {
        if (pointDiff == 0)
        {
            progressImage.rectTransform.anchoredPosition = new Vector2(-500, 0);
        }
        else
        {
            if (pointDiff > 0)
                gameObject.GetRectTransform().localScale = new Vector3(-1, 1, 1);
            else
                gameObject.GetRectTransform().localScale = new Vector3(1, 1, 1);

            var p = Mathf.Abs(pointDiff);
            progressImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(-280, -17, p / 1000.0f), 0);
        }
    }
}
