using UnityEngine;
using static ProjectContentSO;

public class ContentSpawner : MonoBehaviour
{
    [SerializeField] private ProjectContentSO projectContent;
    [SerializeField] private GameObject UXUIImageprefab;
    [SerializeField] private GameObject UXUIVideoprefab;
    [SerializeField] private RectTransform parent;

    private void OnEnable()
    {
        parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1000000f);

        foreach (var mediaItem in projectContent.MediaItems)
        {
            if (mediaItem.Type == MediaItem.MediaType.Image)
            {
                SpawnContentItem(0, mediaItem);
            }
            else if (mediaItem.Type == MediaItem.MediaType.Video)
            {
                SpawnContentItem(1, mediaItem);
            }
        }
    }

    private void SpawnContentItem(int value, MediaItem item)
    {
        if (value == 0)
        {
            GameObject uxuiImage = Instantiate(UXUIImageprefab, parent);
            uxuiImage.GetComponent<ContentPiece>().InitImage(item);  
        }
        else
        {
            GameObject uxuiVideo = Instantiate(UXUIVideoprefab, parent);
            uxuiVideo.GetComponent<ContentPiece>().InitVideo(item);

        }
    }
}
