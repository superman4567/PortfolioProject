using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UXUIProjectShowCaser : MonoBehaviour
{
    [SerializeField] private UXUIProjectSO[] projectConfigs;

    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private TextMeshProUGUI projectSubtext;
    [SerializeField] private Image bannerImage;

    private void OnEnable()
    {
        UXUIProjectButton.OnProjectButtonHovered += OnProjectHovered;
    }

    private void OnDisable()
    {
        UXUIProjectButton.OnProjectButtonHovered -= OnProjectHovered;
    }

    private void OnProjectHovered(EnumUXUIProjects project)
    {
        // Find the SO whose enum matches
        var config = projectConfigs
            .FirstOrDefault(cfg => cfg.projectType == project);

        if (config != null)
        {
            bannerImage.sprite = config.projectBanner;
            projectName.text = config.projectName;
            projectSubtext.text = config.projectSubtext;
        }
        else
        {
            Debug.LogWarning($"No UXUIProjectSO found for {project}");
        }
    }
}
