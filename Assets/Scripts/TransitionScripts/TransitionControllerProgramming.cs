using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerProgramming : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewUXUI view;
    [SerializeField] private SceneHandlerProgramming sceneHandlerProgramming;

    const string TRANSITION_INTRO = "AN_Character_IntroTransition_Programming";
    const string TRANSITION_OUTRO = "AN_Character_OutroTransition_Programming";

    private void OnEnable()
    {
        view.OnBackToMainMenuButtonPressed += BackToMainMenuButtonClicked;
        model.OnAnimationCallback += OnAnimationCallBack;
    }

    private void OnDisable()
    {
        view.OnBackToMainMenuButtonPressed -= BackToMainMenuButtonClicked;
        model.OnAnimationCallback -= OnAnimationCallBack;
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

    //This is called based on the animation that is triggerd in the method above!
    private void OnAnimationCallBack(string eventName)
    {
        if (eventName != "AN_Character_OutroTransition_Programming")
            return;

        SceneManager.LoadScene("MainMenuScene");
    }
}
