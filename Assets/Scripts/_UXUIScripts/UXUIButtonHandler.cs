using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UXUIButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Transform originalParent;
    private Transform tempParent;
    public Transform image;

    private Vector2 originalPosition;
    private Vector2 originalSize;

    private Canvas canvasReference;
    private int originalSiblingIndex;

    private bool isFullScreen = false;
    private ScrollRect scrollRect;
    private GridLayoutGroup gridLayout;

    [Header("Anim values")]
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float transitionDuration = 0.5f;
    private float hoverScaleFactor = 1.1f;

    public void Initialize(RectTransform container, RectTransform tempParent, ScrollRect scroll, Canvas canvas)
    {
        this.rectTransform = GetComponent<RectTransform>();

        this.originalParent = container;
        this.tempParent = tempParent;
        this.scrollRect = scroll;
        this.canvasReference = canvas;

        this.gridLayout = container.GetComponent<GridLayoutGroup>();

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFullScreen)
        {
            image.DOScale(hoverScaleFactor, 0.1f).SetEase(transitionCurve);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isFullScreen)
        {
            image.DOScale(1f, 0.1f).SetEase(transitionCurve);
        }
    }

    private void OnClick()
    {
        if (isFullScreen)
        {
            ShrinkToOriginal();
        }
        else
        {
            ExpandToFullScreen();
        }

        isFullScreen = !isFullScreen;
    }

    private void ExpandToFullScreen()
    {
        // Disable ScrollRect and GridLayout
        if (scrollRect != null) scrollRect.enabled = false;
        if (gridLayout != null) gridLayout.enabled = false;

        originalPosition = rectTransform.anchoredPosition;
        originalSize = rectTransform.sizeDelta;

        rectTransform.SetParent(tempParent, true);
        float scaleFactor = canvasReference.scaleFactor;

        float canvasWidth = Screen.width / scaleFactor;
        float canvasHeight = Screen.height / scaleFactor;

        rectTransform.DOSizeDelta(new Vector2(canvasWidth, canvasHeight), transitionDuration)
            .SetEase(transitionCurve);
        rectTransform.DOAnchorPos(Vector2.zero, transitionDuration).SetEase(transitionCurve);
    }

    private void ShrinkToOriginal()
    {
        rectTransform.DOSizeDelta(originalSize, transitionDuration).SetEase(transitionCurve);
        rectTransform.DOAnchorPos(originalPosition, transitionDuration).SetEase(transitionCurve);

        rectTransform.SetParent(originalParent, true);
        
        if (scrollRect != null) scrollRect.enabled = true;
        if (gridLayout != null) gridLayout.enabled = true;
    }
}
