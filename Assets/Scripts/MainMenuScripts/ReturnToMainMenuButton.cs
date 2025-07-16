using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnToMainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image icon;
    [SerializeField] private float colorTweenDuration = 0.2f;

    [SerializeField] private Color activeColorText = Color.white;
    [SerializeField] private Color inactiveColorText = new Color(0.537f, 0.537f, 0.537f);

    [SerializeField] private Color activeColorBackground = Color.white;
    [SerializeField] private Color inactiveColorbackground = new Color(0.537f, 0.537f, 0.537f);

    public static Action OnReturnToMainMenu;

    private void Start()
    {
        // Initialize the button and text colors to the inactive state
        buttonText.color = inactiveColorText;
        button.image.color = inactiveColorbackground;
        icon.color = inactiveColorText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Tween the text color
        buttonText.DOColor(activeColorText, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);

        icon.DOColor(activeColorText, colorTweenDuration)
           .SetEase(Ease.InOutQuad)
           .SetUpdate(true);

        // Tween the button's image color
        button.image.DOColor(activeColorBackground, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Tween the text color back to inactive
        buttonText.DOColor(inactiveColorText, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);

        icon.DOColor(inactiveColorText, colorTweenDuration)
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
