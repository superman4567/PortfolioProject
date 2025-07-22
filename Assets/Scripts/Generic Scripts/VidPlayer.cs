using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFileName;
    [SerializeField] VideoPlayer videoPlayer;

    private void Start()
    {
        SetString(videoFileName);
    }

    void LoadVideo()
    {
        if (videoPlayer == null || string.IsNullOrEmpty(videoFileName))
            return;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.isLooping = true;

        videoPlayer.url = VideoPathUtility.GetVideoUrl(videoFileName);
        videoPlayer.source = VideoSource.Url;
        
        videoPlayer.Play();
    }

    public void SetString(string value)
    {
        videoFileName = $"{value}.mp4";
        LoadVideo();
    }
}
