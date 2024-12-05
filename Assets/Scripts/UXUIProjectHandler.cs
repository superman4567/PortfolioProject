using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXUIProjectHandler : MonoBehaviour
{
    [SerializeField] private Mask mask;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private float spacing = 10f; // Spacing between child objects
    private List<UXUIImage> childUXUIImages = new();

    private void OnEnable()
    {
        mask.enabled = true;
    }

    private void Start()
    {
        foreach (Transform child in parentRectTransform)
        {
            UXUIImage item = child.GetComponent<UXUIImage>();
            if (item != null)
            {
                childUXUIImages.Add(item);
            }
        }

        Invoke(nameof(ArrangeChildrenVertically),0.2f);
    }

    void ArrangeChildrenVertically()
    {
        float parentWidth = parentRectTransform.rect.width; // Get the parent width

        float currentY = 0f; // Tracks the Y position for placing children

        foreach (UXUIImage childUXUIImage in childUXUIImages)
        {
            float nativeWidth = childUXUIImage.GetOriginalWidth();
            float nativeHeight = childUXUIImage.GetOriginalHeight();

            if (nativeWidth > 0)
            {
                // Calculate the scale factor and adjusted sizes
                float scaleFactor = parentWidth / nativeWidth;
                float newWidth = nativeWidth * scaleFactor;
                float newHeight = nativeHeight * scaleFactor;

                // Adjust the RectTransform size
                RectTransform childRect = childUXUIImage.GetComponent<RectTransform>();
                childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
                childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);

                // Position the child vertically
                childRect.anchoredPosition = new Vector2(0, -currentY); // Offset vertically
                currentY += newHeight + spacing; // Increment Y position by height and spacing
            }
            else
            {
                Debug.LogWarning($"Native width is zero or invalid for child: {childUXUIImage.gameObject.name}");
            }
        }

        float totalHeight = currentY - spacing; // Subtract the extra spacing after the last child
        parentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);

        verticalLayoutGroup.enabled= true;
    }
}
