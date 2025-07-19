using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnToMainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private float colorTweenDuration = 0.2f;
    [SerializeField] private float scaleTweenDuration = 0.2f;      // duration for scale
    [SerializeField] private Vector3 hoverScale = Vector3.one * 1.05f;

    [SerializeField] private Color activeColorBackground = Color.white;
    [SerializeField] private Color inactiveColorBackground = new Color(0.537f, 0.537f, 0.537f);

    public static Action OnReturnToMainMenu;

    private Vector3 _originalScale;

    private void Start()
    {
        // cache and reset initial scale
        _originalScale = button.transform.localScale;
        button.transform.localScale = _originalScale;

        // set initial color
        button.image.color = inactiveColorBackground;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // color tween
        button.image
              .DOColor(activeColorBackground, colorTweenDuration)
              .SetEase(Ease.InOutQuad)
              .SetUpdate(true);

        // scale tween
        button.transform
              .DOScale(hoverScale, scaleTweenDuration)
              .SetEase(Ease.OutQuad)
              .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // color tween back
        button.image
              .DOColor(inactiveColorBackground, colorTweenDuration)
              .SetEase(Ease.InOutQuad)
              .SetUpdate(true);

        // scale tween back
        button.transform
              .DOScale(_originalScale, scaleTweenDuration)
              .SetEase(Ease.InQuad)
              .SetUpdate(true);
    }

    public void OnClick()
    {
        OnReturnToMainMenu?.Invoke();
    }
}
