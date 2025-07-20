using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFileName;
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake()
    {
        LoadVideo();
    }

    void LoadVideo()
    {
        if (videoPlayer == null || string.IsNullOrEmpty(videoFileName))
            return;

        videoPlayer.url = VideoPathUtility.GetVideoUrl(videoFileName);

        videoPlayer.source = VideoSource.Url;

        videoPlayer.waitForFirstFrame = false;
        videoPlayer.isLooping = true;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.Play();
    }

    public void SetString(string value)
    {
        videoFileName = $"{value}.mp4";
        Debug.Log(videoFileName);

        LoadVideo();
    }
}
