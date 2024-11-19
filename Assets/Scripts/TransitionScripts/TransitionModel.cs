using System;
using UnityEngine;

public class TransitionModel : MonoBehaviour
{
    //Animator aanroepen
    public event Action<string> OnAnimationCallback 
    { 
        add => animationEventHandler.OnAnimationCallBack += value; 
        remove => animationEventHandler.OnAnimationCallBack -= value; 
    }

    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private AnimationEventHandler animationEventHandler;

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
