using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThreeDMouseInputChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<bool> OnHoverStateChanged;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverStateChanged?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverStateChanged?.Invoke(false);
    }
}
