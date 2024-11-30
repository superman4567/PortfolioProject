using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SceneHandlerProgrammingIntro : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent();
    public event OnSequenceCompletedEvent OnIntroSequenceCompleted;

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
    
    public Transform cam2Start;
    public Transform cam2End;
    [Space]
    
    public Transform cam3Start;
    public Transform cam3End;
    [Space]
    
    public Transform cam4Start;
    public Transform cam4End;

    [Space]

    [SerializeField] private GameObject environment_Programming_Intro;
    [SerializeField] private GameObject environment_Programming_Outro;

    [Space]

    [SerializeField] private GameObject introCameras;
    [SerializeField] private GameObject outroCameras;

    [Space]

    [SerializeField] private float delayPart1;
    [SerializeField] private float delayPart2;
    [SerializeField] private float delayPart3;
    [SerializeField] private float delayPart4;

    private void Start()
    {
        environment_Programming_Intro.SetActive(true);
        environment_Programming_Outro.SetActive(false);

        introCameras.SetActive(true);
        outroCameras.SetActive(false);
    }

    public IEnumerator CameraSequence()
    {
        accessoiryShower.SetActiveWeapon(AccessoiryShower.WeaponType.Laptop);
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        yield return StartCoroutine(TransitionToPart1());
    }

    private IEnumerator TransitionToPart1()
    {
        ActivateCamera(cam1);
        timeScaleController.PlayTimeCurve(TimeScaleController.EnumCurveChoices.IntroProgramming);
        cam1.transform.position = cam1Start.position;
        cam1.transform.DOMove(cam1End.position, delayPart1).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(delayPart1);

        StartCoroutine(TransitionToPart2());
    }

    private IEnumerator TransitionToPart2()
    {
        ActivateCamera(cam2);

        cam2.transform.position = cam2Start.position;
        cam2.transform.DOMove(cam2End.position, delayPart2).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(delayPart2);

        StartCoroutine(TransitionToPart3());
    }

    private IEnumerator TransitionToPart3()
    {
        ActivateCamera(cam3);

        cam3.transform.position = cam3Start.position;
        cam3.transform.DOMove(cam3End.position, delayPart3).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(delayPart3);

        StartCoroutine(TransitionToPart4());
    }

    private IEnumerator TransitionToPart4()
    {
        ActivateCamera(cam4);

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

        OnIntroSequenceCompleted?.Invoke();
    }

}
