using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerUXUI : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewUXUI view;

    [SerializeField] private SceneHandlerUXUIIntro sceneHandlerIntroUXUI;
    [SerializeField] private SceneHandlerUXUIOutro sceneHandlerOutroUXUI;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_UXUI";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_UXUI";

    private void OnEnable()
    {
        view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
        sceneHandlerOutroUXUI.OnSequenceCompleted += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        sceneHandlerOutroUXUI.OnSequenceCompleted -= LoadMainMenuScene;
    }

    private void Start()
    {
        HandleIntro();
    }

    public void HandleIntro()
    {
        StartCoroutine(sceneHandlerIntroUXUI.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, true);
    }

    public void BackToMainMenuButtonClicked()
    {
        StartCoroutine(sceneHandlerOutroUXUI.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_OUTRO, true);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
