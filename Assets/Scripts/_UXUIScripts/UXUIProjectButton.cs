using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UXUIProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EnumUXUIProjects projectEnum;

    [Space]
    [Header("Button Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonImage;

    [Space]
    [Header("Hover Effects")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;
    [SerializeField] private Color normalColor = new Color(0xBA / 255f, 0xBA / 255f, 0xBA / 255f);
    [SerializeField] private Color hoverColor = Color.white;

    private Vector3 originalScale;
    private Tween scaleTween;
    private Tween colorTween;

    public static event Action<EnumUXUIProjects> OnProjectButtonClicked;
    public static event Action<EnumUXUIProjects> OnProjectButtonHovered;

    private void Awake()
    {
        originalScale = transform.localScale;
        buttonText.text = projectEnum.ToString();
        button.onClick.AddListener(() => OnProjectButtonClicked?.Invoke(projectEnum));
    }

    private void OnDestroy()
    {
        scaleTween?.Kill();
        colorTween?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // scale up
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale * hoverScale, tweenDuration)
            .SetEase(Ease.OutBack);

        // color to white
        colorTween?.Kill();
        colorTween = buttonImage
            .DOColor(hoverColor, tweenDuration)
            .SetEase(Ease.OutQuad);

        ChangeDisplayedProject();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // scale back
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale, tweenDuration)
            .SetEase(Ease.OutBack);

        colorTween = buttonImage
            .DOColor(normalColor, tweenDuration)
            .SetEase(Ease.OutQuad);
    }

    private void ChangeDisplayedProject()
    {
        OnProjectButtonHovered?.Invoke(projectEnum);
    }
}
