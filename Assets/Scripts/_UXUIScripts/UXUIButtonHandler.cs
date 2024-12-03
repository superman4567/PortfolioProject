using System.Collections.Generic;
using UnityEngine;

public class UXUIButtonHandler : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPanelPair
    {
        public Transform button;
        public GameObject panel;
    }
    public List<ButtonPanelPair> buttonPanelPairs;

    public void ActivatePanel(GameObject panelToActivate)
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
