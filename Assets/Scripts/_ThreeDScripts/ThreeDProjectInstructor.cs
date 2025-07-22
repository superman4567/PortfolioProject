using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ThreeDProjectInstructor : MonoBehaviour
{
    [SerializeField] private CanvasGroup buttonContainer;
    [SerializeField] private CanvasGroup galleryContainer;
    [SerializeField] private CanvasGroup returnButtonContainer;


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

        returnButtonContainer.alpha = 1;
        returnButtonContainer.interactable = true;
        returnButtonContainer.blocksRaycasts = true;
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
        returnButtonContainer.DOFade(0, 0.2f);
        returnButtonContainer.interactable = false;
        returnButtonContainer.blocksRaycasts = false;

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
        returnButtonContainer.DOFade(1, 0.2f);
        returnButtonContainer.interactable = true;
        returnButtonContainer.blocksRaycasts = true;

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
