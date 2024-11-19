using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimationDirectly(string animationName, int layer = 0)
    {
        if (animator != null)
        {
            animator.Play(animationName, layer);
        }
    }

    public void CrossFadeAnimation(string animationName, float transitionDuration = 0.1f, int layer = 0)
    {
        if (animator != null)
        {
            animator.CrossFade(animationName, transitionDuration, layer);
        }
    }
}
