using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TapToSkipBroadcaster : MonoBehaviour, IPointerDownHandler
{
    public static event Action OnTapToSkip;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTapToSkip?.Invoke();
    }
}
