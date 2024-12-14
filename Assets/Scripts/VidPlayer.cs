using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFileName;
    [SerializeField] VideoPlayer videoPlayer;

    void Start()
    {
        LoadVideo();
    }

    void LoadVideo()
    {
        if (videoPlayer == null || string.IsNullOrEmpty(videoFileName))
            return;

        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
    }
}
