using UnityEngine;
using MarksAssets.FullscreenWebGL;
using status = MarksAssets.FullscreenWebGL.FullscreenWebGL.status;
using navigationUI = MarksAssets.FullscreenWebGL.FullscreenWebGL.navigationUI;

public class FullScreen : MonoBehaviour
{
    public Canvas enterFullscreenBtn;

    void Start()
    {
        if (FullscreenWebGL.isFullscreenSupported())
        {
            FullscreenWebGL.subscribeToFullscreenchangedEvent();
            FullscreenWebGL.onfullscreenchange += () => {
                if (FullscreenWebGL.isFullscreen())
                {//if it's fullscreen
                    enterFullscreenBtn.sortingOrder = 0;
                }
                else
                {//otherwise do the opposite
                    enterFullscreenBtn.sortingOrder = 999;
                }
            };
        }
        else
        {
            enterFullscreenBtn.sortingOrder = 0;
        }
    }

    //call this on a pointerdown event
    public void Enterfullscreen()
    {
        FullscreenWebGL.requestFullscreen(stat => {
            if (stat == status.Success)
            {
                enterFullscreenBtn.sortingOrder = 0;
            }
        }, navigationUI.hide);//setting navigationUI.hide here is redundant because it's the default value, but I'm doing it for completion. This is an example after all.
    }
}
