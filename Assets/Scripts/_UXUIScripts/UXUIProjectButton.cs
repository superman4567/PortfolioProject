using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UXUIProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EnumUXUIProjects projectEnum;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttontext;
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;

    private Vector3 originalScale;
    private Tween currentTween;

    public static event Action<EnumUXUIProjects> OnProjectButtonClicked;
    public static event Action<EnumUXUIProjects> OnProjectButtonHovered;

    private void Awake()
    {
        originalScale = transform.localScale;
        buttontext.text = projectEnum.ToString();
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
        ChangeDisplayedProject();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform.DOScale(originalScale, tweenDuration).SetEase(Ease.OutBack);
    }

    private void ChangeDisplayedProject()
    {
        OnProjectButtonHovered?.Invoke(projectEnum);
    }
}
