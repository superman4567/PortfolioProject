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
    [Space]
    [Space]
    [SerializeField] private Transform firstTimeStartPosMM;
    [SerializeField] private Transform firstTimeEndPosMM;
    [Space]
    [SerializeField] private Transform defaultTimeStartPosMM;
    [SerializeField] private Transform defaultTimeEndPosMM;
    [Space]
    [SerializeField] private float transitionDuration;

    [Header("Catergory Buttons")]
    [SerializeField] private Button buttonUXUI;
    [SerializeField] private PlayableDirector playableDirectorUXUI;
    [SerializeField] private CanvasGroup canvasgroupUXUI;
    [SerializeField] private Button backButtonUXUI;
    [Space]
    [SerializeField] private Button button3D;
    [SerializeField] private PlayableDirector playableDirector3D;
    [SerializeField] private CanvasGroup canvasgroup3D;
    [SerializeField] private Button backButton3D;
    [Space]
    [SerializeField] private Button buttonProgramming;
    [SerializeField] private PlayableDirector playableDirectorProgramming;
    [SerializeField] private CanvasGroup canvasgroupProgramming;
    [SerializeField] private Button backButtonProgramming;
    [Space]
    [SerializeField] private Button buttongProfile;
    [SerializeField] private CanvasGroup canvasgroupProfile;
    [SerializeField] private Button backButtonProfile;
    [Space]
    [SerializeField] private Button buttonContact;
    [SerializeField] private CanvasGroup canvasgroupContact;
    [SerializeField] private Button backButtonContact;
    [Space]
    [SerializeField] private PlayableDirector playableDirectorReturnToMainMenu;

    [Header("VFX References")]
    [SerializeField] private GameObject[] particleSystems;

    [Header("Black bars")]
    [SerializeField] private RectTransform topbar;
    [SerializeField] private RectTransform botbar;

    void Start()
    {
        OnClickAssigning();
        ShowCategoryOptions(false);

        // Looping UI animation
        pressSpaceToStartCanvasGroup.DOFade(1f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine)
            .SetId("PressSpaceLoop");

        // Initialize camera position
        Transform cameraTransform = introCamera.transform;
        cameraTransform.SetPositionAndRotation(firstTimeStartPosMM.position, firstTimeStartPosMM.rotation);

        // Play idle animations
        PlayIdleAnims();
    }

    public void PlayIdleAnims()
    {
        animHandlerCharacter.CrossFadeAnimation("AN_Character_Idle");
        animHandlerBoss.CrossFadeAnimation("AN_Boss_Idle");
    }

    #region OnClick Assignment

    private void OnClickAssigning()
    {
        pressSpaceToStartButton.onClick.AddListener(ShowCategoryOptionsHandler);

        buttonUXUI.onClick.AddListener(ShowUXUIHandler);
        button3D.onClick.AddListener(Show3DHandler);
        buttonProgramming.onClick.AddListener(ShowProgrammingHandler);
        buttongProfile.onClick.AddListener(ShowProfileHandler);
        buttonContact.onClick.AddListener(ShowContactHandler);

        backButtonUXUI.onClick.AddListener(() => BackToMainMenu(canvasgroupUXUI));
        backButton3D.onClick.AddListener(() => BackToMainMenu(canvasgroup3D));
        backButtonProgramming.onClick.AddListener(() => BackToMainMenu(canvasgroupProgramming));
        backButtonProfile.onClick.AddListener(() => BackToMainMenu(canvasgroupProfile));
        backButtonContact.onClick.AddListener(() => BackToMainMenu(canvasgroupContact));
    }

    #endregion

    #region UI Handlers

    private void ShowUXUIHandler()
    {
        playableDirectorUXUI.Play();
        ShowCategoryOptions(false);
        ToggleBlackBars(true);
    }

    public void ShowUXUI()
    {
        FadeInCanvasGroup(canvasgroupUXUI, "UXUIFadeIn");
    }

    private void Show3DHandler()
    {
        playableDirector3D.Play();
        ShowCategoryOptions(false);
        ToggleBlackBars(true);
    }

    public void Show3D()
    {
        FadeInCanvasGroup(canvasgroup3D, "3DFadeIn");
    }

    private void ShowProgrammingHandler()
    {
        playableDirectorProgramming.Play();
        ShowCategoryOptions(false);
        ToggleBlackBars(true);
    }

    public void ShowProgramming()
    {
        FadeInCanvasGroup(canvasgroupProgramming, "ProgrammingFadeIn");
    }

    private void ShowProfileHandler()
    {
        ShowProfile();
        ShowCategoryOptions(false);
        ToggleBlackBars(true);
    }

    private void ShowProfile()
    {
        FadeInCanvasGroup(canvasgroupProfile, "ProfileFadeIn");
    }

    private void ShowContactHandler()
    {
        ShowContact();
        ShowCategoryOptions(false);
        ToggleBlackBars(true);
    }

    private void ShowContact()
    {
        FadeInCanvasGroup(canvasgroupContact, "ContactFadeIn");
    }

    private void BackToMainMenu(CanvasGroup canvasGroup)
    {
        FadeOutCanvasGroup(canvasGroup, $"{canvasGroup.name}FadeOut");
        DisableAllVFX();
        ToggleBlackBars(true);

        playableDirectorReturnToMainMenu.Play();
    }

    #endregion

    #region Animation Utility

    private Tween FadeInCanvasGroup(CanvasGroup canvasGroup, string tweenId)
    {
        canvasGroup.alpha = 0f;
        return canvasGroup.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .SetId(tweenId)
            .OnStart(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    private void FadeOutCanvasGroup(CanvasGroup canvasGroup, string tweenId)
    {
        canvasGroup.DOFade(0f, .2f)
            .SetEase(Ease.InOutSine)
            .SetId(tweenId)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }

    private void LerpIntroCameraFirstTime()
    {
        Transform cameraTransform = introCamera.transform;

        cameraTransform.SetPositionAndRotation(firstTimeStartPosMM.position, firstTimeStartPosMM.rotation);
        cameraTransform.DOMove(firstTimeEndPosMM.position, transitionDuration).SetEase(Ease.InOutSine).SetId("CameraMoveFirstTime");
        cameraTransform.DORotateQuaternion(firstTimeEndPosMM.rotation, transitionDuration).SetEase(Ease.InOutSine).SetId("CameraRotateFirstTime");
    }

    #endregion

    #region VFX and Misc

    private void DisableAllVFX()
    {
        foreach (var vfx in particleSystems)
        {
            vfx.SetActive(false);
        }
    }

    private void HidePressSpaceToStart()
    {
        DOTween.Kill("PressSpaceLoop");
        FadeOutCanvasGroup(pressSpaceToStartCanvasGroup, "PressSpaceFadeOut");
    }

    private void ShowCategoryOptionsHandler()
    {
        ShowCategoryOptions(true);
        LerpIntroCameraFirstTime();
        HidePressSpaceToStart();
    }

    public void ShowCategoryOptions(bool show)
    {
        if (!show)
        {
            FadeOutCanvasGroup(categoryCanvasGroup, "CategoryOptionsFadeOut");
        }
        else
        {
            FadeInCanvasGroup(categoryCanvasGroup, "CategoryOptionsFadeIn");
        }
    }

    public void ToggleBlackBars(bool show)
    {
        if (show)
        {
            // Tween to expand the height to 120
            topbar.DOSizeDelta(new Vector2(topbar.sizeDelta.x, 80f), 0.5f);
            botbar.DOSizeDelta(new Vector2(botbar.sizeDelta.x, 80f), 0.5f);
        }
        else
        {
            // Tween to collapse the height to 0
            topbar.DOSizeDelta(new Vector2(topbar.sizeDelta.x, 0), 0.5f);
            botbar.DOSizeDelta(new Vector2(botbar.sizeDelta.x, 0), 0.5f);
        }
    }

    public void SlowDownTime(float value)
    {
        Time.timeScale = value;
    }

    public void NormalTime()
    {
        Time.timeScale = 1f;
    }

    #endregion
}
