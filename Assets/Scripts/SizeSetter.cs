using UnityEngine;
using UnityEngine.UI;

public class SizeSetter : MonoBehaviour
{
    [SerializeField] private LayoutElement parent;
    [Space]
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform right;

    private const float spacing = 16f;

    private void OnEnable()
    {
        // Determine the maximum height between left and right RectTransforms
        float maxHeight = Mathf.Max(left.rect.height, right.rect.height);

        // Set the minHeight of the parent LayoutElement
        parent.minHeight = maxHeight;

        // Set the anchor and pivot of the left panel to top-left
        left.anchorMin = new Vector2(0, 1); // Top-left
        left.anchorMax = new Vector2(0, 1); // Top-left
        left.pivot = new Vector2(0, 1);     // Top-left

        // Set the anchor and pivot of the right panel to top-right
        right.anchorMin = new Vector2(1, 1); // Top-right
        right.anchorMax = new Vector2(1, 1); // Top-right
        right.pivot = new Vector2(1, 1);     // Top-right

        // Reset the position of the left and right panels
        left.anchoredPosition = Vector2.zero;
        right.anchoredPosition = Vector2.zero;

        // Calculate the width of the parent RectTransform
        RectTransform parentRect = parent.transform as RectTransform;
        if (parentRect != null)
        {
            float parentWidth = parentRect.rect.width;

            // Set the widths as percentages of the parent's width
            float leftWidth = (parentWidth * 0.666666f) - spacing;  // 66.6666%
            float rightWidth = parentWidth * 0.333334f; // 33.333334%

            // Adjust the sizeDelta for left and right RectTransforms
            left.sizeDelta = new Vector2(leftWidth, left.sizeDelta.y);
            right.sizeDelta = new Vector2(rightWidth, right.sizeDelta.y);
        }
    }
}
