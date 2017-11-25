using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    public Image progress;

    public void SetProgress(float prog)
    {
        prog = Mathf.Clamp(prog, 0, 1);
        progress.gameObject.GetRectTransform().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            Mathf.Lerp(0, gameObject.GetRectTransform().sizeDelta.x, prog));
    }
}
