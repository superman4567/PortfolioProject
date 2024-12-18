using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TransitionViewMainMenu : MonoBehaviour
{
    public delegate void MainMenuButtonEvent(EnumMainMenuChoices choice);
    public event MainMenuButtonEvent OnMainMenuButtonPressed;

    [SerializeField] private Button uxuiButton;
    [SerializeField] private Button threeDArtButton;
    [SerializeField] private Button programmingButton;
    [SerializeField] private Button vfxButton;

    [SerializeField] private Button ProfileButton;
    [SerializeField] private Button contactButton;


    private Button[] buttons;

    private void OnEnable()
    {
        buttons = new Button[] { uxuiButton, threeDArtButton, programmingButton, vfxButton };

        // Set up projectButtons' hover functionality
        SetUpButton(uxuiButton, EnumMainMenuChoices.UXUI);
        SetUpButton(threeDArtButton, EnumMainMenuChoices.ThreeDArt);
        SetUpButton(programmingButton, EnumMainMenuChoices.Programming);
        SetUpButton(vfxButton, EnumMainMenuChoices.VFX);
        SetUpButton(vfxButton, EnumMainMenuChoices.profile);
        SetUpButton(vfxButton, EnumMainMenuChoices.contact);
    }

    private void OnDisable()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }


    private void SetUpButton(Button button, EnumMainMenuChoices choice)
    {
        button.onClick.AddListener(() => HandleButtonPressed(choice));
    }

    private void HandleButtonPressed(EnumMainMenuChoices choice)
    {
        OnMainMenuButtonPressed?.Invoke(choice);
    }
}

