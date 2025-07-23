using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CorperateSceneHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup corperateCanvasgroup;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += OnShowCorperate_Callback;
        ReturnButton.OnReturnToModeSelect += OnHideCorperate_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected += OnShowCorperate_Callback;
        ReturnButton.OnReturnToModeSelect -= OnHideCorperate_Callback;
    }

    private void Start()
    {
        corperateCanvasgroup.alpha = 0f;
        corperateCanvasgroup.interactable = false;
        corperateCanvasgroup.blocksRaycasts = false;
    }

    public void OnShowCorperate_Callback(bool isCorperate)
    {
        if (!isCorperate)
            return;

        StartCoroutine(Fade(corperateCanvasgroup, true));
    }

    public void OnHideCorperate_Callback()
    {
        StartCoroutine(Fade(corperateCanvasgroup, false));
    }

    private IEnumerator Fade(CanvasGroup group, bool fadeIn)
    {
        yield return new WaitForSeconds(0.4f);

        if (fadeIn)
        {
            group.alpha = 0;
            group.DOFade(1f, 1f).SetEase(Ease.InOutSine)
                .OnStart(() => { group.interactable = true; group.blocksRaycasts = true; });
        }
        else
        {
            DOTween.Kill("PressSpaceLoop");
            group.DOFade(0f, 0.2f).SetEase(Ease.InOutSine)
                .OnComplete(() => { group.interactable = false; group.blocksRaycasts = false; });
        }
    }
}
