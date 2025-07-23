using System.Collections;
using UnityEngine;

public class PostprocessManager : MonoBehaviour
{
    [SerializeField] private GameObject postProcessVolume;

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += OnModeSelected_Callback;
        ReturnButton.OnReturnToModeSelect += OnReturnToModeSelect_Callback;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= OnModeSelected_Callback;
        ReturnButton.OnReturnToModeSelect -= OnReturnToModeSelect_Callback;
    }

    void Start()
    {
        postProcessVolume.SetActive(false);
    }


    private void OnModeSelected_Callback(bool a)
    {
        StartCoroutine(SetPostProcess(true));
    }

    private void OnReturnToModeSelect_Callback()
    {
        StartCoroutine(SetPostProcess(false));
    }

    private IEnumerator SetPostProcess(bool enable)
    {
        yield return new WaitForSeconds(0.4f);
        postProcessVolume.SetActive(enable);
    }
}
