using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerUXUI : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewUXUI view;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_UXUI";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_UXUI";

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
