using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerProgramming : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewProgramming view;

    [SerializeField] private SceneHandlerProgrammingIntro sceneHandlerIntroProgramming;
    [SerializeField] private SceneHandlerProgrammingOutro sceneHandlerOutroProgramming;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_Programming";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_Programming";

    private void OnEnable()
    {
        view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
        sceneHandlerIntroProgramming.OnIntroSequenceCompleted += ShowUI;
        sceneHandlerOutroProgramming.OnHideUI += HideUI;
        sceneHandlerOutroProgramming.OnOutroSequenceCompleted += LoadMainMenuScene;

    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        sceneHandlerIntroProgramming.OnIntroSequenceCompleted -= ShowUI;
        sceneHandlerOutroProgramming.OnHideUI -= HideUI;
        sceneHandlerOutroProgramming.OnOutroSequenceCompleted -= LoadMainMenuScene;

    }

    private void Start()
    {
        HandleIntro();
    }


    public void HandleIntro()
    {
        StartCoroutine(sceneHandlerIntroProgramming.CameraSequence());

        model.PlayTransitionAnimation(TRANSITION_INTRO, true);
    }

    public void BackToMainMenuButtonClicked()
    {
        StartCoroutine(sceneHandlerOutroProgramming.CameraSequence());

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
