using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SceneHandler3DIntro : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent();
    public event OnSequenceCompletedEvent OnIntroSequenceCompleted;

    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private TimeScaleController timeScaleController;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private LoadingOverlayHandler loadingOverlayHandler;

    [Space]

    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;
    [SerializeField] private CinemachineVirtualCamera cam3;
    [SerializeField] private CinemachineVirtualCamera cam4;

    [Space]
    public Transform cam1Start;
    public Transform cam1End;

    [Space]
    public Transform cam2Start;
    public Transform cam2End;
    [Space]
    
    public Transform cam3Start;
    public Transform cam3End;
    [Space]
    
    public Transform cam4Start;
    public Transform cam4End;

    [Space]

    public float delayBeforeShot = 0.4f;
    public GameObject bullet;
    public Transform bulletStartPOS;
    public Transform bulletEndPOS;

    [Space]

    [SerializeField] private GameObject environment_UXUI_Intro;
    [SerializeField] private GameObject environment_UXUI_Outro;

    [Space]

    [SerializeField] private GameObject introCameras;
    [SerializeField] private GameObject outroCameras;

    [Space]

    [SerializeField] private float delayPart1 = 1f;
    [SerializeField] private float delayPart2 = 1f;
    [SerializeField] private float delayPart3 = 1f;
    [SerializeField] private float delayPart4 = 1f;

    private bool isActive = false;
    private bool stopCoroutines = false;

    private void Start()
    {
        environment_UXUI_Intro.SetActive(true);
        environment_UXUI_Outro.SetActive(false);
        bullet.SetActive(false);

        introCameras.SetActive(true);
        outroCameras.SetActive(false);

        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Sniper);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isActive)
        {
            SkipSequence();
        }
    }

    public IEnumerator CameraSequence()
    {
        isActive = true;
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.IntroThreeDArt);
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToPart1());
    }

    private IEnumerator TransitionToPart1()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(cam1);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        cam1.transform.position = cam1Start.position;
        cam1.transform.DOMove(cam1End.position, delayPart1).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart1);
        StartCoroutine(TransitionToPart2());
    }

    private IEnumerator TransitionToPart2()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(cam2);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        cam2.transform.position = cam2Start.position;
        cam2.transform.DOMove(cam2End.position, delayPart2).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart2);
        StartCoroutine(TransitionToPart3());
    }

    private IEnumerator TransitionToPart3()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(cam3);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        cam3.transform.SetPositionAndRotation(cam3Start.position, cam3Start.rotation);

        yield return new WaitForSeconds(delayBeforeShot);
        bullet.SetActive(true);
        bullet.transform.position = bulletStartPOS.transform.position;
        bullet.transform.rotation = bulletStartPOS.transform.rotation;
        bullet.transform.DOMove(bulletEndPOS.position, delayPart3 - delayBeforeShot).SetEase(Ease.InOutSine);
        bullet.transform.DORotate(bulletEndPOS.rotation.eulerAngles, delayPart3 - delayBeforeShot).SetEase(Ease.InOutSine);

        cam3.transform.DORotate(cam3End.rotation.eulerAngles, delayPart3 - delayBeforeShot).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart3 - delayBeforeShot);
        StartCoroutine(TransitionToPart4());
    }

    private IEnumerator TransitionToPart4()
    {
        if (stopCoroutines) yield break;

        ActivateCamera(cam4);
        loadingOverlayHandler.FillLoadingAmount(.25f);
        cam4.transform.position = cam4Start.position;
        cam4.transform.DOMove(cam4End.position, delayPart4).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart4);
        SequenceComplete();
    }

    private void ActivateCamera(CinemachineVirtualCamera camera)
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;

        camera.enabled = true;
    }

    private void SkipSequence()
    {
        isActive= false;
        stopCoroutines = true;
        StopAllCoroutines();
        DOTween.KillAll();
        SequenceComplete();
    }

    private void SequenceComplete()
    {
        loadingOverlayHandler.FillLoadingAmount(1f);
        environment_UXUI_Intro.SetActive(false);
        environment_UXUI_Outro.SetActive(true);
        introCameras.SetActive(false);
        outroCameras.SetActive(true);

        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;

        OnIntroSequenceCompleted?.Invoke();
    }

}
