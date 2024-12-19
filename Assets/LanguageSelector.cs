using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class LocaleAbbreviation
{
    public Locale locale;
    public string abbreviation;
    public Sprite flag;
}

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public List<LocaleAbbreviation> localeAbbreviations = new();

    void Start()
    {
        languageDropdown.ClearOptions();
        List<string> languageOptions = new();

        foreach (var localeAbbr in localeAbbreviations)
        {
            languageOptions.Add(localeAbbr.abbreviation);
        }

        languageDropdown.AddOptions(languageOptions);
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        StartCoroutine(WaitForDropdownClick());
    }

    private System.Collections.IEnumerator WaitForDropdownClick()
    {
        while (true)
        {
            if (languageDropdown.IsExpanded)
            {
                AssignFlagsToDropdownOptions();
                yield break;
            }

            yield return null;
        }
    }

    private void AssignFlagsToDropdownOptions()
    {
        Transform dropdownList = languageDropdown.transform.Find("Dropdown List/Viewport/Content");

        for (int i = 1; i < dropdownList.childCount; i++)
        {
            Transform optionItem = dropdownList.GetChild(i);
            Transform flagTransform = optionItem.Find("Flag");

            if (flagTransform != null)
            {
                Image flagImage = flagTransform.GetComponent<Image>();

                if (flagImage != null)
                {
                    flagImage.sprite = localeAbbreviations[i - 1].flag;
                }
            }
        }
    }

    private void OnLanguageChanged(int index)
    {
        Locale selectedLocale = localeAbbreviations[index].locale;
        LocalizationSettings.SelectedLocale = selectedLocale;
    }
}



//using UnityEngine;
//using TMPro;
//using UnityEngine.Localization;
//using UnityEngine.Localization.Settings;
//using UnityEngine.UI;
//using System.Collections.Generic;

//[System.Serializable]
//public class LocaleAbbreviation
//{
//    public Locale locale;
//    public string abbreviation;
//    public Sprite flag;
//}

//public class LanguageSelector : MonoBehaviour
//{
//    public TMP_Dropdown languageDropdown;
//    public List<LocaleAbbreviation> localeAbbreviations = new();

//    void Start()
//    {
//        // Populate the dropdown with the language options
//        languageDropdown.ClearOptions();
//        List<string> languageOptions = new List<string>();

//        foreach (var localeAbbr in localeAbbreviations)
//        {
//            languageOptions.Add(localeAbbr.abbreviation);
//        }

//        languageDropdown.AddOptions(languageOptions);

//        // Assign the flag sprite to each option
//        AssignFlagsToDropdownOptions();

//        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
//    }

//    private void AssignFlagsToDropdownOptions()
//    {
//        // Loop through each dropdown option and set the flag sprite
//        for (int i = 0; i < languageDropdown.options.Count; i++)
//        {
//            Transform flagTransform = languageDropdown.itemText.transform.parent.Find("Flag");

//            languageDropdown.image = flagTransform.gameObject.GetComponent<Image>();
//            Debug.Log(languageDropdown.image.name);
//            languageDropdown.image.sprite = localeAbbreviations[i].flag;
//        }

//        // Refresh the dropdown to apply changes
//        languageDropdown.RefreshShownValue();
//    }

//    private void OnLanguageChanged(int index)
//    {
//        // Get the selected Locale
//        Locale selectedLocale = localeAbbreviations[index].locale;

//        // Change the active locale of the game
//        LocalizationSettings.SelectedLocale = selectedLocale;
//    }
//}

