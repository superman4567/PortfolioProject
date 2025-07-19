using DG.Tweening;
using UnityEngine;

public class PlateuRotator : MonoBehaviour
{
    public Transform centerPoint;

    private Tween swingTween;
    private float swingRange = 20f;       // Total swing (15 left to 15 right)
    private float normalDuration = 7f;
    private float fastMultiplier = 7f;
    private float boostTime = 0.5f;

    void Start()
    {
        centerPoint.localRotation = Quaternion.Euler(0f, -swingRange / 2f, 0f); // Start at -15°
        StartSwingRotation();
    }

    private void StartSwingRotation()
    {
        swingTween = centerPoint
            .DOLocalRotate(new Vector3(0f, swingRange, 0f), normalDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }

    public void BoostRotationSpeed()
    {
        if (swingTween == null || !swingTween.IsActive()) return;

        swingTween.timeScale = fastMultiplier;

        DOVirtual.DelayedCall(boostTime, () =>
        {
            if (swingTween != null && swingTween.IsActive())
            {
                swingTween.timeScale = 1f;
            }
        });
    }
}
