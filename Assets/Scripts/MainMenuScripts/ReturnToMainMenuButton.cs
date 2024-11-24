using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnToMainMenuButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private float colorTweenDuration = 0.2f;

    private readonly Color activeColor = Color.white;
    private readonly Color inactiveColor = new Color(0.537f, 0.537f, 0.537f);

    private void Start()
    {
        buttonText.color = inactiveColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.DOColor(activeColor, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.DOColor(inactiveColor, colorTweenDuration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true);
    }
}
