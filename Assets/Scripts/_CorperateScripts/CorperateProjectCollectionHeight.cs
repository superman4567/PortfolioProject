using UnityEngine;
using UnityEngine.UI;

public class CorperateProjectCollectionHeight : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public float ContentHeight
    {
        get
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            Debug.Log($"Content Height: {rectTransform.rect.height}");
            return rectTransform.rect.height;
        }
    }
}
