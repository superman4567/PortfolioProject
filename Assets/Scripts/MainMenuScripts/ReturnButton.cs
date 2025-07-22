using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private float colorTweenDuration = 0.2f;
    [SerializeField] private float scaleTweenDuration = 0.2f;      // duration for scale
    [SerializeField] private Vector3 hoverScale = Vector3.one * 1.05f;

    [SerializeField] private Color activeColorBackground = Color.white;
    [SerializeField] private Color inactiveColorBackground = new Color(0.537f, 0.537f, 0.537f);

    [Header("Mode select")]
    [SerializeField] private bool isBackToModeSelect = false;

    [Header("Category")]
    [SerializeField] private bool isBackToCategorySelect = false;

    [Header("UXUI")]
    [SerializeField] private bool isBackToUXUIOverview = false;

    [Header("3D")]
    [SerializeField] private bool isBackTo3DList = false;

    public static Action OnReturnToModeSelect;
    public static Action OnReturnTocategory;
    public static Action OnReturnToUXUIOverview;
    public static Action OnReturnTo3DList;

    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = button.transform.localScale;
        button.transform.localScale = _originalScale;
        button.image.color = inactiveColorBackground;
        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image
              .DOColor(activeColorBackground, colorTweenDuration)
              .SetEase(Ease.InOutQuad)
              .SetUpdate(true);

        button.transform
              .DOScale(hoverScale, scaleTweenDuration)
              .SetEase(Ease.OutQuad)
              .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image
              .DOColor(inactiveColorBackground, colorTweenDuration)
              .SetEase(Ease.InOutQuad)
              .SetUpdate(true);

        button.transform
              .DOScale(_originalScale, scaleTweenDuration)
              .SetEase(Ease.InQuad)
              .SetUpdate(true);
    }

    //Set in the inspector!
    public void OnClick()
    {
        if (isBackToCategorySelect)
        {
            OnReturnTocategory?.Invoke();
        }

        if (isBackToUXUIOverview)
        {
            OnReturnToUXUIOverview?.Invoke();
        }

        if (isBackToModeSelect)
        {
            OnReturnToModeSelect?.Invoke();
        }

        if (isBackTo3DList)
        {
            OnReturnTo3DList?.Invoke();
        }
    }
}
