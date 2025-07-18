using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
using System;

public class ModeSelectController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject plateau;

    [Header("Confirm Button")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Image confirmButtonBackground;
    [SerializeField] private TextMeshProUGUI confirmText;

    [Header("Rotate Button")]
    [SerializeField] private Button rotateButton;
    [SerializeField] private Image rotateButtonBackground;
    [SerializeField] private TextMeshProUGUI rotateText;

    [Header("Materials")]
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Material floorMaterial;

    [Header("Confirm Button")]
    [SerializeField] private CanvasGroup buttonsCanvasGroup;

    [Header("Characters")]
    [SerializeField] private Renderer whiteCharacter;
    [SerializeField] private Renderer blackCharacter;

    [Header("Settings")]
    [SerializeField] private float dissolveDuration = 1f;
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private float colorTransitionDuration = 1f;
    [SerializeField] private float cutoffVisible = 1f;
    [SerializeField] private float cutoffHidden = -1f;
    [SerializeField] private Color floorLight = new Color(0.85f, 0.85f, 0.85f);
    [SerializeField] private Color floorDark = new Color(0.15f, 0.15f, 0.15f);

    private bool isCorporate = true;
    private WorldSpaceTextChanger textChanger;
    private PlateuRotator plateuRotator;

    public static event Action<bool> OnModeSelected;

    void Awake()
    {
        textChanger = GetComponent<WorldSpaceTextChanger>();
        plateuRotator = GetComponent<PlateuRotator>();
        confirmButton.onClick.AddListener(OnConfirmButton_CallBack);
        rotateButton.onClick.AddListener(ToggleMode);
    }

    void Start()
    {
        postProcessVolume.enabled = false;
        ApplyMaterials();
        ApplyCharacterDissolve();
        UpdateTexts();
        UpdateButtonVisuals();
    }

    private void OnConfirmButton_CallBack()
    {
        OnModeSelected?.Invoke(isCorporate);
        buttonsCanvasGroup.DOFade(0f, 0.25f);
    }

    private void ToggleMode()
    {
        // **flip first** so all the methods see the new state
        isCorporate = !isCorporate;

        RotatePlateau();
        ApplyMaterials();
        ApplyCharacterDissolve();
        UpdateTexts();
        UpdateButtonVisuals();
        plateuRotator.BoostRotationSpeed();
    }

    private void RotatePlateau()
    {
        plateau.transform
            .DORotate(
                new Vector3(0, plateau.transform.eulerAngles.y + 180f, 0),
                rotationDuration,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.InOutSine);
    }

    private void ApplyMaterials()
    {
        Color bodyColor = isCorporate ? Color.white : Color.black;
        Color floorColor = isCorporate ? floorLight : floorDark;

        if (targetMaterial)
            targetMaterial.DOColor(bodyColor, colorTransitionDuration);

        if (floorMaterial)
            floorMaterial.DOColor(floorColor, colorTransitionDuration);
    }

    private void ApplyCharacterDissolve()
    {
        float whiteCutoff = isCorporate ? cutoffVisible : cutoffHidden;
        float blackCutoff = isCorporate ? cutoffHidden : cutoffVisible;

        SetDissolve(whiteCharacter, whiteCutoff);
        SetDissolve(blackCharacter, blackCutoff);
    }

    private void SetDissolve(Renderer rend, float cutoff)
    {
        foreach (var mat in rend.materials)
            if (mat.HasProperty("_CutOffHeight"))
                mat.DOFloat(cutoff, "_CutOffHeight", dissolveDuration)
                   .SetEase(Ease.InOutSine);
    }

    private void UpdateTexts()
    {
        textChanger.SetTo(isCorporate
            ? WorldSpaceTextChanger.Mode.Corporate
            : WorldSpaceTextChanger.Mode.Gaming);
    }

    private void UpdateButtonVisuals()
    {
        Color textColor = isCorporate ? Color.white : Color.black;
        Color backgroundColor = textColor == Color.white ? Color.black : Color.white;

        rotateText.color = textColor;
        confirmText.color = textColor;
        rotateButtonBackground.color = backgroundColor;
        confirmButtonBackground.color = backgroundColor;
    }
}
