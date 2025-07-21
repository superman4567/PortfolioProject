using System;
using System.Collections;
using UnityEngine;

public class UXUIProjectInstructior : MonoBehaviour
{
    [SerializeField] private CanvasGroup projectContainerCanvasGroup;
    [SerializeField] private CanvasGroup projectOverviewCanvasGroup;

    public static event Action<bool> OnProjectButtonClicked;
    public static event Action<bool> OnBackToUXUIprojectOverviewClicked;

    private readonly float transitionDelay = 0.75f;

    private void OnEnable()
    {
        UXUIProjectButton.OnProjectButtonClicked += OnProjectButtonClick;
        ReturnButton.OnReturnToUXUIOverview += OnBackButtonClick;
    }

    private void OnDisable()
    {
        UXUIProjectButton.OnProjectButtonClicked += OnProjectButtonClick;
        ReturnButton.OnReturnToUXUIOverview -= OnBackButtonClick;
    }

    private void Start()
    {
        StartCoroutine(ShowProjectOverview());
    }

    public void OnProjectButtonClick(EnumUXUIProjects a)
    {
        OnProjectButtonClicked?.Invoke(true);
        StartCoroutine(ShowProjectContainer());
    }

    public void OnBackButtonClick()
    {
        OnBackToUXUIprojectOverviewClicked?.Invoke(true);
        StartCoroutine(ShowProjectOverview());
    }

    public IEnumerator ShowProjectOverview()
    {
        yield return new WaitForSeconds(transitionDelay); // Optional delay for smoother transition

        projectOverviewCanvasGroup.alpha = 1f;
        projectOverviewCanvasGroup.interactable = true;
        projectOverviewCanvasGroup.blocksRaycasts = true;

        projectContainerCanvasGroup.alpha = 0f;
        projectContainerCanvasGroup.interactable = false;
        projectContainerCanvasGroup.blocksRaycasts = false;
    }

    public IEnumerator ShowProjectContainer()
    {
        yield return new WaitForSeconds(transitionDelay);

        projectOverviewCanvasGroup.alpha = 0f;
        projectOverviewCanvasGroup.interactable = false;
        projectOverviewCanvasGroup.blocksRaycasts = false;

        projectContainerCanvasGroup.alpha = 1f;
        projectContainerCanvasGroup.interactable = true;
        projectContainerCanvasGroup.blocksRaycasts = true;
    }
}
