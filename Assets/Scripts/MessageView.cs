using UnityEngine;
using TMPro;
using DG.Tweening;

public class MessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Show(string message)
    {
        text.text = message;
        canvasGroup.alpha = 0;

        canvasGroup.DOFade(1f, 0.2f);

        canvasGroup.DOFade(0f, 0.3f)
            .SetDelay(2f)
            .OnComplete(() => Destroy(gameObject));
    }
}
