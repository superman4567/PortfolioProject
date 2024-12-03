using System.Collections.Generic;
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
                selectedPair.panelWithImages.SetActive(true);
        }
        else
        {
            if (selectedPair.panelWithSpecs != null)
                selectedPair.panelWithSpecs.SetActive(true);
        }

        currentActivePair = selectedPair;
    }

    public void SwapPanels()
    {
        if (currentActivePair == null) return;

        bool isImagesActive = currentActivePair.panelWithImages != null && currentActivePair.panelWithImages.activeSelf;
        bool isSpecsActive = currentActivePair.panelWithSpecs != null && currentActivePair.panelWithSpecs.activeSelf;

        if (currentActivePair.panelWithImages != null)
            currentActivePair.panelWithImages.SetActive(!isImagesActive);

        if (currentActivePair.panelWithSpecs != null)
            currentActivePair.panelWithSpecs.SetActive(!isSpecsActive);
    }
}
