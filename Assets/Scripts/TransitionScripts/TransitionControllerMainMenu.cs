using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerMainMenu : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewMainMenu view;
    [SerializeField] private SceneHandlerMainMenu sceneHandlerMainMenu;

    const string TRANSITION_OUTRO_TO_UXUI =         "AN_Character_OutroTransition_To_UXUI";
    const string TRANSITION_OUTRO_TO_3D =           "AN_Character_OutroTransition_To_3D";
    const string TRANSITION_OUTRO_TO_Programming =  "AN_Character_OutroTransition_To_Programming";
    const string TRANSITION_OUTRO_TO_VFX =          "AN_Character_OutroTransition_To_VFX";

    private EnumMainMenuChoices currentScene;

    private void OnEnable()
    {
        view.OnMainMenuButtonPressed += MainMenuButtonClicked;
        model.OnAnimationCallback += OnAnimationCallBack;
    }

    private void OnDisable()
    {
        view.OnMainMenuButtonPressed -= MainMenuButtonClicked;
        model.OnAnimationCallback -= OnAnimationCallBack;
    }

    public void MainMenuButtonClicked(EnumMainMenuChoices choice)
    {
        StartCoroutine(sceneHandlerMainMenu.CameraSequence());

        currentScene = choice;

        switch (choice)
        {
            case EnumMainMenuChoices.UXUI:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_UXUI, true);
                break;
            case EnumMainMenuChoices.ThreeDArt:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_3D, true);
                break;
            case EnumMainMenuChoices.Programming:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_Programming, true);
                break;
            case EnumMainMenuChoices.VFX:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_VFX, true);
                break;
        }
    }

    //This is called based on the animation that is triggerd in the method above!
    private void OnAnimationCallBack(string eventName)
    {
        if (eventName != "AnimationEnd") 
            return;

        string name = currentScene switch
        {
            EnumMainMenuChoices.MainMenu => "MainMenuScene",
            EnumMainMenuChoices.UXUI => "UXUIScene",
            EnumMainMenuChoices.ThreeDArt => "3DARTScene",
            EnumMainMenuChoices.Programming => "ProgrammingScene",
            EnumMainMenuChoices.VFX => "VFXScene",

            _ => throw new System.NotImplementedException(),
        };

        SceneManager.LoadScene(name);
    }
}
