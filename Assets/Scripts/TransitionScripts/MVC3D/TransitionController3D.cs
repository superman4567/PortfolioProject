using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController3D : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionView3D view;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_3D";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_3D";

    private void OnEnable()
    {
        //view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
    }

    private void OnDisable()
    {
        //view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
    }

    private void ShowUI()
    {
        view.ShowUI();
    }

    private void HideUI()
    {
        view.HideUI();
    }
}
