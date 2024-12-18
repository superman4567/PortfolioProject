using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropdown; // Reference to the TMP Dropdown
    [SerializeField] private Image flagImage; // Reference to the Image for the flag
    [SerializeField] private TextMeshProUGUI abbreviationText; // Reference to the TextMeshProUGUI for abbreviation
    [SerializeField] private Sprite[] flagSprites; // Array of flag sprites
    [SerializeField] private string[] languageAbbreviations; // Array of language abbreviations (e.g., "NL", "DE")

    private void Start()
    {
        // Initialize the dropdown with all available languages
        PopulateDropdown();

        // Add listener to handle language changes
        languageDropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    private void PopulateDropdown()
    {
        languageDropdown.options.Clear(); // Clear existing options
        var locales = LocalizationSettings.AvailableLocales.Locales; // Get available locales

        for (int i = 0; i < locales.Count; i++)
        {
            string displayName = locales[i].Identifier.CultureInfo.DisplayName;
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(displayName));
        }

        // Set initial dropdown value
        languageDropdown.value = LocalizationSettings.SelectedLocale != null ?
            LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale) : 0;
        languageDropdown.RefreshShownValue(); // Ensure the correct value is displayed
    }

    private void OnLanguageSelected(int index)
    {
        // Update locale
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = selectedLocale;

        // Update UI for flag and abbreviation
        flagImage.sprite = flagSprites[index]; // Set flag sprite
        abbreviationText.text = languageAbbreviations[index]; // Set abbreviation text
    }
}
