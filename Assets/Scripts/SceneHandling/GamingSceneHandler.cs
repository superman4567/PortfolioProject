using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GamingSceneHandler : MonoBehaviour
{
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

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += OnModeSelected_Callback;
        ReturnButton.OnReturnToModeSelect += OnReturnToModeSelect_Callback;
        ReturnButton.OnReturnTocategory += OnReturnTocategory_Callback;
        CategoryManager.OnCategoryClicked += OnCategoryClicked_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= OnModeSelected_Callback;
        ReturnButton.OnReturnToModeSelect -= OnReturnToModeSelect_Callback;
        ReturnButton.OnReturnTocategory -= OnReturnTocategory_Callback;
        CategoryManager.OnCategoryClicked -= OnCategoryClicked_Callback;
    }

    void Start()
    {
        categoryGroup.alpha = 0f;
        categoryGroup.interactable = false;
        categoryGroup.blocksRaycasts = false;
        PlayIdleAnims();
    }

    public void OnModeSelected_Callback(bool isCorperate)
    {
        if (isCorperate)
            return;

        StartCoroutine(Fade(categoryGroup, true));
    }

    public void OnReturnTocategory_Callback()
    {
        StartCoroutine(Fade(categoryGroup, true));
    }

    public void OnReturnToModeSelect_Callback()
    {
        StartCoroutine(Fade(categoryGroup, false));
    }

    public void OnCategoryClicked_Callback()
    {
        StartCoroutine(Fade(categoryGroup, false));
    }

    public void PlayIdleAnims()
    {
        animHandlerCharacter.CrossFadeAnimation("AN_Character_Idle");
        animHandlerBoss.CrossFadeAnimation("AN_Boss_Idle");
    }

    private IEnumerator Fade(CanvasGroup group, bool fadeIn)
    {
        yield return new WaitForSeconds(0.4f);

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
