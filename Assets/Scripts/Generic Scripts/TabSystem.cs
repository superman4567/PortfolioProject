using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro support
using System.Collections.Generic;

public class TabSystem : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button tabButton;       // The button for this tab
        public GameObject content;     // The content to show for this tab
        public string buttonText;      // The text to display on the button
    }

    public List<Tab> tabs;             // List of tabs
    public Color activeColor = Color.green;  // Color for active tab projectButtons
    public Color normalColor = Color.white;  // Color for inactive tab projectButtons
    public Color activeTextColor = Color.black; // Text color for the active tab
    public Color normalTextColor = Color.gray;  // Text color for inactive tabs

    private int activeTabIndex = 0; // Currently active tab index

    private void Start()
    {
        InitializeTabs();
    }

    private void InitializeTabs()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int index = i; // Capture index for the lambda function
            tabs[i].tabButton.onClick.AddListener(() => SwitchTab(index));

            // Set button text during initialization
            UpdateButtonText(tabs[i].tabButton, tabs[i].buttonText);
        }

        // Activate the default tab
        SwitchTab(activeTabIndex);
    }

    private void SwitchTab(int index)
    {
        activeTabIndex = index;

        for (int i = 0; i < tabs.Count; i++)
        {
            bool isActive = i == activeTabIndex;

            // Toggle content visibility
            tabs[i].content.SetActive(isActive);

            // Update button color and image color
            Color targetColor = isActive ? activeColor : normalColor;
            UpdateButtonColors(tabs[i].tabButton, targetColor);

            // Update text color
            UpdateButtonTextColor(tabs[i].tabButton, isActive ? activeTextColor : normalTextColor);
        }
    }

    private void UpdateButtonColors(Button button, Color color)
    {
        // Update Button color
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;

        // Update the Image component color
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }

    private void UpdateButtonText(Button button, string text)
    {
        TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = text;
        }
        else
        {
            Text uiText = button.GetComponentInChildren<Text>();
            if (uiText != null)
            {
                uiText.text = text;
            }
        }
    }

    private void UpdateButtonTextColor(Button button, Color color)
    {
        TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.color = color;
        }
        else
        {
            Text uiText = button.GetComponentInChildren<Text>();
            if (uiText != null)
            {
                uiText.color = color;
            }
        }
    }
}
