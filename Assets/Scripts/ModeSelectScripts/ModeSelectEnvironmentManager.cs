using System.Collections;
using UnityEngine;

public class ModeSelectEnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject environment;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += DisableEnvironment_Callback;
        ReturnButton.OnReturnToModeSelect += EnableEnvironment_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= DisableEnvironment_Callback;
        ReturnButton.OnReturnToModeSelect -= EnableEnvironment_Callback;
    }

    void Start()
    {
        StartCoroutine(EnableEnvironment());
    }

    private void EnableEnvironment_Callback()
    {
        StartCoroutine(EnableEnvironment());
    }

    private void DisableEnvironment_Callback(bool isCorporate)
    {
        StartCoroutine(DisableEnvironment());
    }

    private IEnumerator DisableEnvironment()
    {
        yield return new WaitForSeconds(0.4f);
        environment.SetActive(false);
    }

    private IEnumerator EnableEnvironment()
    {
        yield return new WaitForSeconds(0.4f);
        environment.SetActive(true);
    }
}
