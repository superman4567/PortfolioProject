using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ThreeDProjectInstructor : MonoBehaviour
{
    [SerializeField] private CanvasGroup buttonContainer;
    [SerializeField] private CanvasGroup galleryContainer;

    private readonly float transitionDelay = 0.1f;

    private void OnEnable()
    {
        ThreeDProjectButton.OnProjectButtonClicked += OnProjectButtonClicked_Callback;
        ReturnButton.OnReturnTo3DList += OnBackButtonClick;
    }

    private void OnDisable()
    {
        ThreeDProjectButton.OnProjectButtonClicked -= OnProjectButtonClicked_Callback;
        ReturnButton.OnReturnTo3DList -= OnBackButtonClick;
    }

    private void Start()
    {
        buttonContainer.alpha = 1;
        buttonContainer.interactable = true;
        buttonContainer.blocksRaycasts = true;

        galleryContainer.alpha = 0;
        galleryContainer.interactable = false;
        galleryContainer.blocksRaycasts = false;
    }

    public void OnProjectButtonClicked_Callback(EnumThreeDProjects a)
    {
        StartCoroutine(OnThreeDProjectButtonClicked());
    }

    public void OnBackButtonClick()
    {
        StartCoroutine(OnBackToThreeDButtonList());
    }

    public IEnumerator OnThreeDProjectButtonClicked()
    {
        yield return new WaitForSeconds(transitionDelay); // Optional delay for smoother transition

        buttonContainer.DOFade(0, 0.5f).OnComplete(() =>
        {
            buttonContainer.interactable = false;
            buttonContainer.blocksRaycasts = false;

            galleryContainer.DOFade(1, 0.5f);
            galleryContainer.interactable = true;
            galleryContainer.blocksRaycasts = true;

        });
    }

    public IEnumerator OnBackToThreeDButtonList()
    {
        yield return new WaitForSeconds(transitionDelay);

        galleryContainer.DOFade(0, 0.5f).OnComplete(() =>
        {
            galleryContainer.interactable = false;
            galleryContainer.blocksRaycasts = false;

            buttonContainer.DOFade(1, 0.5f);
            buttonContainer.interactable = true;
            buttonContainer.blocksRaycasts = true;
        });
    }
}
