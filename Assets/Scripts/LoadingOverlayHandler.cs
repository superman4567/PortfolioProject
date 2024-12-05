using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingOverlayHandler : MonoBehaviour
{
    [Header("LoadingBar References")]
    [SerializeField] private CanvasGroup loadingSceneOverlay;
    [SerializeField] private Image loadingBar;

    private float currentAmount = 0f;

    void Start()
    {
        loadingSceneOverlay.alpha = 0f; // Hide overlay initially
        loadingBar.fillAmount = 0f;     // Reset loading bar
    }

    public void FillLoadingAmount(float addedAmount)
    {
        if (addedAmount != 1)
        {
            loadingSceneOverlay.DOFade(1f, 0.5f);

            float targetAmount = (currentAmount += addedAmount);

            DOTween.To(() => currentAmount, x =>
            {
                currentAmount = x;
                loadingBar.fillAmount = currentAmount;
            }, targetAmount, 1f)
            .SetEase(Ease.InOutQuad);

            if (currentAmount >= 1f)
            {
                Invoke(nameof(ResetFillAmount), 2f);
            }
        }
        else
        {
            ResetFillAmount();
        }
        
    }

    public void ResetFillAmount()
    {
        loadingSceneOverlay.DOFade(0f, 0.5f);
        loadingSceneOverlay.alpha = 0f;
        currentAmount = 0f;
    }
}
