using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingProjectsController : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPanelPair
    {
        public Button button;
        public GameObject panel;
    }

    public List<ButtonPanelPair> buttonPanelPairs;

    private void Start()
    {
        // Assign listeners to each button
        foreach (var pair in buttonPanelPairs)
        {
            var currentPair = pair;
            pair.button.onClick.AddListener(() => ActivatePanelAndChild(currentPair));
        }

        // Activate the project at index 0
        if (buttonPanelPairs.Count > 0)
        {
            ActivatePanelAndChild(buttonPanelPairs[0]);
        }
        else
        {
            Debug.LogWarning("No ButtonPanelPairs are assigned in the Inspector.");
        }
    }

    private void ActivatePanelAndChild(ButtonPanelPair selectedPair)
    {
        foreach (var pair in buttonPanelPairs)
        {
            // Deactivate all panels
            if (pair.panel != null)
                pair.panel.SetActive(false);

            // Disable the Image component on all buttons' children
            var childImage = pair.button.GetComponentInChildren<Image>();
            if (childImage != null)
                childImage.enabled= false;
        }

        // Activate the selected panel
        if (selectedPair.panel != null)
            selectedPair.panel.SetActive(true);

        // Enable the Image component on the selected button's child
        var selectedChildImage = selectedPair.button.GetComponentInChildren<Image>();
        if (selectedChildImage != null)
            selectedChildImage.enabled = true;
        else
        {
            Debug.Log("Help");
        }
    }
}
