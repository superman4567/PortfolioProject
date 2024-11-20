using UnityEngine;
using UnityEngine.UI;

public class TransitionViewUXUI : MonoBehaviour
{
    public delegate void BackToMainMenuButtonEvent();
    public event BackToMainMenuButtonEvent OnBackToMainMenuButtonPressed;

    [SerializeField] private Button returnToMainMenuButton;

    private void OnEnable()
    {
        SetUpButton(returnToMainMenuButton);
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
}
