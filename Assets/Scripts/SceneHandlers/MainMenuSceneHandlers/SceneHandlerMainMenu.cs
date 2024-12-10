using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class SceneHandlerMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TransitionControllerMainMenu transitionControllerMainMenu;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private SceneHandlerMainMenu sceneHandlerMainMenu;

    [Space]

    [SerializeField] private CinemachineVirtualCamera mmCamera1;
    [SerializeField] private Transform mmCameraStartPOS;
    [SerializeField] private Transform mmCameraEndPOS;
    [SerializeField] private float cameraDuration = 1f;

    [Header("Press Space To Start References")]
    [SerializeField] private CanvasGroup pressSpaceToStartCanvasGroup;
    [SerializeField] private AnimationCurve animationCurve0;
    [SerializeField] private float duration1 = 1f;
    private Tween alphaTween;

    [Header("Category References")]
    [SerializeField] private CanvasGroup categoriesCanvasGroup;
    [SerializeField] private AnimationCurve animationCurve2;
    [SerializeField] private float duration2 = 1f;

    private void Start()
    {
        mmCamera1.Priority = 1;
        mmCamera1.enabled = true;

        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Katana);
        categoriesCanvasGroup.alpha = 0;
        categoriesCanvasGroup.interactable = false;
        AnimatePressSpaceToStartCanvasGroup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LerpToCanvasOptions();
        }
    }

    public void LerpToCanvasOptions()
    {
        AnimateInCategoryCanvasGroup();
        sceneHandlerMainMenu.StopPressSpaceAnimation();

        mmCamera1.transform.position = mmCameraStartPOS.position;
        mmCamera1.transform.DOMove(mmCameraEndPOS.position, cameraDuration).SetEase(Ease.InOutSine);
    }

    public void SetCameraPriorityToNull()
    {
        mmCamera1.enabled = false;
        mmCamera1.Priority = 0;
    }

    public void StopPressSpaceAnimation()
    {
        if (alphaTween != null)
        {
            alphaTween.Kill();
            pressSpaceToStartCanvasGroup.alpha = 0;
        }
    }

    public void AnimatePressSpaceToStartCanvasGroup()
    {
       pressSpaceToStartCanvasGroup.alpha = 0;
       alphaTween = pressSpaceToStartCanvasGroup.DOFade(1, duration1).SetEase(animationCurve0).SetLoops(-1, LoopType.Yoyo);
       
    }

    public void AnimateInCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 0;
        categoriesCanvasGroup.DOFade(1, duration2).SetEase(animationCurve2);
        categoriesCanvasGroup.interactable = true;
    }

    public void AnimateOutCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 1;
        categoriesCanvasGroup.DOFade(0, duration2).SetEase(animationCurve2);
        categoriesCanvasGroup.interactable = false;
    }
}
