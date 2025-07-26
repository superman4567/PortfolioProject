using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CorporateProjectPanelSizing : MonoBehaviour
{
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private RectTransform rectContent;
    [SerializeField] private Button toggleButton;
    [SerializeField] private LocalizeStringEvent toggleButtonText;
    [SerializeField] private float tweenDuration = 0.5f;
    [SerializeField] Ease ease = Ease.InOutSine;

    [Space]

    [SerializeField] private LocalizedString readmoreText;
    [SerializeField] private LocalizedString readlessText;

    readonly float collapsedHeight = 82f;

    private float expandedHeight;
    private bool isExpanded;

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
        toggleButtonText.StringReference = readmoreText;
        StartCoroutine(Measure());
    }

    private IEnumerator Measure()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectContent);
        expandedHeight = rectContent.rect.height;
       
        layoutElement.preferredHeight = collapsedHeight;

        StartCoroutine(ReMeasureAndToggle());
    }

    private void Toggle()
    {
        ToggleTextExpansionButtonText();

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

        if (!isExpanded)
            yield break;

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

    private void ToggleTextExpansionButtonText()
    {
        if (isExpanded)
        {
            toggleButtonText.StringReference = readmoreText;
        }
        else
        {
            toggleButtonText.StringReference = readlessText;
        }
    }
}
