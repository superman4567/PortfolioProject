using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SceneHandlerMainMenuToProgramming : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent(EnumMainMenuChoices choice);
    public event OnSequenceCompletedEvent OnSequenceCompleted;

    [Header("Camera References")]
    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private SceneHandlerMainMenu sceneHandlerMainMenu;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private LoadingOverlayHandler loadingOverlayHandler;
    [Space]
    
    [SerializeField] private CinemachineVirtualCamera camera0;
    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineVirtualCamera camera3;
    [Space]
    [SerializeField] private float transitionDuration1 = 0.5f;
    [SerializeField] private float transitionDuration2 = 0.5f;
    [SerializeField] private float transitionDuration3 = 0.5f;

    [Header("External References")]
    [SerializeField] private CanvasGroup loginCanvasGroup;
    [SerializeField] private Image logoFill;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private AnimationCurve customCurve;

    [Header("External References")]
    [SerializeField] private GameObject laptop;
    [SerializeField] private Material laptopMaterial;

    [Header("External References")]
    [SerializeField] private GameObject building;
    [SerializeField] private Material material;
    [Space]

    [SerializeField] private GameObject katana;
    [SerializeField] private Material materialKatana;
    [Space]

    [SerializeField] private Volume volume;

    private bool isActive = false;
    private bool stopCoroutines = false;

    private void Start()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;

        material = building.GetComponent<MeshRenderer>().material;
        laptopMaterial = laptop.GetComponent<MeshRenderer>().material;
        materialKatana = katana.GetComponent<SkinnedMeshRenderer>().materials[0];

        loginCanvasGroup.alpha = 0f;
        logoFill.fillAmount = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isActive)
        {
            SkipSequenceProgramming();
        }
    }

    public IEnumerator CameraSequence()
    {
        isActive = true;
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Katana);
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
        StartCoroutine(DissolveKatana());
        yield return new WaitForSeconds(transitionDuration3);
        StartCoroutine(TransitionToCamera3());
    }

    private IEnumerator TransitionToCamera3()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(camera3);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        StartCoroutine(LerpMaterialProperty());
        StartCoroutine(LerpLaptopCamera());
    }

    private IEnumerator LerpLaptopCamera()
    {
        var transposer = camera3.GetCinemachineComponent<CinemachineFramingTransposer>();

        float startValue = 1f;
        float endValue = 0.1f;
        float duration = 0.5f;

        transposer.m_CameraDistance = startValue;

        DOTween.To(
            () => transposer.m_CameraDistance,
            x => transposer.m_CameraDistance = x,
            endValue,
            duration
        ).SetEase(Ease.InSine);

        yield return new WaitForSeconds(transitionDuration3);

        LerpLoginCanvas();
    }

    private void LerpLoginCanvas()
    {
        laptopMaterial.SetFloat("_BlackAmount", 0);

        float startValue = 0f;
        float endValue = 1f;
        float duration = 3f;

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            UpdateCanvasGroup(startValue);
        }, endValue, 0.2f);

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            UpdateLogoAndText(startValue);
        }, endValue, duration)
         .SetEase(customCurve)
         .OnComplete(ScreenToBlack);
    }

    private void UpdateCanvasGroup(float value)
    {
        loginCanvasGroup.alpha = value;
    }

    private void UpdateLogoAndText(float value)
    {
       logoFill.fillAmount = value;
       loadingText.text = $"LOADING GITHUB PROJECTS  -  {Mathf.RoundToInt(value * 100) + "%"}";
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

    private IEnumerator DissolveKatana()
    {
        yield return new WaitForSeconds(0.7f);

        float lerpDuration = 1f;
        float elapsedTime = 0f;

        float startValue = 2.5f;
        float endValue = 0f;

        while (elapsedTime < lerpDuration)
        {
            float value = Mathf.Lerp(startValue, endValue, elapsedTime / lerpDuration);
            materialKatana.SetFloat("_ColorHeight", value);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        materialKatana.SetFloat("_ColorHeight", endValue);
    }

    private void ActivateCamera(CinemachineVirtualCamera camera)
    {
        camera0.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;

        camera.enabled = true;
    }

    private void ScreenToBlack()
    {
        float startValue = 1f;
        float endValue = 0f;

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            UpdateCanvasGroup(startValue);
        }, endValue, 0.4f)
        .OnComplete(SequenceComplete);
    }

    public void SkipSequenceProgramming()
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
        
        OnSequenceCompleted?.Invoke(EnumMainMenuChoices.Programming);
    }
}
