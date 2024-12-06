using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDButtonHandler : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPanelPair
    {
        public Button button;
        public GameObject modelPrefab;
        public GameObject panelWithImages;
        public GameObject panelWithSpecs;
    }

    public List<ButtonPanelPair> buttonPanelPairs;
    public Button swapButton;
    public RectTransform swapButtonRect;
    public TextMeshProUGUI buttonText;

    private const string imageState = "Preview Model Data";
    private const string modelState = "Preview Project Gallery";
    private ButtonPanelPair currentActivePair;

    private void Start()
    {
        if (buttonPanelPairs.Count > 0)
        {
            currentActivePair = buttonPanelPairs[0];
            ActivatePanel(currentActivePair);
        }

        foreach (var pair in buttonPanelPairs)
        {
            var currentPair = pair;
            pair.button.onClick.AddListener(() => ActivatePanel(currentPair));
        }

        swapButton.onClick.AddListener(SwapPanels);
        swapButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ActivatePanel(ButtonPanelPair selectedPair)
    {
        foreach (var pair in buttonPanelPairs)
        {
            if (pair.panelWithImages != null)
                pair.panelWithImages.SetActive(false);

            if (pair.panelWithSpecs != null)
                pair.panelWithSpecs.SetActive(false);
        }

        if (selectedPair.modelPrefab == null)
        {
            if (selectedPair.panelWithImages != null)
            {
                selectedPair.panelWithImages.SetActive(true);
                buttonText.text = imageState;

                Vector2 newPosition = swapButtonRect.anchoredPosition;
                newPosition.x = -220f; 
                swapButtonRect.anchoredPosition = newPosition;
            }
        }
        else
        {
            if (selectedPair.panelWithSpecs != null)
            {
                selectedPair.panelWithSpecs.SetActive(true);
                buttonText.text = modelState;

                Vector2 newPosition = swapButtonRect.anchoredPosition;
                newPosition.x = 0f;
                swapButtonRect.anchoredPosition = newPosition;
            }
        }

        currentActivePair = selectedPair;
    }

    public void SwapPanels()
    {
        if (currentActivePair == null) return;

        bool isImagesActive = currentActivePair.panelWithImages != null && currentActivePair.panelWithImages.activeSelf;
        bool isSpecsActive = currentActivePair.panelWithSpecs != null && currentActivePair.panelWithSpecs.activeSelf;

        // Toggle panels
        if (currentActivePair.panelWithImages != null)
            currentActivePair.panelWithImages.SetActive(!isImagesActive);

        if (currentActivePair.panelWithSpecs != null)
            currentActivePair.panelWithSpecs.SetActive(!isSpecsActive);

        float targetX = isImagesActive ? 0f : -220f; // Adjust based on active state
        swapButtonRect.DOAnchorPosX(targetX, 0.1f);

        buttonText.text = isImagesActive ? modelState : imageState;
    }
}
