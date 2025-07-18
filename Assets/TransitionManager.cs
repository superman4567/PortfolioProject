using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private RectTransform uiPanel;
    [SerializeField] private Image uiImage;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private float pauseDuration = 1f;
    [SerializeField] private PlayableDirector returnToMainDirector;

    private readonly Vector2 hiddenPos = Vector2.zero;
    private readonly Vector2 visiblePos = new Vector2(-3000f, 0f);
    private readonly Vector2 destinationPos = new Vector2(-6204f, 0f);

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += HandleModeChange;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= HandleModeChange;
    }

    private void Start()
    {
        uiPanel.anchoredPosition = hiddenPos;
    }

    private void HandleModeChange(bool isGamingMode)
    {
        if (uiImage != null)
            uiImage.color = isGamingMode ? Color.white : Color.black;

        var seq = DOTween.Sequence();
        seq.Append(uiPanel.DOAnchorPos(visiblePos, slideDuration).SetEase(Ease.OutCubic));
        seq.AppendInterval(pauseDuration);
        seq.Append(uiPanel.DOAnchorPos(destinationPos, slideDuration).SetEase(Ease.InCubic));

        float totalDuration = slideDuration + pauseDuration + slideDuration;
        float callbackTime = totalDuration - 0.25f;
        seq.InsertCallback(callbackTime, HandleEnterTimeline);
    }

    private void HandleEnterTimeline()
    {
        returnToMainDirector.Play();
    }
}
