using UnityEngine;
using MarksAssets.FullscreenWebGL;
using status = MarksAssets.FullscreenWebGL.FullscreenWebGL.status;
using navigationUI = MarksAssets.FullscreenWebGL.FullscreenWebGL.navigationUI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class FullScreen : MonoBehaviour
{
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    public EventTrigger inputReceiver;

    private void Awake()
    {
        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        
        entry.callback.AddListener((data) => Enterfullscreen());
        inputReceiver.triggers.Add(entry);
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
                Hide();
            }
        }, navigationUI.hide);
    }
}
