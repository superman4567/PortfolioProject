using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    [Header("References")]
    public Button button; // Reference to the button component
    public Image flagImage; // Image displaying the locale's flag
    public TMP_Text languageText; // Text displaying the locale acronym

    private Locale assignedLocale;
    private System.Action<Locale> onLanguageSelected;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    public void SetLocale(Locale locale, System.Action<Locale> onLanguageSelectedCallback)
    {
        assignedLocale = locale;
        onLanguageSelected = onLanguageSelectedCallback;

        // Update flag and acronym
        var metadata = locale.Metadata.GetMetadata<LocaleMetadata>();
        if (metadata != null)
        {
            flagImage.sprite = metadata.Flag; // Custom metadata for flags
        }
        languageText.text = locale.Identifier.Code.ToUpper();
    }

    public Locale GetLocale()
    {
        return assignedLocale;
    }

    private void OnButtonClicked()
    {
        onLanguageSelected?.Invoke(assignedLocale);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }
}