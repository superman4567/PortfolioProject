using UnityEngine;
using UnityEngine.UI;

public class LayoutAspectRatio : MonoBehaviour, ILayoutSelfController
{
    public float aspectRatio = 1f;
    public LayoutElement _layoutElement;
    public RectTransform _rt;
   

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
        if (_layoutElement == null || _rt == null)
        {
            Debug.LogWarning("Missing components in LayoutAspectRatio", this);
            return;
        }

        float width = _rt.rect.width;
        if (aspectRatio > 0 && width > 0)
        {
            _layoutElement.preferredHeight = width / aspectRatio;
        }
    }
}
