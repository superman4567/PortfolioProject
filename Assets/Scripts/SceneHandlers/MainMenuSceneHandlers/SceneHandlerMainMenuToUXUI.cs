using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SceneHandlerMainMenuToUXUI : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent (EnumMainMenuChoices choice);
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
    [Space]
    [SerializeField] private float loadsceneBuffer = 0.5f;
    [SerializeField] private AnimationCurve customCurve;

    [Header("External References")]
    [SerializeField] private GameObject building;
    [SerializeField] private Material materialEnvironment;

    [Space]
    [SerializeField] private GameObject laptop;
    [SerializeField] private Material materialLaptop;

    [Space]
    [SerializeField] private GameObject character;
    [SerializeField] private Material materialCharacterDissolve;

    [Space]
    [SerializeField] private RectTransform brightImage;

    [Space]
    [SerializeField] private Volume volume;

    private bool isActive = false;
    private bool stopCoroutines = false;

    private void Start()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;

        materialEnvironment = building.GetComponent<MeshRenderer>().material;
        materialLaptop = laptop.GetComponent<MeshRenderer>().material;
        materialCharacterDissolve = character.GetComponent<SkinnedMeshRenderer>().materials[2];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isActive)
        {
            SkipSequenceUXUI(); 
        }
    }

    public IEnumerator CameraSequence()
    {
        isActive = true;
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Nothing);
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.EntryUXUI);
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToCamera0());
    }

    private IEnumerator TransitionToCamera0()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera0);
        sceneHandlerMainMenu.AnimateOutCategoryCanvasGroup();
        loadingOverlayHandler.FillLoadingAmount(.25f);
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
        yield return new WaitForSeconds(loadsceneBuffer);
        SequenceComplete();
    }

    private IEnumerator TransitionToCamera3()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera3);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        StartCoroutine(LerpMaterialProperty());
        StartCoroutine(LerpCharacterDissolve());
        StartCoroutine(LerpSprite());
        LerpCamera3();
    }

    private void LerpCamera3()
    {
        var transposer = camera3.GetCinemachineComponent<CinemachineFramingTransposer>();

        float startValue = 3f;
        float endValue = 5f;
        float duration = .8f;

        transposer.m_CameraDistance = startValue;

        DOTween.To(
            () => transposer.m_CameraDistance,
            x => transposer.m_CameraDistance = x,
            endValue,
            duration
        ).SetEase(customCurve);
    }

    private IEnumerator LerpMaterialProperty()
    {
        float lerpDuration = 2f;
        float elapsedTime = 0f;

        float startValueBuilding = 0.5f;
        float endValueBuilding = -40f;

        float startValueLapatop = 1f;
        float endValueLaptop = -40f;

        while (elapsedTime < lerpDuration)
        {
            float valueBuilding = Mathf.Lerp(startValueBuilding, endValueBuilding, elapsedTime / lerpDuration);
            materialEnvironment.SetFloat("_BlackAmount", valueBuilding);

            float valueLaptop = Mathf.Lerp(startValueLapatop, endValueLaptop, elapsedTime / lerpDuration);
            materialLaptop.SetFloat("_BlackAmount", valueLaptop);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        materialEnvironment.SetFloat("_BlackAmount", endValueBuilding);
        materialLaptop.SetFloat("_BlackAmount", endValueLaptop);
    }

    private IEnumerator LerpCharacterDissolve()
    {
        yield return new WaitForSeconds(1f);

        float lerpDuration = 1f;
        float elapsedTime = 0f;

        float startValue = 2.5f;
        float endValue = 6f;

        while (elapsedTime < lerpDuration)
        {
            float value = Mathf.Lerp(startValue, endValue, elapsedTime / lerpDuration);
            materialCharacterDissolve.SetFloat("_ColorHeight", value);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        materialCharacterDissolve.SetFloat("_ColorHeight", endValue);
    }

    private IEnumerator LerpSprite()
    {
        yield return new WaitForSeconds(1.4f);

        float lerpDuration = 0.3f;
        float elapsedTime = 0f;

        // Start and end dimensions
        float startWidth = 0f;
        float endWidth = 4096f;
        float startHeight = 0f;
        float endHeight = 4096f;


        while (elapsedTime < lerpDuration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / lerpDuration;

            // Lerp the width and height
            float currentWidth = Mathf.Lerp(startWidth, endWidth, t);
            float currentHeight = Mathf.Lerp(startHeight, endHeight, t);

            // Apply the new dimensions
            brightImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            brightImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is set to the target dimensions
        brightImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, endWidth);
        brightImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, endHeight);
    }

    private void ActivateCamera(CinemachineVirtualCamera camera)
    {
        camera0.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;

        camera.enabled = true;
    }

    public void SkipSequenceUXUI()
    {
        if (!isActive)
            return;

        isActive = true;
        stopCoroutines = true;
        StopAllCoroutines();
        DOTween.KillAll();
        SequenceComplete();
    }

    private void SequenceComplete()
    {
        loadingOverlayHandler.FillLoadingAmount(1f);
        OnSequenceCompleted?.Invoke(EnumMainMenuChoices.UXUI);
    }
}
