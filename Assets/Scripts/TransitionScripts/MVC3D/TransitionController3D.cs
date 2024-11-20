using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController3D : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionView3D view;

    [SerializeField] private SceneHandler3DIntro sceneHandlerIntro3D;
    [SerializeField] private SceneHandler3DOutro sceneHandlerOutro3D;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_3D";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_3D";

    private void OnEnable()
    {
        view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
        sceneHandlerOutro3D.OnSequenceCompleted += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        sceneHandlerOutro3D.OnSequenceCompleted += LoadMainMenuScene;
    }

    private void Start()
    {
        HandleIntro();
    }

    public void HandleIntro()
    {
        StartCoroutine(sceneHandlerIntro3D.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, true);
    }

    public void BackToMainMenuButtonClicked()
    {
        StartCoroutine(sceneHandlerOutro3D.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_OUTRO, true);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
