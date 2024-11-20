using DG.Tweening;
using UnityEngine;

public class SceneHandlerMainMenu : MonoBehaviour
{
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
        categoriesCanvasGroup.alpha = 0;
        
        AnimatePressSpaceToStartCanvasGroup();
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

        alphaTween = pressSpaceToStartCanvasGroup.DOFade(1, duration1)
            .SetEase(animationCurve0)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void AnimateInCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 0;

        categoriesCanvasGroup.DOFade(1, duration2)
            .SetEase(animationCurve2);
    }

    public void AnimateOutCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 1;

        categoriesCanvasGroup.DOFade(0, duration2)
            .SetEase(animationCurve2);
    }
}
