using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CorporateProjectPanelSizing : MonoBehaviour
{
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] RectTransform rectContent, rectScroll;
    [SerializeField] Button toggleButton;
    [SerializeField] float tweenDuration = 0.5f;
    [SerializeField] Ease ease = Ease.InOutSine;

    float collapsedHeight = 82f;
    float expandedHeight;
    bool isExpanded;

    void Awake()
    {
        toggleButton.onClick.AddListener(Toggle);
    }

    void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    void Start()
    {
        StartCoroutine(Measure());
    }

    private IEnumerator Measure()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectContent);
        expandedHeight = rectContent.rect.height;
        layoutElement.preferredHeight = collapsedHeight;
    }

    private void Toggle()
    {
        DOTween.Kill(this);
        float target = isExpanded ? collapsedHeight : expandedHeight;
        TweenHeight(target, () => isExpanded = !isExpanded);
    }

    private void OnLocaleChanged(UnityEngine.Localization.Locale _)
    {
        StartCoroutine(ReMeasureAndTween());
    }

    private IEnumerator ReMeasureAndTween()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectContent);
        float newExpanded = rectContent.rect.height;
        expandedHeight = newExpanded;
        float target = isExpanded ? newExpanded : collapsedHeight;
        DOTween.Kill(this);
        TweenHeight(target, null);
    }

    private void TweenHeight(float targetHeight, TweenCallback onComplete)
    {
        DOTween.To(
            () => layoutElement.preferredHeight,
            h =>
            {
                layoutElement.preferredHeight = h;
                LayoutRebuilder.MarkLayoutForRebuild(rectScroll);
            },
            targetHeight,
            tweenDuration
        )
        .SetEase(ease)
        .OnComplete(onComplete ?? (() => { }))
        .SetId(this);
    }
}
