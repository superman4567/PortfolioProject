using DG.Tweening;
using System;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [Header("Material Settings")]
    public Material dissolveMaterial;

    [Header("Dissolve Settings")]
    public float dissolveDuration = 1f;

    [Header("Timing")]
    public float delayBeforeDissolveIn = 0.5f; // seconds

    private Color whiteTint = Color.white;
    private Color blackTint = Color.black;

    private float visibleLevel = 0f; // Fully visible
    private float hiddenLevel = 1f;  // Fully dissolved

    private void OnEnable()
    {
        ModeSelectController.OnModeSelected += HandleModeChange;
    }

    private void OnDisable()
    {
        ModeSelectController.OnModeSelected -= HandleModeChange;
    }

    private void Start()
    {
        DissolveOut();
    }

    private void HandleModeChange(bool isDarkMode)
    {
        // First, change tint color immediately
        Color targetColor = isDarkMode ? whiteTint : blackTint;
        dissolveMaterial.DOColor(targetColor, "_Color", dissolveDuration);

        // Dissolve in immediately (show)
        DissolveIn();

        // Schedule dissolve out after a delay
        DOVirtual.DelayedCall(1f, () =>
        {
            DissolveOut();
        });
    }


    public void SetToGamingMode() // Dark mode
    {
        if (!dissolveMaterial) return;

        dissolveMaterial.DOColor(whiteTint, "_Color", dissolveDuration);
        dissolveMaterial.DOFloat(visibleLevel, "_Level", dissolveDuration);
    }

    public void SetToCorporateMode()
    {
        if (!dissolveMaterial) return;

        dissolveMaterial.DOColor(blackTint, "_Color", dissolveDuration);
        dissolveMaterial.DOFloat(visibleLevel, "_Level", dissolveDuration);
    }

    public void DissolveOut()
    {
        if (!dissolveMaterial) return;

        dissolveMaterial.DOFloat(hiddenLevel, "_Level", dissolveDuration);
    }

    public void DissolveIn()
    {
        if (!dissolveMaterial) return;

        dissolveMaterial.DOFloat(visibleLevel, "_Level", dissolveDuration);
    }
}
