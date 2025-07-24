using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CorporateProjectPanelSizing : MonoBehaviour
{
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] RectTransform rectContent;
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
        StartCoroutine(ReMeasureAndToggle());
        DOTween.Kill(this);
        float target = isExpanded ? collapsedHeight : expandedHeight;
        TweenHeight(target, () => isExpanded = !isExpanded);
    }

    private void OnLocaleChanged(UnityEngine.Localization.Locale _)
    {
        StartCoroutine(ReMeasureAndToggle());
    }

    private IEnumerator ReMeasureAndToggle()
    {
        yield return null;

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectContent);
        float newExpanded = rectContent.rect.height;

        expandedHeight = newExpanded;
        float target = isExpanded ? collapsedHeight : expandedHeight;

        DOTween.Kill(this);
        TweenHeight(target, () => isExpanded = !isExpanded);
    }

    private void TweenHeight(float targetHeight, TweenCallback onComplete)
    {
        DOTween.To(
            () => layoutElement.preferredHeight,
            h =>
            {
                layoutElement.preferredHeight = h;
                LayoutRebuilder.MarkLayoutForRebuild(rectContent);
            },
            targetHeight,
            tweenDuration
        )
        .SetEase(ease)
        .OnComplete(onComplete ?? (() => { }))
        .SetId(this);
    }
}
