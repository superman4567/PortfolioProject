using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private const float normalFontSize = 82f;
    private const float largeFontSize = 112f;

    private readonly Color activeColor = Color.white;
    private readonly Color inactiveColor = new Color(0.537f, 0.537f, 0.537f);

    [SerializeField] private float fontSizeTweenDuration = 0.2f; // Duration for font size change
    [SerializeField] private float colorTweenDuration = 0.2f;  // Duration for color change

    private void Start()
    {
        // Set the initial state of the button
        buttonText.fontSize = normalFontSize;
        buttonText.color = inactiveColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animate font size and color with DOTween using custom durations
        DOTween.To(() => buttonText.fontSize, x => buttonText.fontSize = x, largeFontSize, fontSizeTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);

        buttonText.DOColor(activeColor, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Animate font size and color back to normal when pointer exits using custom durations
        DOTween.To(() => buttonText.fontSize, x => buttonText.fontSize = x, normalFontSize, fontSizeTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);

        buttonText.DOColor(inactiveColor, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }
}
