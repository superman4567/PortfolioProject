using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup rootCanvasGroup;
    [SerializeField] private CanvasGroup messageCanvasGroup;
    [SerializeField] private Button claimButton;
    [SerializeField] private GameObject rewardTweenObject;

    private void Start()
    {
        // Initial state
        rootCanvasGroup.alpha = 0f;
        rootCanvasGroup.interactable = false;
        rootCanvasGroup.blocksRaycasts = false;

        messageCanvasGroup.alpha = 0f;
        messageCanvasGroup.interactable = false;
        messageCanvasGroup.blocksRaycasts = false;

        claimButton.onClick.AddListener(OnClaimClicked);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowPopup());
    }

    private IEnumerator ShowPopup()
    {
        yield return new WaitForSeconds(0.2f);

        rootCanvasGroup.DOFade(1f, 0.6f).SetEase(Ease.InOutQuad);
        rootCanvasGroup.interactable = true;
        rootCanvasGroup.blocksRaycasts = true;

        messageCanvasGroup.DOFade(1f, 0.4f).SetEase(Ease.InOutQuad);
        messageCanvasGroup.interactable = true;
        messageCanvasGroup.blocksRaycasts = true;
    }

    private void OnClaimClicked()
    {
        messageCanvasGroup.interactable = false;
        messageCanvasGroup.blocksRaycasts = false;

        messageCanvasGroup.DOFade(0f, 0.3f);
        messageCanvasGroup.gameObject.SetActive(false);
        rewardTweenObject.SetActive(true);
    }
}
