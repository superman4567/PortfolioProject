using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;
using System;

public class ModeSelectController : MonoBehaviour
{
    [Header("References")]
    public GameObject plateau;
    [Space]
    public Button confirmButon;
    public Button rotateButton;
    public Image rotateButtonImage;
    public TextMeshProUGUI rotateButtonText;
    [Space]
    public Volume volume;
    public Material targetMaterial;
    public Material floorMaterial;
   
    [Header("Characters")]
    public Renderer whiteCharacter;
    public Renderer blackCharacter;

    [Header("Dissolve Settings")]
    public float cutoffStart = -1f;
    public float cutoffEnd = 1f;
    public float dissolveDuration = 1f;

    [Header("Transition Settings")]
    public float rotationDuration = 1f;
    public float colorTransitionDuration = 1f;

    [Header("Floor Material Settings")]
    public Color floorLightColor = new Color(0.85f, 0.85f, 0.85f);
    public Color floorDarkColor = new Color(0.15f, 0.15f, 0.15f);

    private WorldSpaceTextChanger worldSpaceTextChanger;
    private PlateuRotator plateuRotator;
    private bool isDarkMode = false;

    public static event Action<bool> OnModeSelected;

    private void Awake()
    {
        worldSpaceTextChanger = GetComponent<WorldSpaceTextChanger>();
        plateuRotator = GetComponent<PlateuRotator>();
    }

    void Start()
    {
        if (volume) volume.enabled = false;

        if (targetMaterial) targetMaterial.color = Color.white;
        if (floorMaterial) floorMaterial.color = floorLightColor;

        confirmButon.onClick.AddListener(Button_OnModeSelected);
        rotateButton.onClick.AddListener(OnRotateButtonPressed);

        SetDissolve(whiteCharacter, cutoffStart);
        SetDissolve(blackCharacter, cutoffEnd);
    }

    private void OnRotateButtonPressed()
    {
        RotatePlateau();
        ToggleMaterialColor();
        ToggleCharacterDissolve();
        TextChange();
        ButtonColorChange();
        SpeedUpPlateau();
    }

    public void Button_OnModeSelected()
    {
        OnModeSelected?.Invoke(isDarkMode);
    }

    private void SpeedUpPlateau()
    {
        plateuRotator.BoostRotationSpeed();
    }

    private void ButtonColorChange()
    {
        if (isDarkMode)
        {
            rotateButtonImage.color = Color.white;
            rotateButtonText.color = Color.black;
        }
        else
        {
            rotateButtonImage.color = Color.black;
            rotateButtonText.color = Color.white;
        }
    }

    private void RotatePlateau()
    {
        if (!plateau) return;

        float targetY = plateau.transform.eulerAngles.y + 180f;
        plateau.transform
            .DORotate(new Vector3(0f, targetY, 0f), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine);
    }

    private void ToggleMaterialColor()
    {
        if (targetMaterial)
        {
            Color target = isDarkMode ? Color.white : Color.black;
            targetMaterial.DOColor(target, colorTransitionDuration);
        }

        if (floorMaterial)
        {
            Color floorTarget = isDarkMode ? floorLightColor : floorDarkColor;
            floorMaterial.DOColor(floorTarget, colorTransitionDuration);
        }

        isDarkMode = !isDarkMode;
    }

    private void ToggleCharacterDissolve()
    {
        SetDissolve(whiteCharacter, isDarkMode ? cutoffEnd : cutoffStart);
        SetDissolve(blackCharacter, isDarkMode ? cutoffStart : cutoffEnd);
    }

    private void SetDissolve(Renderer rend, float targetCutoff)
    {
        if (!rend) return;

        foreach (var mat in rend.materials)
        {
            if (mat.HasProperty("_CutOffHeight"))
            {
                mat.DOFloat(targetCutoff, "_CutOffHeight", dissolveDuration)
                   .SetEase(Ease.InOutSine);
            }
        }
    }

    private void TextChange()
    {
        if (isDarkMode)
        {
            worldSpaceTextChanger.SetToGaming();
        }
        else
        {
            worldSpaceTextChanger.SetToCorperate();
        }
    }
}
