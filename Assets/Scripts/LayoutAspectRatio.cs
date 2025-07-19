using UnityEngine;
using UnityEngine.UI;

public class LayoutAspectRatio : MonoBehaviour, ILayoutSelfController
{
    public float aspectRatio = 1f;
    private LayoutElement _layoutElement;
    private RectTransform _rt;

    public void SetLayoutHorizontal()
    {
        if (_layoutElement == null)
        {
            _layoutElement = GetComponent<LayoutElement>();
        }


        if (_rt == null)
        {
            _rt = GetComponent<RectTransform>();
        }

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
