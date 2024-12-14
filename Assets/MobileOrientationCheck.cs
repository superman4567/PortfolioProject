using System.Collections;
using UnityEngine;

public class MobileOrientationCheck : MonoBehaviour
{
    // Reference to your Canvas
    public Canvas myCanvas;

    // Interval to check orientation (in seconds)
    public float checkInterval = 0.5f;

    private void Start()
    {
        // Start the coroutine to check orientation
        StartCoroutine(CheckOrientationPeriodically());
    }

    private IEnumerator CheckOrientationPeriodically()
    {
        while (true)
        {
            // Check if the game is running on a mobile device
            if (IsMobile())
            {
                // Check if the device is in landscape or portrait mode
                if (IsLandscape())
                {
                    SetCanvasPriority(0); // Landscape mode
                }
                else
                {
                    SetCanvasPriority(1000); // Portrait mode
                }
            }

            // Wait for the next interval before checking again
            yield return new WaitForSeconds(checkInterval);
        }
    }

    // Check if the game is being played on a mobile device
    bool IsMobile()
    {
        return Application.isMobilePlatform;
    }

    // Check if the device is in landscape mode
    bool IsLandscape()
    {
        return Screen.width > Screen.height;
    }

    // Set the canvas priority (sorting order) based on orientation
    void SetCanvasPriority(int priority)
    {
        if (myCanvas != null)
        {
            myCanvas.sortingOrder = priority;
        }
    }
}
