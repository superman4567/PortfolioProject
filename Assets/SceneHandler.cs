using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
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

    [SerializeField] private CanvasGroup categoryCanvasGroup;
    private Tween categoryTween;
    [Space]
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
    
    [SerializeField] private Button buttonUXUI;
    [SerializeField] private PlayableDirector playableDirectorUXUI;
    [SerializeField] private CanvasGroup canvasgroupUXUI;
    [SerializeField] private Button backButtonUXUI;
    private Tween uXUITween;
    [Space]
    [SerializeField] private Button button3D;
    [SerializeField] private PlayableDirector playableDirector3D;
    [SerializeField] private CanvasGroup canvasgroup3D;
    [SerializeField] private Button backButton3D;
    private Tween threeDTween;
    [Space]
    [SerializeField] private Button buttonProgramming;
    [SerializeField] private PlayableDirector playableDirectorProgramming;
    [SerializeField] private CanvasGroup canvasgroupProgramming;
    [SerializeField] private Button backButtonProgramming;
    private Tween programmingTween;
    [Space]
    [SerializeField] private Button buttongProfile;
    [SerializeField] private CanvasGroup canvasgroupProfile;
    [SerializeField] private Button backButtonProfile;
    private Tween profileTween;
    [Space]
    [SerializeField] private Button buttonContact;
    [SerializeField] private CanvasGroup canvasgroupContact;
    [SerializeField] private Button backButtonContact;
    private Tween contactTween;

    void Start()
    {
        OnClickAssigning();

        //Set UI
        ShowPressSpaceToStart(true);
        ShowCategoryOptions(false);

        //Set start pos camera
        Transform cameraTransform = introCamera.transform;
        cameraTransform.SetPositionAndRotation(firstTimeStartPosMM.position, firstTimeStartPosMM.rotation);

        //Set idle anims
        animHandlerCharacter.PlayAnimationDirectly("AN_Character_Idle");
        animHandlerBoss.PlayAnimationDirectly("AN_Boss_Idle");
    }

    #region onclick

    private void OnClickAssigning()
    {
        pressSpaceToStartButton.onClick.AddListener(ShowCatergoryOptionsHandler);

        buttonUXUI.onClick.AddListener(ShowUXUIHandler);
        button3D.onClick.AddListener(Show3DHandler);
        buttonProgramming.onClick.AddListener(ShowProgrammingHandler);
        buttongProfile.onClick.AddListener(ShowProfileHandler);
        buttonContact.onClick.AddListener(ShowContactHandler);

        backButtonUXUI.onClick.AddListener(() => BackToMainMenu(canvasgroupUXUI, ref uXUITween));
        backButton3D.onClick.AddListener(() => BackToMainMenu(canvasgroup3D, ref threeDTween));
        backButtonProgramming.onClick.AddListener(() => BackToMainMenu(canvasgroupProgramming, ref programmingTween));
        backButtonProfile.onClick.AddListener(() => BackToMainMenu(canvasgroupProfile, ref profileTween));
        backButtonContact.onClick.AddListener(() => BackToMainMenu(canvasgroupContact, ref contactTween));
    }

    private void ShowUXUIHandler()
    {
        playableDirectorUXUI.Play();
        ShowCategoryOptions(false);
    }

    public void ShowUXUI()
    {
        canvasgroupUXUI.alpha = 0f;
        uXUITween = canvasgroupUXUI.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        canvasgroupUXUI.interactable = true;
        canvasgroupUXUI.blocksRaycasts = true;
    }

    private void Show3DHandler()
    {
        playableDirector3D.Play();
        ShowCategoryOptions(false);
    }

    public void Show3D()
    {
        canvasgroup3D.alpha = 0f;
        threeDTween = canvasgroup3D.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        canvasgroup3D.interactable = true;
        canvasgroup3D.blocksRaycasts = true;
    }

    private void ShowProgrammingHandler()
    {
        playableDirectorProgramming.Play();
        ShowCategoryOptions(false);
    }

    public void ShowProgramming()
    {
        canvasgroupProgramming.alpha = 0f;
        programmingTween = canvasgroupProgramming.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        canvasgroupProgramming.interactable = true;
        canvasgroupProgramming.blocksRaycasts = true;
    }

    private void ShowProfileHandler()
    {
        ShowProfile();
        ShowCategoryOptions(false);
    }

    private void ShowProfile()
    {
        canvasgroupProfile.alpha = 0f;
        profileTween = canvasgroupProfile.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        canvasgroupProfile.interactable = true;
        canvasgroupProfile.blocksRaycasts = true;
    }

    private void ShowContactHandler()
    {
        ShowContact();
        ShowCategoryOptions(false);
    }

    private void ShowContact()
    {
        canvasgroupContact.alpha = 0f;
        contactTween = canvasgroupContact.DOFade(1f, 1f).SetEase(Ease.InOutSine);
        canvasgroupContact.interactable = true;
        canvasgroupContact.blocksRaycasts = true;
    }

    private void BackToMainMenu(CanvasGroup canvasGroup, ref Tween tween)
    {
        FadeOutCanvasGroup(canvasGroup, ref tween);
        LerpIntroCameraDefault();
        ShowCategoryOptions(true);
    }

    private void FadeOutCanvasGroup(CanvasGroup canvasGroup, ref Tween tween)
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = 1f;
        tween = canvasGroup.DOFade(0f, 1f).SetEase(Ease.InOutSine);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    #endregion

    private void ShowPressSpaceToStart(bool play)
    {
        if (!play)
        {
            pressSpaceTween?.Kill();
            pressSpaceToStartCanvasGroup.alpha = 0f;
            pressSpaceToStartCanvasGroup.interactable = false;
            pressSpaceToStartCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            pressSpaceToStartCanvasGroup.alpha = 0f;
            pressSpaceTween = pressSpaceToStartCanvasGroup.DOFade(1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            pressSpaceToStartCanvasGroup.interactable = true;
            pressSpaceToStartCanvasGroup.blocksRaycasts = true;
        }
    }

    private void ShowCatergoryOptionsHandler()
    {
        //Fix this for returning to MM
        LerpIntroCameraFirstTime(); 
        ShowCategoryOptions(true);
        ShowPressSpaceToStart(false);
    }

    private void ShowCategoryOptions(bool play)
    {
        if (!play)
        {
            categoryTween?.Kill(); // Stop the category fade tween
            categoryCanvasGroup.alpha = 0f;
            categoryCanvasGroup.interactable = false;
            categoryCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            pressSpaceTween?.Kill();

            categoryCanvasGroup.alpha = 0f;
            categoryTween = categoryCanvasGroup.DOFade(1f, 1f).SetEase(Ease.InOutSine);
            categoryCanvasGroup.interactable = true;
            categoryCanvasGroup.blocksRaycasts = true;
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

    public void SlowDownTime(float value)
    {
        Time.timeScale = value;
    }

    public void NormalTime()
    {
        Time.timeScale = 1f;
    }
}
