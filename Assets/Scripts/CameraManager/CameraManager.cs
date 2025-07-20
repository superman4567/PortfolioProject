using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

public class CameraManager : MonoBehaviour
{
    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera modeSelectCamera;
    [SerializeField] private CinemachineVirtualCamera gamingCamera;
    [SerializeField] private CinemachineVirtualCamera corperateCamera;

    [Header("Camera Transforms")]
    [SerializeField] private Transform gamingCamStart;
    [SerializeField] private Transform gamingCamEnd;

    [Header("Transition")]
    [SerializeField] private float transitionDuration = 1f;

    private Transform activeCamTransform => gamingCamera.transform;

    private const float priorityDelay = 0.25f;

    private void OnEnable()
    {
        ReturnButton.OnReturnToModeSelect += ReturnToModeSelect;
        ModeSelectController.OnModeSelected += HandleModeSelected;
    }

    private void OnDisable()
    {
        ReturnButton.OnReturnToModeSelect -= ReturnToModeSelect;
        ModeSelectController.OnModeSelected -= HandleModeSelected;
    }

    private void Start()
    {
        ReturnToModeSelect();
    }

    private void HandleModeSelected(bool isCorperate)
    {
        if (!isCorperate)
            ExitModeSelectToGaming();

        else if (isCorperate)
            ExitModeSelectToCorperate();
    }

    public void ReturnToModeSelect()
    {
        // delay the actual priority changes
        DOVirtual.DelayedCall(priorityDelay, () => ApplyPriorities(10, 0, 0));
    }

    public void ExitModeSelectToGaming()
    {
        // delay the priority swap
        DOVirtual.DelayedCall(priorityDelay, () => ApplyPriorities(0, 10, 0));

        // then do your camera move immediately
        MoveGamingCamera(gamingCamStart, gamingCamEnd);
    }

    public void ExitModeSelectToCorperate()
    {
        DOVirtual.DelayedCall(priorityDelay, () => ApplyPriorities(0, 0, 10));
        MoveGamingCamera(gamingCamStart, gamingCamEnd);
    }

    private void ApplyPriorities(int modeSelectPrio, int gamingPrio, int corperatePrio)
    {
        SetCameraPriority(modeSelectCamera, modeSelectPrio);
        SetCameraPriority(gamingCamera, gamingPrio);
        SetCameraPriority(corperateCamera, corperatePrio);
    }

    private void MoveGamingCamera(Transform from, Transform to)
    {
        Transform cam = activeCamTransform;
        cam.SetPositionAndRotation(from.position, from.rotation);

        cam.DOMove(to.position, transitionDuration).SetEase(Ease.InOutSine);
        cam.DORotateQuaternion(to.rotation, transitionDuration).SetEase(Ease.InOutSine);
    }

    private void SetCameraPriority(CinemachineVirtualCamera cam, int priority)
    {
        if (cam != null)
            cam.Priority = priority;
    }
}
