using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowThisObject());
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        closeButton.onClick.AddListener(DestroyThisObject);
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
