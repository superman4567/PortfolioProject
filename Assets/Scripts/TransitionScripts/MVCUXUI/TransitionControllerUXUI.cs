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
        sceneHandlerIntroUXUI.OnIntroSequenceCompleted += ShowUI;
        sceneHandlerOutroUXUI.OnHideUI += HideUI;
        sceneHandlerOutroUXUI.OnOutroSequenceCompleted += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        sceneHandlerIntroUXUI.OnIntroSequenceCompleted -= ShowUI;
        sceneHandlerOutroUXUI.OnHideUI -= HideUI;
        sceneHandlerOutroUXUI.OnOutroSequenceCompleted -= LoadMainMenuScene;
    }

    private void Start()
    {
        HandleIntro();
    }

    public void HandleIntro()
    {
        StartCoroutine(sceneHandlerIntroUXUI.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, false);
    }

    public void BackToMainMenuButtonClicked()
    {
        sceneHandlerOutroUXUI.CameraSequence();

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
