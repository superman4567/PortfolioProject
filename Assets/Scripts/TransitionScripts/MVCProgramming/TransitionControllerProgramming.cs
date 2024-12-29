using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerProgramming : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewProgramming view;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_Programming";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_Programming";

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
