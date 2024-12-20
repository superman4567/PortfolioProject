using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using DG.Tweening;

public class SceneHandlerMainMenuToVFX : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent(EnumMainMenuChoices choice);
    public event OnSequenceCompletedEvent OnSequenceCompleted;

    [Header("Camera References")]
    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private SceneHandlerMainMenu sceneHandlerMainMenu;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private TimeScaleController timeScaleController;
    [SerializeField] private LoadingOverlayHandler loadingOverlayHandler;
    [Space]
    [SerializeField] private CinemachineVirtualCamera mmCamera;
    [SerializeField] private CinemachineVirtualCamera camera0;
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineVirtualCamera camera3;
    [Space]
    [SerializeField] private float transitionDuration1 = 0.5f;
    [SerializeField] private float transitionDuration2 = 0.5f;
    [SerializeField] private float transitionDuration3 = 0.5f;
    [SerializeField] private float transitionDuration4 = 0.5f;

    [Header("External References")]
    [SerializeField] private GameObject building;
    [SerializeField] private Material material;
    [Space]
    [SerializeField] private Volume volume;

    private bool isActive = false;
    private bool stopCoroutines = false;

    private void Start()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        material = building.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isActive)
        {
            SkipSequenceVFX();
        }
    }

    public IEnumerator CameraSequence()
    {
        if (stopCoroutines) yield break;

        isActive= true;
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Katana);
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.EntryVFX);
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToCamera0());
    }

    private IEnumerator TransitionToCamera0()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera0);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        sceneHandlerMainMenu.AnimateOutCategoryCanvasGroup();
        yield return new WaitForSeconds(transitionDuration1);
        StartCoroutine(TransitionToCamera1());
    }

    private IEnumerator TransitionToCamera1()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera1);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        yield return new WaitForSeconds(transitionDuration2);
        StartCoroutine(TransitionToCamera2());
    }

    private IEnumerator TransitionToCamera2()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera2);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        yield return new WaitForSeconds(transitionDuration3);
        StartCoroutine(TransitionToCamera3());
    }

    private IEnumerator TransitionToCamera3()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera3);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        StartCoroutine(LerpMaterialProperty());
        yield return new WaitForSeconds(transitionDuration4);
        SequenceComplete();
    }

    private IEnumerator LerpMaterialProperty()
    {
        float lerpDuration = 2f; // Adjust as needed
        float startValue = 0.5f;
        float endValue = -40f;
        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            float value = Mathf.Lerp(startValue, endValue, elapsedTime / lerpDuration);
            material.SetFloat("_BlackAmount", value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_BlackAmount", endValue); // Ensure the final value is set
    }

    private void ActivateCamera(CinemachineVirtualCamera camera)
    {
        camera0.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;

        camera.enabled = true;
    }

    public void SkipSequenceVFX()
    {
        if (!isActive)
            return;

        isActive = false;
        stopCoroutines = true;
        StopAllCoroutines();
        DOTween.KillAll();
        SequenceComplete();
    }

    private void SequenceComplete()
    {
        OnSequenceCompleted?.Invoke(EnumMainMenuChoices.VFX);
    }
}
