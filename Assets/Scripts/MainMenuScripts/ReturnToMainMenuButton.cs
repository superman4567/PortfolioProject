using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnToMainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private float colorTweenDuration = 0.2f;

    [SerializeField] private Color activeColorBackground = Color.white;
    [SerializeField] private Color inactiveColorbackground = new Color(0.537f, 0.537f, 0.537f);

    public static Action OnReturnToMainMenu;

    private void Start()
    {
        button.image.color = inactiveColorbackground;
        icon.color = activeColorBackground;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.DOColor(activeColorBackground, colorTweenDuration)
           .SetEase(Ease.InOutQuad)
           .SetUpdate(true);

        // Tween the button's image color
        button.image.DOColor(activeColorBackground, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.DOColor(inactiveColorbackground, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);

        // Tween the button's image color back to inactive
        button.image.DOColor(inactiveColorbackground, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }

    public void OnClick()
    {
        OnReturnToMainMenu?.Invoke();
    }
}
