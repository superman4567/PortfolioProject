using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour 
{
    public event Action<string> OnAnimationCallBack;

    [SerializeField] private AnimationHandler animationHandler;

    private void Start()
    {
        animationHandler.PlayAnimationDirectly("AN_Character_MainMenu_Idle");
    }

    #region MainMenu

    public void AN_Character_OutroTransition_MainMenu_To_UXUI()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_MainMenu_To_UXUI");
    }

    public void AN_Character_OutroTransition_MainMenu_To_3D()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_MainMenu_To_3D");
    }

    public void AN_Character_OutroTransition_MainMenu_To_Programming()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_MainMenu_To_Programming");
    }

    public void AN_Character_OutroTransition_MainMenu_To_VFX()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_MainMenu_To_VFX");
    }

    #endregion

    #region UXUI

    public void AN_Character_IntroTransition_UXUI()
    {
        OnAnimationCallBack?.Invoke("AN_Character_IntroTransition_UXUI");
    }

    public void AN_Character_OutroTransition_UXUI()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_UXUI");
    }

    #endregion

    #region 3D

    public void AN_Character_IntroTransition_3D()
    {
        OnAnimationCallBack?.Invoke("AN_Character_IntroTransition_3D");
    }

    public void AN_Character_OutroTransition_3D()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_3D");
    }

    #endregion

    #region Programming

    public void AN_Character_IntroTransition_Programming()
    {
        OnAnimationCallBack?.Invoke("AN_Character_IntroTransition_Programming");
    }

    public void AN_Character_OutroTransition_Programming()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_Programming");
    }

    #endregion

    #region VFX

    public void AN_Character_IntroTransition_VFX()
    {
        OnAnimationCallBack?.Invoke("AN_Character_IntroTransition_VFX");
    }

    public void AN_Character_OutroTransition_VFX()
    {
        OnAnimationCallBack?.Invoke("AN_Character_OutroTransition_VFX");
    }

    #endregion

}
