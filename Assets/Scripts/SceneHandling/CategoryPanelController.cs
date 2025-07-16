using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class CategoryPanelController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup panel;

    [Header("Entrance Animation")]
    [SerializeField] private PlayableDirector entranceDirector;

    private void OnEnable()
    {
        if (entranceDirector != null)
            entranceDirector.stopped += OnTimelineFinished;
    }

    private void OnDisable()
    {
        if (entranceDirector != null)
            entranceDirector.stopped -= OnTimelineFinished;
    }

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

    private void OnTimelineFinished(PlayableDirector director)
    {
        Show();
    }
}
