using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class WarningPopup : MonoBehaviour
{
    [SerializeField] private Button targetButton;
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += OnMOodeSelected_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= OnMOodeSelected_Callback;
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        targetButton.onClick.AddListener(DestroyThisObject);
    }

    private void OnMOodeSelected_Callback(bool isCorperate)
    {
        if (isCorperate)
            return;
        
        StartCoroutine(ShowThisObject());
    }

    private IEnumerator ShowThisObject()
    {
        yield return new WaitForSeconds(1f);

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1f, 1f).SetEase(Ease.InOutQuad);
    }

    private void DestroyThisObject()
    {
        canvasGroup.DOFade(0f, 1f).SetEase(Ease.InOutQuad);
        canvasGroup.DOKill();
        Destroy(gameObject, 1f);
    }
}
