using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;
using UnityEngine.Rendering;

public class SceneHandlerMainMenu : MonoBehaviour
{
    [Header("Press Space To Start References")]
    [SerializeField] private CanvasGroup pressSpaceToStartCanvasGroup;
    [SerializeField] private AnimationCurve animationCurve0;
    [SerializeField] private float duration1 = 1f;
    private Tween alphaTween;

    [Header("Category References")]
    [SerializeField] private CanvasGroup categoriesCanvasGroup;
    [SerializeField] private AnimationCurve animationCurve2;
    [SerializeField] private float duration2 = 1f;

    [Header("Camera References")]
    [SerializeField] private CinemachineBrain cineBrain;
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

    [Header("External References")]
    [SerializeField] private GameObject building;
    [SerializeField] private Material material;
    [Space]
    [SerializeField] private Volume volume;
    private Fog fog;
    private GradientSky gradientSky;

    private bool passedScreen = false;

    private void Start()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;

        categoriesCanvasGroup.alpha = 0f;

        AnimatePressSpaceToStartCanvasGroup();

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

            alphaTween.Kill();
            pressSpaceToStartCanvasGroup.alpha = 0;

            AnimateInCategoryCanvasGroup();

            mmCamera.Priority = 0;
            camera0.Priority = 1;
        }
    }

    private void AnimatePressSpaceToStartCanvasGroup()
    {
        pressSpaceToStartCanvasGroup.alpha = 0;
        
        alphaTween = pressSpaceToStartCanvasGroup.DOFade(1, duration1)
            .SetEase(animationCurve0)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void AnimateInCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 0;

        categoriesCanvasGroup.DOFade(1, duration2)
            .SetEase(animationCurve2); 
    }

    private void AnimateOutCategoryCanvasGroup()
    {
        categoriesCanvasGroup.alpha = 1;

        categoriesCanvasGroup.DOFade(0, duration2)
            .SetEase(animationCurve2);
    }

    public IEnumerator CameraSequence()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

        yield return StartCoroutine(TransitionToCamera0());
    }

    private IEnumerator TransitionToCamera0()
    {
        ActivateCamera(camera0);

        AnimateOutCategoryCanvasGroup();

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
        StartCoroutine(LerpSkyColors());

        yield return new WaitForSeconds(transitionDuration3);

        TransitionToCamera3();
    }

    private void TransitionToCamera3()
    {
        ActivateCamera(camera3);

        StartCoroutine(LerpMaterialProperty());
        
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

    private IEnumerator LerpFogColor()
    {
        float lerpDuration = 2f;
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
        float lerpDuration = 2f; // Adjust as needed
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
}
