using System.Collections;
using UnityEngine;
using DG.Tweening; // Import DOTween

public class MobileOrientationCheck : MonoBehaviour
{
    public Canvas myCanvas;
    public CanvasGroup canvasGroup;
    public float checkInterval = 0.5f;

    private bool isLandscape = false;

    private void Start()
    {
        if (IsMobile() && !IsWebGL())
        {
            StartCoroutine(CheckOrientationPeriodically());
        }
        else
        {
            TweenAlpha(0f);
        }
    }

    private IEnumerator CheckOrientationPeriodically()
    {
        while (true)
        {
            if (IsLandscape() != isLandscape) // Check if orientation has changed
            {
                isLandscape = IsLandscape();

                if (isLandscape)
                {
                    // Landscape: Fade out (alpha to 0)
                    TweenAlpha(0f);
                }
                else
                {
                    // Portrait: Fade in (alpha to 1)
                    TweenAlpha(1f);
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    bool IsMobile()
    {
        return Application.isMobilePlatform;
    }

    bool IsWebGL()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer;
    }

    bool IsLandscape()
    {
        return Screen.width > Screen.height;
    }

    void TweenAlpha(float targetAlpha)
    {
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(targetAlpha, 0.5f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
        }
    }
}
