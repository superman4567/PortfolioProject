using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera introCamera;
    [SerializeField] private AnimationHandler animHandlerCharacter;
    [SerializeField] private AnimationHandler animHandlerBoss;
    [SerializeField] private CanvasGroup pressStartGroup;
    [SerializeField] private CanvasGroup categoryGroup;

    [SerializeField] private Transform camStart;
    [SerializeField] private Transform camEnd;
    [SerializeField] private Transform camDefaultStart;
    [SerializeField] private Transform camDefaultEnd;
    [SerializeField] private float transitionDuration;

    private CategoryManager categoryManager;
    private VFXManager vfxManager;

    private void Awake()
    {
        categoryManager = GetComponent<CategoryManager>();
        vfxManager = GetComponent<VFXManager>();
    }

    void Start()
    {
        FadeLoop(pressStartGroup, true);
        categoryGroup.alpha = 0f;
        PlayIdleAnims();
        introCamera.transform.SetPositionAndRotation(camStart.position, camStart.rotation);
    }

    public void OnPressStart()
    {
        FadeLoop(pressStartGroup, false);
        Fade(pressStartGroup, false);
        Fade(categoryGroup, true);
        MoveCameraTo(camStart, camEnd);
    }

    public void OnShowCategoies_Callbnck()
    {
        Fade(categoryGroup, true);
    }

    public void OnReturnAtMainMenu()
    {
        MoveCameraTo(camDefaultStart, camDefaultEnd);
    }

    public void PlayIdleAnims()
    {
        animHandlerCharacter.CrossFadeAnimation("AN_Character_Idle");
        animHandlerBoss.CrossFadeAnimation("AN_Boss_Idle");
    }

    private void MoveCameraTo(Transform beginPosition, Transform target)
    {
        Transform cam = introCamera.transform;
        cam.position = beginPosition.position;
        cam.DOMove(target.position, transitionDuration).SetEase(Ease.InOutSine);
        cam.DORotateQuaternion(target.rotation, transitionDuration).SetEase(Ease.InOutSine);
    }

    private void Fade(CanvasGroup group, bool fadeIn)
    {
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

    private void FadeLoop(CanvasGroup group, bool loop)
    {
        DOTween.Kill("PressSpaceLoop");
        if (loop)
        {
            group.DOFade(1f, 2f).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).SetId("PressSpaceLoop");
        }
    }
}
