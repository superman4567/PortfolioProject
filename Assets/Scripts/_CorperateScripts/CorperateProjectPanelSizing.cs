using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CorperateProjectPanelSizing : MonoBehaviour
{
    [Header("Tweening")]
    [SerializeField] private LayoutAspectRatio aspectScript;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private Button _button;
    [SerializeField] private float tweenDuration = 0.5f;
    [SerializeField] private Ease tweenEase = Ease.InOutSine;
    [SerializeField] private float measurementDelay = 0.1f;

    private RectTransform rectTransform;
    private float collapsedHeight;
    private float expandedHeight;
    private bool isExpanded = false;

    void Awake()
    {
        _button.onClick.AddListener(OnClicked);
        rectTransform = layoutElement.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void Start()
    {
        StartCoroutine(GetCollapseAndExpandValues());
    }

    private void OnLocaleChanged(UnityEngine.Localization.Locale _)
    {
        StartCoroutine(GetCollapseAndExpandValues());
    }

    private IEnumerator GetCollapseAndExpandValues()
    {
        yield return new WaitForSeconds(measurementDelay);

        aspectScript.enabled = false;
        layoutElement.enabled = false;
        verticalLayoutGroup.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        expandedHeight = LayoutUtility.GetPreferredHeight(rectTransform);

        yield return new WaitForSeconds(measurementDelay);

        verticalLayoutGroup.enabled = false;
        aspectScript.enabled = true;
        layoutElement.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        collapsedHeight = LayoutUtility.GetPreferredHeight(rectTransform);

        layoutElement.preferredHeight = collapsedHeight;
    }

    private void OnClicked()
    {
        DOTween.Kill(this);

        if (isExpanded)
            TweenToCollapsed();
        else
            TweenToExpand();
    }

    private void TweenToExpand()
    {
        aspectScript.enabled = false;
        layoutElement.enabled = true;
        verticalLayoutGroup.enabled = true;

        float startH = layoutElement.preferredHeight;
        float endH = expandedHeight;

        DOVirtual.Float(startH, endH, tweenDuration, h =>
        {
            layoutElement.preferredHeight = h;
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        })
        .SetEase(tweenEase)
        .OnComplete(() =>
        {
            layoutElement.enabled = false;
            isExpanded = true;
        })
        .SetId(this);
    }

    private void TweenToCollapsed()
    {
        layoutElement.enabled = true;
        aspectScript.enabled = true;
        verticalLayoutGroup.enabled = false;

        float startH = layoutElement.preferredHeight;
        float endH = collapsedHeight;

        DOVirtual.Float(startH, endH, tweenDuration, h =>
        {
            layoutElement.preferredHeight = h;
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        })
        .SetEase(tweenEase)
        .OnComplete(() =>
        {
            isExpanded = false;
        })
        .SetId(this);
    }
}
