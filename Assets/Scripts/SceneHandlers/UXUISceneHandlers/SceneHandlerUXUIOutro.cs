using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SceneHandlerUXUIOutro : MonoBehaviour
{
    public delegate void OnSequenceCompletedEvent();
    public event OnSequenceCompletedEvent OnOutroSequenceCompleted;

    public delegate void OnHideUIEvent();
    public event OnHideUIEvent OnHideUI;

    [SerializeField] private CinemachineBrain cineBrain;
    [SerializeField] private CinemachineVirtualCamera cam1;
    public Transform cam1Start;
    public Transform cam1End;
    [Space]
    [SerializeField] private CinemachineVirtualCamera cam2;
    public Transform cam2Start;
    public Transform cam2End;
    [Space]
    [SerializeField] private CinemachineVirtualCamera cam3;
    public Transform cam3Start;
    public Transform cam3End;
    [Space]
    [SerializeField] private CinemachineVirtualCamera cam4;
    public Transform cam4Start;
    public Transform cam4End;
    [Space]

    [SerializeField] private GameObject environment_UXUI_Intro;
    [SerializeField] private GameObject environment_UXUI_Outro;

    [Space]

    [SerializeField] private float delayPart1 = 1f;
    [SerializeField] private float delayPart2 = 1f;
    [SerializeField] private float delayPart3 = 1f;

    [Space]

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerModel;

    public void CameraSequence()
    {
        cineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;

        OnHideUI?.Invoke();

        StartCoroutine(TransitionToPart1());
    }

    private IEnumerator TransitionToPart1()
    {
        ActivateCamera(cam1);

        cam1.transform.position = cam1Start.position;
        cam1.transform.DOMove(cam1End.position, delayPart1).SetEase(Ease.InOutSine);

        animator.SetTrigger("DriftAnimation");

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
        playerModel.SetActive(false);

        ActivateCamera(cam3);

        cam3.transform.position = cam3Start.position;
        cam3.transform.DOMove(cam3End.position, delayPart3).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(delayPart3);

        StartCoroutine(TransitionToPart4());
    }

    private IEnumerator TransitionToPart4()
    {
        ActivateCamera(cam4);

        float delay = 3f;

        cam4.transform.position = cam4Start.position;
        cam4.transform.DOMove(cam4End.position, delay).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(delay);

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
        OnOutroSequenceCompleted?.Invoke();

        environment_UXUI_Intro.SetActive(false);
        environment_UXUI_Outro.SetActive(false);

        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;
    }
}