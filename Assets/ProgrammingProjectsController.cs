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
        foreach (var pair in buttonPanelPairs)
        {
            // Cache the reference for the panelWithImages to avoid using closures
            var panelToActivate = pair.panel;

            // Assign onClick listener for each button
            pair.button.onClick.AddListener(() => ActivatePanel(panelToActivate));
        }
    }

    private void ActivatePanel(GameObject panelToActivate)
    {
        // Loop through all panels and deactivate them
        foreach (var pair in buttonPanelPairs)
        {
            if (pair.panel != null)
                pair.panel.SetActive(false);
        }

        // Activate the selected panelWithImages
        if (panelToActivate != null)
            panelToActivate.SetActive(true);
    }
}
