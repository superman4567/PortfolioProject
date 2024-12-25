using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class ThreeDButtonHandler : MonoBehaviour
{
    [System.Serializable]
    private class ButtonGameObjectPair
    {
        [SerializeField] private Button button;
        public GameObject associatedObject;
        public GameObject model;

        public Button Button => button;
        public GameObject AssociatedObject => associatedObject;
    }

    [SerializeField] private ButtonGameObjectPair[] buttonGameObjectPairs;

    [Space]

    [SerializeField] private RectTransform projectSelectContainer;
    private RectTransform threeDModelGallery;
    private RectTransform threeDModelSpecs;

    [Space]

    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject tools;

   [SerializeField] private GameObject rawImage; 

    [Space]

    [SerializeField] private Color inactiveButtonColor = new Color(0.7f, 0.7f, 0.7f);

    private const float modelGalleryDefaultPosY = 0f;
    private const float modelGalleryHiddenXPosY = -1100f;
    private const float projectSelectDefaultPosX = 32f;
    private const float projectSelectHiddenPosX = -400f;
    private const float threeDModelSpecsDefaultPosX = 0f;
    private const float threeDModelSpecsHiddenPosX = 400f;

    private LocalizedString imageState;
    private LocalizedString modelState;

    private ButtonGameObjectPair currentActivePair;
    private bool isGalleryVisible = true;

    private void Start()
    {
        if (buttonGameObjectPairs.Length > 0)
        {
            currentActivePair = buttonGameObjectPairs[0];
            SetActivePair(currentActivePair);
        }

        foreach (var pair in buttonGameObjectPairs)
        {
            var currentPair = pair;
            currentPair.Button.onClick.AddListener(() => OnButtonClicked(currentPair));
        }

        toggleButton.onClick.AddListener(ToggleGallery);

        GetCorrectChilds();

        Show3DModelUI();
        buttonText.text = modelState.GetLocalizedString();
    }

    private void OnButtonClicked(ButtonGameObjectPair clickedPair)
    {
        SetActivePair(clickedPair);
    }

    private void SetActivePair(ButtonGameObjectPair pairToActivate)
    {
        foreach (var pair in buttonGameObjectPairs)
        {
            var colors = pair.Button.colors;
            colors.normalColor = pair == pairToActivate ? Color.white : inactiveButtonColor;
            pair.Button.colors = colors;

            pair.AssociatedObject.SetActive(pair == pairToActivate);
        }

        SetCorrect3DModel(pairToActivate);

        currentActivePair = pairToActivate;

        GetCorrectChilds();

        Show3DModelUI();
    }

    private void ToggleGallery()
    {
        if (isGalleryVisible)
        {
            ShowGallery();
            Toggle3DModel(false);
        }
        else
        {
            Show3DModelUI();
            Toggle3DModel(true);
        }

        isGalleryVisible = !isGalleryVisible;
    }

    private void GetCorrectChilds()
    {
        RectTransform parentGallery = currentActivePair.AssociatedObject.transform.GetChild(0) as RectTransform;
        threeDModelGallery = parentGallery.transform.GetChild(2) as RectTransform;


        RectTransform parentSpecs = currentActivePair.AssociatedObject.transform.GetChild(0) as RectTransform;
        threeDModelSpecs = parentSpecs.transform.GetChild(1) as RectTransform;
    }

    private void Show3DModelUI()
    {
        ToggleRawImage(true);
        MoveGallery(modelGalleryHiddenXPosY);
        MoveProjectSelect(projectSelectDefaultPosX);
        MoveModelSpecs(threeDModelSpecsDefaultPosX);
        buttonText.text = modelState.GetLocalizedString();

        ShowToolIcons(true);
    }

    private void ShowGallery()
    {
        ToggleRawImage(false);
        MoveGallery(modelGalleryDefaultPosY);
        MoveProjectSelect(projectSelectHiddenPosX);
        MoveModelSpecs(threeDModelSpecsHiddenPosX);
        buttonText.text = imageState.GetLocalizedString();

        ShowToolIcons(false);
    }

    private void ShowToolIcons(bool value)
    {
        tools.SetActive(value);
    }

    private void MoveGallery(float targetYPos)
    {
       
        threeDModelGallery.DOAnchorPosY(targetYPos, 0.2f);
    }

    private void MoveProjectSelect(float targetXPos)
    {
        projectSelectContainer.DOAnchorPosX(targetXPos, 0.2f);
    }

    private void MoveModelSpecs(float targetXPos)
    {
        
        threeDModelSpecs.DOAnchorPosX(targetXPos, 0.2f);
    }

    private void ToggleRawImage(bool value)
    {
        rawImage.SetActive(value);
    }

    private void Toggle3DModel(bool value)
    {
        if (value)
        {
            currentActivePair.model.SetActive(true);
        }
        else
        {
            currentActivePair.model.SetActive(false);
        }
    }

    private void SetCorrect3DModel(ButtonGameObjectPair clickedPair)
    {
        currentActivePair.model.SetActive(false);
        clickedPair.model.SetActive(true);
    }
}
