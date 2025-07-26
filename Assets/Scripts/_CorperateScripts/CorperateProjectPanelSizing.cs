using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class CorporateProjectPanelSizing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CorperateProjectCollectionHeight collectionHeight;
    [SerializeField] private RectTransform rectContent;
    [SerializeField] private Button toggleButton;
    [SerializeField] private LocalizeStringEvent toggleButtonText;

    [Header("Animation")]
    [SerializeField] private float tweenDuration = 0.5f;
    [SerializeField] private Ease ease = Ease.InOutSine;

    [Header("Localized Text")]
    [SerializeField] private LocalizedString readMoreText;
    [SerializeField] private LocalizedString readLessText;
    [SerializeField] private LayoutElement layoutElement;
    
    private bool isExpanded;
    private const float CollapsedHeight = 82f;

    private void Awake()
    {
        toggleButton.onClick.AddListener(Toggle);
    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void Start()
    {
        UpdateToggleText();
    }

    private void Toggle()
    {
        isExpanded = !isExpanded;
        float targetHeight = isExpanded ? collectionHeight.ContentHeight : CollapsedHeight;

        TweenHeight(targetHeight);
        UpdateToggleText();
    }

    private void OnLocaleChanged(UnityEngine.Localization.Locale _)
    {
        if (isExpanded)
        {
            TweenHeight(collectionHeight.ContentHeight);
        }
    }

    private void TweenHeight(float targetHeight)
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
        .SetId(this);
    }

    private void UpdateToggleText()
    {
        toggleButtonText.StringReference = isExpanded ? readLessText : readMoreText;
    }
}
