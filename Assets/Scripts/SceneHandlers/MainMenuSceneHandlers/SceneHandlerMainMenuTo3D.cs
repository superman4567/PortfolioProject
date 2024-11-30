using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using System.Collections;

public class SceneHandlerMainMenuTo3D : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent(EnumMainMenuChoices choice);
    public event OnSequenceCompletedEvent OnSequenceCompleted;

    [Header("Camera References")]
    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private SceneHandlerMainMenu sceneHandlerMainMenu;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private TimeScaleController timeScaleController;
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

    [Header("External References")]
    [SerializeField] private GameObject building;
    [SerializeField] private Material material;
    [Space]
    [SerializeField] private Volume volume;
    [SerializeField] private SplineFollower[] ravens;
    private Fog fog;
    private GradientSky gradientSky;

    private bool passedScreen = false;

    private void Start()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        material = building.GetComponent<MeshRenderer>().material;
        material.SetFloat("_BlackAmount", 0.5f);
        volume.profile.TryGet<Fog>(out fog);
        volume.profile.TryGet<GradientSky>(out gradientSky);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !passedScreen)
        {
            passedScreen = true;
            sceneHandlerMainMenu.AnimateInCategoryCanvasGroup();
            sceneHandlerMainMenu.StopPressSpaceAnimation();
            mmCamera.Priority = 0;
            camera0.Priority = 1;
        }
    }

    public IEnumerator CameraSequence()
    {
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Katana);
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.EntryThreeDArt);
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToCamera0());
    }

    private IEnumerator TransitionToCamera0()
    {
        ActivateCamera(camera0);
        sceneHandlerMainMenu.AnimateOutCategoryCanvasGroup();
        yield return new WaitForSeconds(transitionDuration1);
        StartCoroutine(TransitionToCamera1());
    }

    private IEnumerator TransitionToCamera1()
    {
        ActivateCamera(camera1);
        yield return new WaitForSeconds(transitionDuration2);
        StartCoroutine(TransitionToCamera2());
    }

    private IEnumerator TransitionToCamera2()
    {
        ActivateCamera(camera2);
        StartCoroutine(LerpFogColor());
        yield return new WaitForSeconds(transitionDuration3);
        StartCoroutine(TransitionToCamera3());    
    }

    private IEnumerator TransitionToCamera3()
    {
        ActivateCamera(camera3);
        yield return new WaitForSeconds(0.2f);

        foreach (var raven in ravens) 
        {
            yield return new WaitForSeconds(0.05f);
            raven.isLooping = true;
        }

        StartCoroutine(LerpMaterialProperty());
        StartCoroutine(LerpSkyColors());
        yield return new WaitForSeconds(loadsceneBuffer);
        SequenceComplete();
    }

    private IEnumerator LerpMaterialProperty()
    {
        float lerpDuration = 1.2f; // Adjust as needed
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

    private IEnumerator LerpFogColor()
    {
        float lerpDuration = 1.2f;
        Color startColor = new Color(113f / 255f, 113f / 255f, 113f / 255f);
        Color endColor = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / lerpDuration);
            fog.tint.value = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set
        fog.tint.value = endColor;
    }

    private IEnumerator LerpSkyColors()
    {
        float lerpDuration = 1.2f; // Adjust as needed
        Color startColorTop = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        Color startColorMid = new Color(34f / 255f, 34f / 255f, 34f / 255f);
        Color startColorBot = new Color(159f / 255f, 159f / 255f, 159f / 255f);

        Color endColorTop = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        Color endColorMid = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        Color endColorBot = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / lerpDuration;

            // Lerp each color component
            gradientSky.top.value = Color.Lerp(startColorTop, endColorTop, t);
            gradientSky.middle.value = Color.Lerp(startColorMid, endColorMid, t);
            gradientSky.bottom.value = Color.Lerp(startColorBot, endColorBot, t);

            // Update elapsed time and yield
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set
        gradientSky.top.value = endColorTop;
        gradientSky.middle.value = endColorMid;
        gradientSky.bottom.value = endColorBot;
    }

    private void ActivateCamera(CinemachineVirtualCamera camera)
    {
        camera0.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;

        camera.enabled = true;
    }

    private void SequenceComplete()
    {
        OnSequenceCompleted?.Invoke(EnumMainMenuChoices.ThreeDArt);
    }
}
