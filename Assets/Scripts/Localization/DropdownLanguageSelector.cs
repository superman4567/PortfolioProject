using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Metadata;

public class DropdownLanguageSelector : MonoBehaviour
{
    [Header("References")]
    public Button toggleButton;
    public RectTransform dropdownContent;
    public GameObject buttonPrefab;

    [Header("Toggle Button Display")]
    public Image toggleFlagImage;
    public TMP_Text toggleLanguageText;

    private bool isDropdownOpen = true;
    private List<GameObject> spawnedButtons = new List<GameObject>();
    private Locale currentLocale;

    private void OnDisable()
    {
        toggleButton.onClick.RemoveListener(ToggleDropdown);
        LocalizationSettings.SelectedLocaleChanged -= UpdateToggleDisplay;
    }

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleDropdown);

        PopulateDropdown();
        ToggleDropdown();

        UpdateToggleDisplay(LocalizationSettings.SelectedLocale);

        LocalizationSettings.SelectedLocaleChanged += UpdateToggleDisplay;
    }

    void ToggleDropdown()
    {
        isDropdownOpen = !isDropdownOpen;
        dropdownContent.gameObject.SetActive(isDropdownOpen);

        // Update button styles to highlight the current locale
        UpdateButtonStyles();
    }

    void PopulateDropdown()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in locales)
        {
            var buttonInstance = Instantiate(buttonPrefab, dropdownContent);
            var buttonScript = buttonInstance.GetComponent<LanguageButton>();

            if (buttonScript != null)
            {
                buttonScript.SetLocale(locale, OnLanguageSelected);
            }

            spawnedButtons.Add(buttonInstance);
        }
    }

    void UpdateToggleDisplay(Locale selectedLocale)
    {
        if (selectedLocale == null) return;

        currentLocale = selectedLocale;

        var metadata = selectedLocale.Metadata.GetMetadata<LocaleMetadata>();
        if (metadata != null)
        {
            toggleFlagImage.sprite = metadata.Flag; // Custom metadata for flags
        }

        toggleLanguageText.text = FormatLocaleCode(selectedLocale.Identifier.Code);
    }

    void UpdateButtonStyles()
    {
        foreach (var button in spawnedButtons)
        {
            var buttonScript = button.GetComponent<LanguageButton>();
            if (buttonScript != null)
            {
                // Check if the button's locale matches the current selected locale
                bool isCurrentLocale = buttonScript.GetLocale() == currentLocale;

                // Change button appearance
                var buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = isCurrentLocale ? Color.red : Color.white;
                }
            }
        }
    }

    void OnLanguageSelected(Locale selectedLocale)
    {
        // Change the active locale and close the dropdown
        LocalizationSettings.SelectedLocale = selectedLocale;
        ToggleDropdown();
    }

    string FormatLocaleCode(string code)
    {
        // Force Chinese locale code to be "ZH"
        if (code.ToLower().StartsWith("zh"))
        {
            return "ZH";
        }

        return code.ToUpper();
    }
}
