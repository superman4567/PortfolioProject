using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [Header("Camera reference")]
    [SerializeField] private CinemachineVirtualCamera introCamera;
    [SerializeField] private AnimationHandler animHandlerCharacter;
    [SerializeField] private AnimationHandler animHandlerBoss;

    [Header("First Time Entering")]
    [SerializeField] private CanvasGroup pressSpaceToStartCanvasGroup;
    [SerializeField] private Button pressSpaceToStartButton;
    [Space]
    [SerializeField] private Transform firstTimeStartPosMM;
    [SerializeField] private Transform firstTimeEndPosMM;
    [Space]
    [SerializeField] private Transform defaultTimeStartPosMM;
    [SerializeField] private Transform defaultTimeEndPosMM;
    [Space]
    [SerializeField] private float transitionDuration;
    private Tween pressSpaceTween;

    [Header("Entering")]
    [SerializeField] private CanvasGroup categoryCanvasGroup;
    [Space]
    [SerializeField] private Button buttonUXUI;
    [SerializeField] private Button button3D;
    [SerializeField] private Button buttonProgramming;
    [SerializeField] private Button buttongProfile;
    [SerializeField] private Button buttonContact;
    [Space]
    [SerializeField] private Button[] backButtons;
    private Tween categoryTween;

    [Header("UXUI")]
    [SerializeField] private Transform UXUIStartPosMM;
    [SerializeField] private Transform UXUIEndPosMM;
    

    void Start()
    {
        AssignOnclicks();

        //Set UI
        ShowPressSpaceToStart(false);
        TweenLoopCategoryOptions(true);

        //Set start pos camera
        Transform cameraTransform = introCamera.transform;
        cameraTransform.SetPositionAndRotation(firstTimeStartPosMM.position, firstTimeStartPosMM.rotation);

        //Set idle anims
        animHandlerCharacter.PlayAnimationDirectly("AN_Character_Idle");
        animHandlerBoss.PlayAnimationDirectly("AN_Boss_Idle");
    }

    private void AssignOnclicks()
    {
        pressSpaceToStartButton.onClick.AddListener(ShowCatergoryOptions);

        buttonUXUI.onClick.AddListener(ShowUXUI);
        button3D.onClick.AddListener(ShowUXUI);
        buttonProgramming.onClick.AddListener(ShowUXUI);
        buttongProfile.onClick.AddListener(ShowUXUI);
        buttonContact.onClick.AddListener(ShowUXUI);

        foreach (Button button in backButtons)
        {
            button.onClick.AddListener(BackToMainMenu);
        }
    }

    #region Entering Main Menu

    private void ShowPressSpaceToStart(bool stopTween)
    {
        if (stopTween)
        {
            pressSpaceTween?.Kill();
            pressSpaceToStartCanvasGroup.alpha = 0f;
            pressSpaceToStartCanvasGroup.interactable = false;
        }
        else
        {
            pressSpaceToStartCanvasGroup.alpha = 0f;
            pressSpaceTween = pressSpaceToStartCanvasGroup.DOFade(1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            pressSpaceToStartCanvasGroup.interactable = true;
        }
    }

    private void ShowCatergoryOptions()
    {
        LerpIntroCameraFirstTime();
        ShowPressSpaceToStart(true); // Stop tween for pressSpaceToStartCanvasGroup
        TweenLoopCategoryOptions(false); // Show category options
    }

    private void TweenLoopCategoryOptions(bool stopTween)
    {
        if (stopTween)
        {
            categoryTween?.Kill(); // Stop the category fade tween
            categoryCanvasGroup.alpha = 0f;
            categoryCanvasGroup.interactable = false;
        }
        else
        {
            pressSpaceTween?.Kill();

            categoryCanvasGroup.alpha = 0f;
            categoryTween = categoryCanvasGroup.DOFade(1f, 1f).SetEase(Ease.InOutSine);
            categoryCanvasGroup.interactable = true;
        }
    }

    private void LerpIntroCameraFirstTime()
    {
        Transform cameraTransform = introCamera.transform;

        cameraTransform.SetPositionAndRotation(firstTimeStartPosMM.position, firstTimeStartPosMM.rotation);
        cameraTransform.DOMove(firstTimeEndPosMM.position, transitionDuration).SetEase(Ease.InOutSine);
        cameraTransform.DORotateQuaternion(firstTimeEndPosMM.rotation, transitionDuration).SetEase(Ease.InOutSine);
    }

    private void LerpIntroCameraDefault()
    {
        Transform cameraTransform = introCamera.transform;

        cameraTransform.SetPositionAndRotation(defaultTimeStartPosMM.position, defaultTimeStartPosMM.rotation);
        cameraTransform.DOMove(defaultTimeEndPosMM.position, transitionDuration).SetEase(Ease.InOutSine);
        cameraTransform.DORotateQuaternion(defaultTimeEndPosMM.rotation, transitionDuration).SetEase(Ease.InOutSine);
    }

    private void BackToMainMenu()
    {
        LerpIntroCameraDefault();
    }

    #endregion

    #region UXUI

    private void ShowUXUI()
    {
        //Start Timeline
    }

    #endregion

    #region 3D

    private void Show3D()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Programming

    private void ShowProgramming()
    {
        throw new NotImplementedException();
    }

    #endregion
}
