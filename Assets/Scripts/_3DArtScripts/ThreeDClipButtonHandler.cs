using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class ThreeDClipButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private Image thumbnail;

    [SerializeField] private RawImage videoPreview;
    [SerializeField] private VideoPlayer videoPlayer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        thumbnail.enabled = false;

        if (videoPlayer.clip != null)
        {
            videoPreview.gameObject.SetActive(true);
            videoPlayer.isLooping = true;
            videoPlayer.SetDirectAudioMute(0, true);
            videoPlayer.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thumbnail.enabled = true;
        
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            videoPreview.gameObject.SetActive(false);
        }
    }

    private void OnClick()
    {
        // Make full screen and play clip from start
        if (videoPlayer.clip != null)
        {
            videoPreview.gameObject.SetActive(true); // Ensure the preview is visible
            videoPlayer.isLooping = false; // Disable looping for full playback
            videoPlayer.SetDirectAudioMute(0, false); // Enable audio
            videoPlayer.Stop(); // Restart the video from the beginning
            videoPlayer.Play();

            // Optional: Handle full-screen UI (like toggling a dedicated video panelWithImages)
            // Example:
            // fullscreenPanel.SetActive(true);
        }
    }
}
