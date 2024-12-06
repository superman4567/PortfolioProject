using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static ProjectContentUXUISO;

public class ContentPieceUXUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;

    public void InitImage(MediaItem item)
    {
        image.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
        image.sprite = item.Image;
    }


    public void InitVideo(MediaItem item)
    {
        image.gameObject.SetActive(false);

        Transform parentTransform = transform.parent;
        RectTransform parentRect = parentTransform.GetComponent<RectTransform>();

        float renderTextureWidth = parentRect.rect.width;
        float renderTextureHeight = parentRect.rect.height;

        RenderTexture uniqueTexture = new RenderTexture(800, 450, 0);
        uniqueTexture.Create();

        rawImage.texture = uniqueTexture;

        videoPlayer.targetTexture = uniqueTexture;
        videoPlayer.clip = item.Video;
        
        videoPlayer.gameObject.SetActive(true);
    }
}
