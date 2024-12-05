using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class UXUIVideo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private VideoPlayer videoPlayer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartPlayingClip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopPlayingClip();
    }

    private void StartPlayingClip()
    {
        videoPlayer.Play();
    }

    private void StopPlayingClip()
    {
        videoPlayer.Pause();
    }
}
