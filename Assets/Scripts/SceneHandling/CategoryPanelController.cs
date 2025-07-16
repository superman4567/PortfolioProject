using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class CategoryPanelController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup panel;

    public void Show()
    {
        panel.alpha = 0f;
        panel.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .OnStart(() =>
            {
                panel.interactable = true;
                panel.blocksRaycasts = true;
            });
    }

    public void Hide()
    {
        panel.DOFade(0f, 0.2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                panel.interactable = false;
                panel.blocksRaycasts = false;
            });
    }
}
