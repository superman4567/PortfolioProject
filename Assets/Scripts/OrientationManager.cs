using UnityEngine;
using UnityEngine.UI;

public class OrientationManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject orientationPrompt;
    public Button closeOrientationButton;

    private bool isMobile;

    void Start()
    {
        // Detect if the game is running on a mobile device
        isMobile = SystemInfo.deviceType == DeviceType.Handheld || Application.platform == RuntimePlatform.WebGLPlayer && IsMobileBrowser();

        // Check orientation initially
        CheckOrientation();
    }

    void Update()
    {
        // Continuously check orientation on mobile
        if (isMobile)
        {
            CheckOrientation();
        }
    }

    void CheckOrientation()
    {
        // Check if the game is in landscape
        if (Screen.width > Screen.height)
        {
            // Landscape - hide the prompt
            if (orientationPrompt != null) orientationPrompt.SetActive(false);
        }
        else
        {
            // Portrait - show the prompt
            if (orientationPrompt != null) orientationPrompt.SetActive(true);
        }
    }

    bool IsMobileBrowser()
    {
        // Check if the game is played on a mobile browser
        string userAgent = SystemInfo.operatingSystem;
        return userAgent.Contains("Android") || userAgent.Contains("iPhone") || userAgent.Contains("iPad");
    }
}
