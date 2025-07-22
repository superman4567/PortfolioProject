using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class WorldSpaceTextChanger : MonoBehaviour
{
    public enum Mode { Corporate, Gaming }

    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI textElement;
    [SerializeField] private LocalizeStringEvent textEvent;
    [SerializeField] private LocalizedString gamingString;
    [SerializeField] private LocalizedString corporateString;

    [Header("Typing Settings")]
    [SerializeField] private float typeSpeed = 0.05f;

    private Coroutine currentRoutine;

    public void SetTo(Mode mode)
    {
        switch (mode)
        {
            case Mode.Gaming:
                ChangeText(gamingString, Color.white);
                break;
            case Mode.Corporate:
                ChangeText(corporateString, Color.black);
                break;
        }
    }

    public void SetToGaming() => SetTo(Mode.Gaming);
    public void SetToCorporate() => SetTo(Mode.Corporate);

    private void ChangeText(LocalizedString localizedSource, Color newColor)
    {
        if (textElement == null || textEvent == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        textEvent.StringReference = localizedSource;
        textEvent.OnUpdateString.RemoveAllListeners();
        textEvent.OnUpdateString.AddListener(str => StartTyping(str, newColor));
        textEvent.RefreshString();
    }

    private void StartTyping(string newText, Color newColor)
    {
        currentRoutine = StartCoroutine(AnimateTextChange(newText, newColor));
    }

    private IEnumerator AnimateTextChange(string newText, Color newColor)
    {
        yield return new WaitForSeconds(0.2f);

        string current = textElement.text;
        for (int i = current.Length; i >= 0; i--)
        {
            textElement.text = current.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        textElement.color = newColor;

        for (int i = 1; i <= newText.Length; i++)
        {
            textElement.text = newText.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
