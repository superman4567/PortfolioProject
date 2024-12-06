using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXUIProjectHandler : MonoBehaviour
{
    [SerializeField] private Mask mask;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private float rowSpacing = 10f;
    [SerializeField] private float childSpacing = 10f;
    private readonly List<UXUIImage> childUXUIImages = new();

    private void OnEnable() => mask.enabled = true;

    private void Start()
    {
        foreach (Transform child in parentRectTransform)
        {
            if (child.TryGetComponent(out UXUIImage item))
                childUXUIImages.Add(item);
        }
        Invoke(nameof(ArrangeChildrenVertically), 0.2f);
    }

    private void ArrangeChildrenVertically()
    {
        float parentWidth = parentRectTransform.rect.width;
        float currentY = 0f;
        float rowHeight = 0f;
        List<RectTransform> currentRow = new();

        foreach (UXUIImage child in childUXUIImages)
        {
            float nativeWidth = child.GetOriginalWidth();
            float nativeHeight = child.GetOriginalHeight();
            if (nativeWidth <= 0 || nativeHeight <= 0) continue;

            RectTransform childRect = child.GetComponent<RectTransform>();

            if (nativeWidth < nativeHeight)
            {
                currentRow.Add(childRect);
                if (currentRow.Count == 2)
                {
                    FitRowToParent(currentRow, parentWidth, ref currentY, ref rowHeight);
                    currentRow.Clear();
                }
            }
            else
            {
                if (currentRow.Count > 0)
                {
                    FitRowToParent(currentRow, parentWidth, ref currentY, ref rowHeight);
                    currentRow.Clear();
                }

                ScaleAndPositionChild(childRect, nativeWidth, nativeHeight, parentWidth, ref currentY);
            }
        }

        if (currentRow.Count > 0)
            FitRowToParent(currentRow, parentWidth, ref currentY, ref rowHeight);

        parentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentY);
        verticalLayoutGroup.enabled = true;
    }

    private void FitRowToParent(List<RectTransform> row, float parentWidth, ref float currentY, ref float rowHeight)
    {
        if (row.Count == 0) return;

        GameObject newParent = new GameObject("RowGroup");
        RectTransform newParentRect = newParent.AddComponent<RectTransform>();
        newParent.transform.SetParent(parentRectTransform);
        newParentRect.anchorMin = newParentRect.anchorMax = newParentRect.pivot = new Vector2(0, 1);
        newParentRect.localScale = Vector3.one;

        float totalNativeWidth = 0f;
        foreach (RectTransform child in row)
            totalNativeWidth += child.GetComponent<UXUIImage>().GetOriginalWidth();

        float scaleFactor = (parentWidth - childSpacing) / totalNativeWidth;
        float xOffset = 0f;

        foreach (RectTransform child in row)
        {
            float nativeWidth = child.GetComponent<UXUIImage>().GetOriginalWidth();
            float nativeHeight = child.GetComponent<UXUIImage>().GetOriginalHeight();

            float newWidth = nativeWidth * scaleFactor;
            float newHeight = nativeHeight * scaleFactor;

            child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            child.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
            child.SetParent(newParentRect);
            child.anchoredPosition = new Vector2(xOffset, 0);

            xOffset += newWidth + childSpacing;
            rowHeight = Mathf.Max(rowHeight, newHeight);
        }

        newParentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentWidth);
        newParentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rowHeight);
        newParentRect.anchoredPosition = new Vector2(0, -currentY);

        currentY += rowHeight + rowSpacing;
        rowHeight = 0f;
    }

    private void ScaleAndPositionChild(RectTransform childRect, float nativeWidth, float nativeHeight, float parentWidth, ref float currentY)
    {
        float scaleFactor = parentWidth / nativeWidth;
        float newWidth = nativeWidth * scaleFactor;
        float newHeight = nativeHeight * scaleFactor;

        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        childRect.anchoredPosition = new Vector2(0, -currentY);

        currentY += newHeight + rowSpacing;
    }
}
