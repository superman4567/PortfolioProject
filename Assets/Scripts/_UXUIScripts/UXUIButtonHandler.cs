using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXUIButtonHandler : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPanelPair
    {
        public Button button; // Change Transform to Button for easier event handling
        public GameObject panel;
    }

    public List<ButtonPanelPair> buttonPanelPairs;
    public UXUITitleScrollHandler titleScrollHandler; // Reference to the scroll handler script

    private void Start()
    {
        foreach (var pair in buttonPanelPairs)
        {
            if (pair.button != null)
            {
                // Add an onClick listener for each button
                pair.button.onClick.AddListener(() => OnButtonClicked(pair));
            }
        }
    }

    private void OnButtonClicked(ButtonPanelPair clickedPair)
    {
        // Find the index of the clicked button
        int clickedIndex = buttonPanelPairs.IndexOf(clickedPair);
        if (clickedIndex != -1)
        {
            // Set the clicked button as the active button in the scroll handler
            titleScrollHandler.SetActiveIndex(clickedIndex);
        }
    }

    public void ActivatePanel(GameObject panelToActivate)
    {
        // Deactivate all panels
        foreach (var pair in buttonPanelPairs)
        {
            if (pair.panel != null)
                pair.panel.SetActive(false);
        }

        // Activate the selected panel
        if (panelToActivate != null)
            panelToActivate.SetActive(true);
    }
}
