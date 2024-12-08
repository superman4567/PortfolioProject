using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDButtonHandler : MonoBehaviour
{
    [System.Serializable]
    private class ButtonPanelPair
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject modelPrefab;

        public Button Button => button;
        public GameObject ModelPrefab => modelPrefab;
    }

    [SerializeField] private List<ButtonPanelPair> buttonPanelPairs;

    [Space]

    [SerializeField] private RectTransform threeDModelGallery;
    [SerializeField] private RectTransform characterSelectContainer;
    [SerializeField] private RectTransform threeDModelSpecs;

    [Space]

    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Space]

    [SerializeField] private Transform activeProjectHolder;
    [SerializeField] private Color inactiveButtonColor = new Color(0.7f, 0.7f, 0.7f);

    private const float modelGalleryDefaultPosY = 0f;
    private const float modelGalleryHiddenXPosY = -1100f;
    private const float projectSelectDefaultPosX = 32f;
    private const float projectSelectHiddenPosX = -400f;
    private const float threeDModelSpecsDefaultPosX = 0f;
    private const float threeDModelSpecsHiddenPosX = 400f;

    private const string imageState = "Preview Model Data";
    private const string modelState = "Preview Project Gallery";

    private ButtonPanelPair currentActivePair;
    private bool isGalleryVisible = true;

    private void Start()
    {
        if (buttonPanelPairs.Count > 0)
        {
            currentActivePair = buttonPanelPairs[0];
            UpdateUI(currentActivePair);
        }

        foreach (var pair in buttonPanelPairs)
        {
            var currentPair = pair;
            currentPair.Button.onClick.AddListener(() => UpdateUI(currentPair));
        }

        toggleButton.onClick.AddListener(ToggleGallery);
        buttonText.text = modelState;
    }

    private void UpdateUI(ButtonPanelPair selectedPair)
    {
        currentActivePair = selectedPair;

        foreach (var pair in buttonPanelPairs)
        {
            if (pair.Button == currentActivePair.Button)
            {
                var colors = pair.Button.colors;
                colors.normalColor = Color.white; 
                pair.Button.colors = colors;
            }
            else
            {
                var colors = pair.Button.colors;
                colors.normalColor = inactiveButtonColor; 
                pair.Button.colors = colors;
            }
        }
    }

    private void ToggleGallery()
    {
        if (isGalleryVisible)
        {
            MoveGallery(modelGalleryDefaultPosY);
            MoveCharacterSelect(projectSelectHiddenPosX);
            MoveModelSpecs(threeDModelSpecsHiddenPosX);
            buttonText.text = imageState;
        }
        else
        {
            MoveGallery(modelGalleryHiddenXPosY);
            MoveCharacterSelect(projectSelectDefaultPosX);
            MoveModelSpecs(threeDModelSpecsDefaultPosX);
            buttonText.text = modelState;
        }

        isGalleryVisible = !isGalleryVisible;
    }

    private void MoveGallery(float targetYPos)
    {
        threeDModelGallery.DOAnchorPosY(targetYPos, 0.5f);
    }

    private void MoveCharacterSelect(float targetXPos)
    {
        characterSelectContainer.DOAnchorPosX(targetXPos, 0.5f);
    }

    private void MoveModelSpecs(float targetXPos)
    {
        threeDModelSpecs.DOAnchorPosX(targetXPos, 0.5f);
    }
}
