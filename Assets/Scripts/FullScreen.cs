using UnityEngine;
using MarksAssets.FullscreenWebGL;
using status = MarksAssets.FullscreenWebGL.FullscreenWebGL.status;
using navigationUI = MarksAssets.FullscreenWebGL.FullscreenWebGL.navigationUI;
using DG.Tweening;

public class FullScreen : MonoBehaviour
{
    public Canvas canvas;

    public CanvasGroup canvasGroup;

    void Start()
    {
        if (FullscreenWebGL.isFullscreenSupported())
        {
            FullscreenWebGL.subscribeToFullscreenchangedEvent();
            FullscreenWebGL.onfullscreenchange += () => {
                if (FullscreenWebGL.isFullscreen())
                {//if it's fullscreen
                    canvas.sortingOrder = 0;
                    canvasGroup.DOFade(0, 0.5f).OnComplete(() => canvasGroup.blocksRaycasts = false);
                }
                else
                {//otherwise do the opposite
                    canvas.sortingOrder = 999;
                    canvasGroup.alpha= 1.0f;
                }
            };
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    //call this on a pointerdown event
    public void Enterfullscreen()
    {
        FullscreenWebGL.requestFullscreen(stat => {
            if (stat == status.Success)
            {
                canvas.sortingOrder = 0;
            }
        }, navigationUI.hide);//setting navigationUI.hide here is redundant because it's the default value, but I'm doing it for completion. This is an example after all.

        canvasGroup.DOFade(0, 0.5f).OnComplete(() => canvasGroup.blocksRaycasts = false);
    }
}
