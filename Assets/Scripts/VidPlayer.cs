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

        videoPlayer.source = VideoSource.Url;
        
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;

        videoPlayer.waitForFirstFrame = false;
        videoPlayer.isLooping= true;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
    }

    public void SetString(string value)
    {
        videoFileName = $"{value}.mp4";
        Debug.Log(videoFileName);

        LoadVideo();
    }
}
