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
    [SerializeField] private float fadeStartPadding = 100f;
    [SerializeField] private float fadeDuration = 0.25f;

    private List<float> initialX;
    private List<float> itemWidths;
    private float totalRowWidth;
    private float offset;

    private Tween scrollTween;

    private void OnDestroy()
    {
        scrollTween?.Kill();
    }

    private void Start()
    {
        int count = itemsList.Count;
        if (count == 0) return;

        initialX = new List<float>(count);
        itemWidths = new List<float>(count);

        float currentX = 0f;

        foreach (var item in itemsList)
        {
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

        scrollTween = DOTween
            .To(() => offset, x => offset = x, totalRowWidth, totalRowWidth / scrollSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .OnUpdate(UpdateScroll);
    }

    private void UpdateScroll()
    {
        offset %= totalRowWidth;

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

            float itemCenter = x + (width * 0.5f);
            float distanceToLeft = Mathf.Clamp01((itemCenter - viewLeft) / fadeStartPadding);
            float distanceToRight = Mathf.Clamp01((viewRight - itemCenter) / fadeStartPadding);
            float fadeFactor = Mathf.Min(distanceToLeft, distanceToRight);

            CanvasGroup cg = item.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                float currentAlpha = cg.alpha;
                float targetAlpha = fadeFactor;

                if (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
                {
                    DOTween.Kill(cg);
                    cg.DOFade(targetAlpha, fadeDuration).SetId(cg);
                }
            }
        }
    }
}
