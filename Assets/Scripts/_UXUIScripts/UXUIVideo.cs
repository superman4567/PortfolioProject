using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class UXUIVideo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private VideoPlayer videoPlayer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!videoPlayer.gameObject.activeSelf)
            return;

        StartPlayingClip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!videoPlayer.gameObject.activeSelf)
            return;

        PausePlayingClip();
    }

    private void StartPlayingClip()
    {
        videoPlayer.Play();
    }

    private void PausePlayingClip()
    {
        videoPlayer.Pause();
    }
}
