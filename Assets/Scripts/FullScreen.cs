using UnityEngine;
using MarksAssets.FullscreenWebGL;
using status = MarksAssets.FullscreenWebGL.FullscreenWebGL.status;
using navigationUI = MarksAssets.FullscreenWebGL.FullscreenWebGL.navigationUI;
using DG.Tweening;

public class FullScreen : MonoBehaviour
{
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private const string HasEnteredGameOnceKey = "hasEnteredGameOnce";

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(HasEnteredGameOnceKey, 0) == 1)
        {
            Hide();
            Debug.Log("Hide");
        }
    }

    void Start()
    {
        if (FullscreenWebGL.isFullscreenSupported())
        {
            Show();

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
    }

    private void Hide()
    {
        canvas.sortingOrder = 0;
        canvasGroup.DOFade(0, 0.2f).OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    private void Show()
    {
        canvas.sortingOrder = 999;
        canvasGroup.DOFade(1, 0.2f).OnComplete(() => canvasGroup.blocksRaycasts = true);
    }

    public void Enterfullscreen()
    {
        FullscreenWebGL.requestFullscreen(stat => {
            if (stat == status.Success)
            {
                PlayerPrefs.SetInt(HasEnteredGameOnceKey, 1);
                PlayerPrefs.Save(); // Ensure the data is written immediately
                Hide();
            }
        }, navigationUI.hide);
    }
}
