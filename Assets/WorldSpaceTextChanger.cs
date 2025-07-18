using UnityEngine;
using TMPro;
using System.Collections;

public class WorldSpaceTextChanger : MonoBehaviour
{
    [Header("Text Reference")]
    public TextMeshProUGUI textElement;

    [Header("Typing Settings")]
    public float typeSpeed = 0.05f;

    private Coroutine currentRoutine;

    public void SetToGaming()
    {
        ChangeText("Gaming", Color.white);
    }

    public void SetToCorperate()
    {
        ChangeText("Corperate", Color.black);
    }

    private void ChangeText(string newText, Color newColor)
    {
        if (textElement == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(AnimateTextChange(newText, newColor));
    }

    private IEnumerator AnimateTextChange(string newText, Color newColor)
    {

        yield return new WaitForSeconds(.2f);

        string current = textElement.text;

        // Step 1: Backspace animation
        for (int i = current.Length; i >= 0; i--)
        {
            textElement.text = current.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        // Step 2: Set new color
        textElement.color = newColor;

        // Step 3: Type-in animation
        for (int i = 1; i <= newText.Length; i++)
        {
            textElement.text = newText.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
