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

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += HandleModeSelected;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= HandleModeSelected;
    }

    private void Start()
    {
        ReturnToModeSelect();
    }

    private void HandleModeSelected(bool obj)
    {
        if (obj)
        {
            ExitModeSelectToGaming();
        }
        else
        {
            ExitModeSelectToCorperate();
        }
    }

    public void ReturnToModeSelect()
    {
        SetCameraPriority(modeSelectCamera, 10);
        SetCameraPriority(gamingCamera, 0);
        SetCameraPriority(corperateCamera, 0);
    }

    public void ExitModeSelectToGaming()
    {
        SetCameraPriority(modeSelectCamera, 0);
        SetCameraPriority(gamingCamera, 10);
        SetCameraPriority(corperateCamera, 0);

        // Reset position of gaming camera to its start point
        MoveGamingCamera(gamingCamStart, gamingCamEnd);
    }

    public void ExitModeSelectToCorperate()
    {
        SetCameraPriority(modeSelectCamera, 0);
        SetCameraPriority(gamingCamera, 0);
        SetCameraPriority(corperateCamera, 10);

        // Reset position of gaming camera to its start point
        MoveGamingCamera(gamingCamStart, gamingCamEnd);
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
