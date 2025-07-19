using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class UXUIProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EnumUXUIProjects projectEnum;
    [SerializeField] private Button button;
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;

    private Vector3 originalScale;
    private Tween currentTween;

    public static event Action<EnumUXUIProjects> OnProjectButtonClicked;

    private void Awake()
    {
        // Cache the original scale
        originalScale = transform.localScale;
        button.onClick.AddListener(() => OnProjectButtonClicked?.Invoke(projectEnum));
    }

    private void OnDestroy()
    {
        currentTween?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform.DOScale(originalScale * hoverScale, tweenDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform.DOScale(originalScale, tweenDuration).SetEase(Ease.OutBack);
    }
}
