using DG.Tweening;
using UnityEngine;

public class ToggleBlackBars : MonoBehaviour
{
    [Header("Black bars")]
    [SerializeField] private RectTransform topbar;
    [SerializeField] private RectTransform botbar;

    private void OnEnable()
    {
        CategoryManager.OnShowBlackBars += ToggleBlackBars_Callback;
    }

    private void OnDisable()
    {
        CategoryManager.OnShowBlackBars -= ToggleBlackBars_Callback;
    }

    private void ToggleBlackBars_Callback(bool show)
    {
        float targetHeight = show ? 80f : 0f;

        topbar.DOSizeDelta(new Vector2(topbar.sizeDelta.x, targetHeight), 0.5f).SetEase(Ease.InOutSine);
        botbar.DOSizeDelta(new Vector2(botbar.sizeDelta.x, targetHeight), 0.5f).SetEase(Ease.InOutSine);
    }
}
