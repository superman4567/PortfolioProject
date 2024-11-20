using System;
using UnityEngine;

public class TransitionModel : MonoBehaviour
{
    [SerializeField] private AnimationHandler animationHandler;

    public void PlayTransitionAnimation(string animationName, bool interpolate)
    {
        if (!interpolate)
        {
            animationHandler.PlayAnimationDirectly(animationName);
            return;
        }
       
        animationHandler.CrossFadeAnimation(animationName);
    }
}
