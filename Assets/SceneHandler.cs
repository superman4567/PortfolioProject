using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [Header("First Time Entering")]
    [SerializeField] private CanvasGroup pressSpaceToStartCanvasGroup;
    private Tween pressSpaceTween;
    [SerializeField] private Button pressSpaceToStartButton;

    [Space]
    [SerializeField] private CinemachineVirtualCamera introCamera;
    [SerializeField] private Transform startPosMM;
    [SerializeField] private Transform endPosMM;
    [SerializeField] private float transitionDuration;

    [Header("Entering")]
    [SerializeField] private CanvasGroup categoryCanvasGroup;
    private Tween categoryTween;
    [SerializeField] private GameObject[] CategoryButtons;

    void Start()
    {
        AssignOnclicks();
        ShowPressSpaceToStart(false);
        LerpCategoryOptions(true);

        Transform cameraTransform = introCamera.transform;
        cameraTransform.SetPositionAndRotation(startPosMM.position, startPosMM.rotation);
    }

    private void AssignOnclicks()
    {
        pressSpaceToStartButton.onClick.AddListener(ShowCatergoryOptions);
    }

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
        LerpCategoryOptions(false); // Show category options
    }

    private void LerpCategoryOptions(bool stopTween)
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

    public void LerpIntroCameraFirstTime()
    {
        Transform cameraTransform = introCamera.transform;

        cameraTransform.SetPositionAndRotation(startPosMM.position, startPosMM.rotation);
        cameraTransform.DOMove(endPosMM.position, transitionDuration).SetEase(Ease.InOutSine);
        cameraTransform.DORotateQuaternion(endPosMM.rotation, transitionDuration).SetEase(Ease.InOutSine);
    }
}
