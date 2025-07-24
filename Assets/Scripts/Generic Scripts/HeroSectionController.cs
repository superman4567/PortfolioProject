using UnityEngine;
using DG.Tweening;
using System.Collections;

public class HeroSectionController : MonoBehaviour
{
    [Header("Hero Elements")]
    [SerializeField] private RectTransform iamObject;
    [SerializeField] private CanvasGroup iamCanvasGroup;

    [SerializeField] private RectTransform nathanObject;
    [SerializeField] private CanvasGroup nathanCanvasGroup;

    [SerializeField] private RectTransform imageObject;
    [SerializeField] private CanvasGroup imageCanvasGroup;

    [Header("Tween Settings")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float moveOffset = 150f;
    [SerializeField] private float delayBetween = 0.3f;
    [SerializeField] private float initialDelay = 2f;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += ModeSelected_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= ModeSelected_Callback;
    }

    private void Start()
    {
        // Ensure all hero elements are initially invisible
        iamCanvasGroup.alpha = 0;
        nathanCanvasGroup.alpha = 0;
        imageCanvasGroup.alpha = 0;
    }

    private void ModeSelected_Callback(bool isCorperate)
    {
        if (!isCorperate)
            return;

        StartCoroutine(AnimateHeroElements());
    }

    private IEnumerator AnimateHeroElements()
    {
        yield return new WaitForSeconds(initialDelay);

        // Store original positions
        Vector2 iamStartPos = iamObject.anchoredPosition;
        Vector2 nathanStartPos = nathanObject.anchoredPosition;
        Vector2 imageStartPos = imageObject.anchoredPosition;

        // Set starting states (invisible and offset)
        iamCanvasGroup.alpha = 0;
        iamObject.anchoredPosition = iamStartPos + Vector2.left * moveOffset;

        nathanCanvasGroup.alpha = 0;
        nathanObject.anchoredPosition = nathanStartPos + Vector2.down * moveOffset;

        imageCanvasGroup.alpha = 0;
        imageObject.anchoredPosition = imageStartPos + Vector2.down * moveOffset;

        // Sequence animations
        Sequence sequence = DOTween.Sequence();

        sequence.Append(iamCanvasGroup.DOFade(1f, fadeDuration));
        sequence.Join(iamObject.DOAnchorPos(iamStartPos, moveDuration).SetEase(Ease.OutQuad));

        sequence.AppendInterval(delayBetween);

        sequence.Append(nathanCanvasGroup.DOFade(1f, fadeDuration));
        sequence.Join(nathanObject.DOAnchorPos(nathanStartPos, moveDuration).SetEase(Ease.OutQuad));

        sequence.AppendInterval(delayBetween);

        sequence.Append(imageCanvasGroup.DOFade(1f, fadeDuration));
        sequence.Join(imageObject.DOAnchorPos(imageStartPos, moveDuration).SetEase(Ease.OutQuad));
    }
}
