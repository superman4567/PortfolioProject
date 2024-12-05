using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static ProjectContentSO;

public class ContentPiece : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private VideoPlayer videoPlayer;

    public void InitImage(MediaItem item)
    {
        videoPlayer.gameObject.SetActive(false);
        image.sprite = item.Image;
    }


    public void InitVideo(MediaItem  item)
    {
        image.gameObject.SetActive(false);
        videoPlayer.clip = item.Video;
    }
}
