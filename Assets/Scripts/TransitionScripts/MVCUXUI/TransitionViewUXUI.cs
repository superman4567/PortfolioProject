using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionViewUXUI : MonoBehaviour
{
    public delegate void BackToMainMenuButtonEvent();
    public event BackToMainMenuButtonEvent OnBackToMainMenuButtonPressed;

    [SerializeField] private Button returnToMainMenuButton;
    [SerializeField] private CanvasGroup canvasgroup;

    private void OnEnable()
    {
        SetUpButton(returnToMainMenuButton);

        canvasgroup.interactable = false;
        canvasgroup.alpha = 0f;
    }

    private void OnDisable()
    {
        returnToMainMenuButton.onClick.RemoveAllListeners();
    }

    private void SetUpButton(Button button)
    {
        button.onClick.AddListener(() => HandleButtonPressed());
    }

    private void HandleButtonPressed()
    {
        OnBackToMainMenuButtonPressed?.Invoke();
    }

    public void ShowUI()
    {
        float duration = 0.4f;
        canvasgroup.alpha = 0;
        canvasgroup.DOFade(1, duration).SetEase(Ease.InOutSine);

        canvasgroup.interactable = true;
    }

    public void HideUI()
    {
        float duration = 0.4f;
        canvasgroup.alpha = 1;
        canvasgroup.DOFade(0, duration).SetEase(Ease.InOutSine);

        canvasgroup.interactable = false;
    }
}
