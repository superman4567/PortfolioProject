using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UXUIImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;

    [Header("Hover Scaling")]
    [SerializeField] private float scaleSpeed = 0.2f; // Speed of scaling
    private Vector3 hoverScale = new Vector3(1.01f, 1.01f, 1.01f); // Scale on hover

    private Vector3 originalScale; // Store the original scale

    private void Awake()
    {
        originalScale = transform.localScale; // Store the initial scale of the object
    }

    public float GetOriginalWidth()
    {
        return image.preferredWidth;
    }

    public float GetOriginalHeight()
    {
        return image.preferredHeight;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up on hover
        StopAllCoroutines(); // Stop any ongoing scaling
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale down on hover exit
        StopAllCoroutines(); // Stop any ongoing scaling
        StartCoroutine(ScaleTo(originalScale));
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 initialScale = image.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleSpeed)
        {
            image.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.transform.localScale = targetScale; // Ensure it reaches the target scale
    }
}
