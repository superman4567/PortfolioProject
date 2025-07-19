using System.Collections;
using UnityEngine;
using DG.Tweening; // Import DOTween

public class MobileOrientationCheck : MonoBehaviour
{
    [Header("Canvas Settings")]
    public Canvas myCanvas;
    public CanvasGroup canvasGroup;

    [Header("Configuration")]
    [Tooltip("Interval (in seconds) for checking screen orientation.")]
    public float checkInterval = 0.5f;

    private bool isLandscape;

    private void Start()
    {
        if (ShouldCheckOrientation())
        {
            isLandscape = IsLandscape(); // Initialize the current orientation
            StartCoroutine(CheckOrientationPeriodically());
        }
        else
        {
            SetCanvasVisibility(false); // Hide the canvas on unsupported platforms
        }
    }

    private IEnumerator CheckOrientationPeriodically()
    {
        while (true)
        {
            bool currentOrientation = IsLandscape();
            if (currentOrientation != isLandscape) // Orientation changed
            {
                isLandscape = currentOrientation;
                SetCanvasVisibility(!isLandscape); // Show in portrait, hide in landscape
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    private bool ShouldCheckOrientation()
    {
        return Application.isMobilePlatform && Application.platform != RuntimePlatform.WebGLPlayer;
    }

    private bool IsLandscape()
    {
        return Screen.width > Screen.height;
    }

    private void SetCanvasVisibility(bool visible)
    {
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(visible ? 1f : 0f, 0.5f)
                .OnComplete(() => canvasGroup.blocksRaycasts = visible);
        }
    }
}
