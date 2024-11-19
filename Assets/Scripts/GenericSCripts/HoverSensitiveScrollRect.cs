using UnityEngine;
using UnityEngine.UI;

public class HoverSensitiveScrollRect : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform rectTransform;

    private void Update()
    {
        if (IsMouseOver())
        {
            HandleScrollInput();
        }
    }

    private bool IsMouseOver()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            Input.mousePosition,
            Camera.main
        );
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            if (scrollRect.vertical)
            {
                scrollRect.verticalNormalizedPosition -= scroll * scrollRect.scrollSensitivity;
            }
            else if (scrollRect.horizontal)
            {
                scrollRect.horizontalNormalizedPosition -= scroll * scrollRect.scrollSensitivity;
            }
        }
    }
}
