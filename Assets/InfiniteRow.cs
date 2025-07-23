using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteChildren : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform viewportRect;
    [SerializeField] private List<RectTransform> itemsList;
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float spacing = 32f;

    [Header("Fade Settings")]
    [SerializeField] private float fadeStartPadding = 100f; // distance from edge where fade starts
    [SerializeField] private float fadeDuration = 0.25f;

    private List<float> initialX;
    private List<float> itemWidths;
    private float totalRowWidth;
    private float offset;



    public void Start()
    {
        int count = itemsList.Count;
        if (count == 0) return;

        initialX = new List<float>(count);
        itemWidths = new List<float>(count);

        float currentX = 0f;

        foreach (var item in itemsList)
        {
            // Ensure anchor/pivot is left-middle
            item.anchorMin = new Vector2(0f, 0.5f);
            item.anchorMax = new Vector2(0f, 0.5f);
            item.pivot = new Vector2(0f, 0.5f);
        }

        for (int i = 0; i < count; i++)
        {
            RectTransform item = itemsList[i];
            float width = item.rect.width;

            initialX.Add(currentX);
            itemWidths.Add(width);

            item.anchoredPosition = new Vector2(currentX, item.anchoredPosition.y);
            currentX += width + spacing;
        }

        totalRowWidth = currentX;
    }

    private void Update()
    {
        offset = (offset + scrollSpeed * Time.deltaTime) % totalRowWidth;
        float viewWidth = viewportRect.rect.width;
        float viewLeft = 0f;
        float viewRight = viewWidth;

        for (int i = 0; i < itemsList.Count; i++)
        {
            var item = itemsList[i];
            float width = itemWidths[i];
            float x = initialX[i] + offset;

            if (x > totalRowWidth)
                x -= totalRowWidth;
            else if (x < -width)
                x += totalRowWidth;

            item.anchoredPosition = new Vector2(x, item.anchoredPosition.y);

            // --- FADE BASED ON X ---
            float itemCenter = x + (width * 0.5f);
            float distanceToLeft = Mathf.Clamp01((itemCenter - viewLeft) / fadeStartPadding);
            float distanceToRight = Mathf.Clamp01((viewRight - itemCenter) / fadeStartPadding);
            float fadeFactor = Mathf.Min(distanceToLeft, distanceToRight); // 1 = center, 0 = edge

            CanvasGroup cg = item.GetComponent<CanvasGroup>();
            float targetAlpha = fadeFactor;
            cg.DOFade(targetAlpha, fadeDuration);
        }
    }
}
