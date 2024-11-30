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
        sceneHandlerIntro3D.OnIntroSequenceCompleted += ShowUI;
        sceneHandlerOutro3D.OnHideUI += HideUI;
        sceneHandlerOutro3D.OnOutroSequenceCompleted += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        sceneHandlerIntro3D.OnIntroSequenceCompleted -= ShowUI;
        sceneHandlerOutro3D.OnHideUI -= HideUI;
        sceneHandlerOutro3D.OnOutroSequenceCompleted -= LoadMainMenuScene;
    }

    private void Start()
    {
        HandleIntro();
    }

    public void HandleIntro()
    {
        StartCoroutine(sceneHandlerIntro3D.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, false);
    }

    public void BackToMainMenuButtonClicked()
    {
        StartCoroutine(sceneHandlerOutro3D.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_OUTRO, false);
    }

    private void ShowUI()
    {
        view.ShowUI();
    }

    private void HideUI()
    {
        view.HideUI();
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
