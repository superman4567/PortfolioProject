using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerMainMenu : MonoBehaviour
{
    [SerializeField] private TransitionModel model;
    [SerializeField] private TransitionViewMainMenu view;

    [Space]

    [SerializeField] private SceneHandlerMainMenuToUXUI sceneHandlerMainMenuToUXUI;
    [SerializeField] private SceneHandlerMainMenuTo3D sceneHandlerMainMenuTo3D;
    [SerializeField] private SceneHandlerMainMenuToProgramming sceneHandlerMainMenuToProgramming;
    [SerializeField] private SceneHandlerMainMenuToVFX sceneHandlerMainMenuToVFX;

    [Space]

    [SerializeField] private GameObject camerasUXUI;
    [SerializeField] private GameObject cameras3D;
    [SerializeField] private GameObject camerasProgramming;
    [SerializeField] private GameObject camerasVFX;

    const string TRANSITION_OUTRO_TO_UXUI =         "AN_Character_OutroTransition_To_UXUI";
    const string TRANSITION_OUTRO_TO_3D =           "AN_Character_OutroTransition_To_3D";
    const string TRANSITION_OUTRO_TO_Programming =  "AN_Character_OutroTransition_To_Programming";
    const string TRANSITION_OUTRO_TO_VFX =          "AN_Character_OutroTransition_To_VFX";

    private EnumMainMenuChoices currentScene;

    private void OnEnable()
    {
        view.OnMainMenuButtonPressed += MainMenuButtonClicked;

        sceneHandlerMainMenuToUXUI.OnSequenceCompleted += LoadScene;
        sceneHandlerMainMenuTo3D.OnSequenceCompleted += LoadScene;
        sceneHandlerMainMenuToProgramming.OnSequenceCompleted += LoadScene;
        sceneHandlerMainMenuToVFX.OnSequenceCompleted += LoadScene;
    }

    private void OnDisable()
    {
        view.OnMainMenuButtonPressed -= MainMenuButtonClicked;

        sceneHandlerMainMenuToUXUI.OnSequenceCompleted -= LoadScene;
        sceneHandlerMainMenuTo3D.OnSequenceCompleted -= LoadScene;
        sceneHandlerMainMenuToProgramming.OnSequenceCompleted -= LoadScene;
        sceneHandlerMainMenuToVFX.OnSequenceCompleted -= LoadScene;
    }

    public void MainMenuButtonClicked(EnumMainMenuChoices choice)
    {
        currentScene = choice;

        switch (choice)
        {
            case EnumMainMenuChoices.UXUI:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_UXUI, true);
                StartCoroutine(sceneHandlerMainMenuToUXUI.CameraSequence());

                camerasUXUI.SetActive(true);
                cameras3D.SetActive(false);
                camerasProgramming.SetActive(false);
                camerasVFX.SetActive(false);
                break;
            case EnumMainMenuChoices.ThreeDArt:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_3D, true);
                StartCoroutine(sceneHandlerMainMenuTo3D.CameraSequence());

                camerasUXUI.SetActive(false);
                cameras3D.SetActive(true);
                camerasProgramming.SetActive(false);
                camerasVFX.SetActive(false);
                break;
            case EnumMainMenuChoices.Programming:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_Programming, true);
                StartCoroutine(sceneHandlerMainMenuToProgramming.CameraSequence());

                camerasUXUI.SetActive(false);
                cameras3D.SetActive(false);
                camerasProgramming.SetActive(true);
                camerasVFX.SetActive(false);
                break;
            case EnumMainMenuChoices.VFX:
                model.PlayTransitionAnimation(TRANSITION_OUTRO_TO_VFX, true);
                StartCoroutine(sceneHandlerMainMenuToVFX.CameraSequence());

                camerasUXUI.SetActive(false);
                cameras3D.SetActive(false);
                camerasProgramming.SetActive(false);
                camerasVFX.SetActive(true);
                break;
        }
    }

    private void LoadScene(EnumMainMenuChoices choice)
    {
        string name = choice switch
        {
            EnumMainMenuChoices.MainMenu => "MainMenuScene",
            EnumMainMenuChoices.UXUI => "UXUIScene",
            EnumMainMenuChoices.ThreeDArt => "3DARTScene",
            EnumMainMenuChoices.Programming => "ProgrammingScene",
            EnumMainMenuChoices.VFX => "VFXScene",

            _ => throw new System.NotImplementedException(),
        };

        SceneManager.LoadScene(name);

        Debug.Log(name);
    }
}
