using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class UXUIProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private EnumUXUIProjects projectEnum;
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;

    private Vector3 originalScale;
    private Tween currentTween;

    public static event Action<EnumUXUIProjects> OnProjectButtonClicked;

    private void Awake()
    {
        // Cache the original scale
        originalScale = transform.localScale;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnProjectButtonClicked?.Invoke(projectEnum);
    }
}
