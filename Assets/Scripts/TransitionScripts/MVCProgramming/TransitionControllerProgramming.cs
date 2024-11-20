using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerProgramming : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewUXUI view;
    [SerializeField] private SceneHandlerMainMenuToProgramming sceneHandlerProgramming;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_Programming";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_Programming";

    private void OnEnable()
    {
        view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
    }

    public void HandleIntro()
    {
        //StartCoroutine(sceneHandlerProgramming.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, true);
    }

    public void BackToMainMenuButtonClicked()
    {
        //StartCoroutine(sceneHandlerProgramming.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_OUTRO, true);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
