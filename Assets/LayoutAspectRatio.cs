using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class LayoutAspectRatio : MonoBehaviour, ILayoutSelfController
{
    [Tooltip("Width/Height ratio of the sprite or desired aspect")]
    public float aspectRatio = 1f;

    private LayoutElement _layoutElement;
    private RectTransform _rt;

    void Awake()
    {
        _rt = (RectTransform)transform;
        _layoutElement = GetComponent<LayoutElement>();
    }

    // Called by the layout system after it sets widths.
    public void SetLayoutHorizontal()
    {
        UpdatePreferredHeight();
    }

    public void SetLayoutVertical()
    {
        // nothing extra needed here
    }

    private void UpdatePreferredHeight()
    {
        float width = _rt.rect.width;
        if (aspectRatio > 0 && width > 0)
        {
            _layoutElement.preferredHeight = width / aspectRatio;
        }
    }
}
