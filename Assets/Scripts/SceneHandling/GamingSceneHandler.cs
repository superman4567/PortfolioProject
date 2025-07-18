using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GamingSceneHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera introCamera;
    [SerializeField] private AnimationHandler animHandlerCharacter;
    [SerializeField] private AnimationHandler animHandlerBoss;
    [SerializeField] private CanvasGroup categoryGroup;
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
        categoryGroup.alpha = 0f;
        PlayIdleAnims();
    }

    public void OnShowCategoies_Callbnck()
    {
        Fade(categoryGroup, true);
    }

    public void PlayIdleAnims()
    {
        animHandlerCharacter.CrossFadeAnimation("AN_Character_Idle");
        animHandlerBoss.CrossFadeAnimation("AN_Boss_Idle");
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
}
