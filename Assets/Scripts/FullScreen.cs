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
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Hide();
        }
    

        if (FullscreenWebGL.isFullscreenSupported())
        {
            FullscreenWebGL.subscribeToFullscreenchangedEvent();
            FullscreenWebGL.onfullscreenchange += () => {
                if (FullscreenWebGL.isFullscreen())
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            };
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        canvas.sortingOrder = 0;
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    private void Show()
    {
        canvas.sortingOrder = 999;
        canvasGroup.alpha = 1.0f;
    }

    public void Enterfullscreen()
    {
        FullscreenWebGL.requestFullscreen(stat => {
            if (stat == status.Success)
            {
                Hide();
            }
        }, navigationUI.hide);
    }
}
