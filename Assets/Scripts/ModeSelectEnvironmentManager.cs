using UnityEngine;

public class ModeSelectEnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject environment;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += DisableEnvironment_Callback;
        ReturnButton.OnReturnToModeSelect += EnableEnvironment;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= DisableEnvironment_Callback;
        ReturnButton.OnReturnToModeSelect -= EnableEnvironment;
    }

    void Start()
    {
        EnableEnvironment();
    }

    private void DisableEnvironment_Callback(bool isCorporate)
    {
        DisableEnvironment();
    }

    private void DisableEnvironment()
    {
        environment.SetActive(false);
    }

    private void EnableEnvironment()
    {
        environment.SetActive(true);
    }
}
