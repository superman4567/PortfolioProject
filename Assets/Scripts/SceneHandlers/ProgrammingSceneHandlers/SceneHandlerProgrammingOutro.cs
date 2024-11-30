using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SceneHandlerProgrammingOutro : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent();
    public event OnSequenceCompletedEvent OnOutroSequenceCompleted;

    public delegate void OnHideUIEvent();
    public event OnHideUIEvent OnHideUI;

    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private AccessoiryShower accessoiryShower;
    [SerializeField] private TimeScaleController timeScaleController;

    [Space]

    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;
    [SerializeField] private CinemachineVirtualCamera cam3;
    [SerializeField] private CinemachineVirtualCamera cam4;

    [Space]

    public Transform cam1Start;
    public Transform cam1End;
    [Space]

    public Transform cam3Start;
    public Transform cam3End;
    [Space]

    public Transform cam4Start;
    public Transform cam4End;

    [Space]

    public GameObject bullet;
    public Transform bulletStartPOS;
    public Transform bulletEndPOS;
    public float shootDelay = 1f;

    [Space]

    [SerializeField] private GameObject environment_Programming_Intro;
    [SerializeField] private GameObject environment_Programming_Outro;

    [Space]

    [SerializeField] private GameObject introCameras;
    [SerializeField] private GameObject outroCameras;

    [Space]

    [SerializeField] private float delayPart1 = 1f;
    [SerializeField] private float delayPart2 = 1f;
    [SerializeField] private float delayPart3 = 1f;
    [SerializeField] private float delayPart4 = 1f;

    [Space]

    [SerializeField] private Animator animator;

    private void Start()
    {
        bullet.SetActive(false);
    }

    public IEnumerator CameraSequence()
    {
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Sniper);
        animator.SetTrigger("GlassdoorAnimation");
        OnHideUI?.Invoke();
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToPart1());
    }

    private IEnumerator TransitionToPart1()
    {
        ActivateCamera(cam1);
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.OutroProgramming);
        cam1.transform.position = cam1Start.position;
        cam1.transform.DOMove(cam1End.position, delayPart1).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart1);
        StartCoroutine(TransitionToPart2());
    }

    private IEnumerator TransitionToPart2()
    {
        ActivateCamera(cam2);
        var transposer = cam2.GetCinemachineComponent<CinemachineFramingTransposer>();
        DOTween.To(() => 1.2f, value => transposer.m_CameraDistance = value, 3f, delayPart2).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart2);
        StartCoroutine(TransitionToPart3());
    }

    private IEnumerator TransitionToPart3()
    {
        ActivateCamera(cam3);
        cam3.transform.position = cam3Start.position;
        cam3.transform.rotation = cam3Start.rotation;
        cam3.transform.DOMove(cam3End.position, delayPart3).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart3);
        StartCoroutine(TransitionToPart4());
    }

    private IEnumerator TransitionToPart4()
    {
        ActivateCamera(cam4);
        
        cam4.transform.position = cam4Start.position;
        cam4.transform.DOMove(cam4End.position, delayPart4).SetEase(Ease.InOutSine);

        var transposer = cam4.GetCinemachineComponent<CinemachineFramingTransposer>();
        DOTween.To(() => 2f, value => transposer.m_CameraDistance = value, 0.2f, (delayPart4 + 0.3f)).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(shootDelay);
        bullet.SetActive(true);
        bullet.transform.position = bulletStartPOS.position;
        bullet.transform.DOMove(bulletEndPOS.position, (delayPart4 - shootDelay)).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(delayPart4 - shootDelay);
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

    private void SequenceComplete()
    {
        environment_Programming_Intro.SetActive(false);
        environment_Programming_Outro.SetActive(true);

        introCameras.SetActive(false);
        outroCameras.SetActive(true);

        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;

        OnOutroSequenceCompleted?.Invoke();
    }


}
