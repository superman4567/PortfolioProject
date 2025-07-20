using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UXUIProjectShowCaser : MonoBehaviour
{
    [SerializeField] private UXUIProjectSO[] projectConfigs;
    [SerializeField] private Image bannerImage;

    // Fast lookup table
    private Dictionary<EnumUXUIProjects, Sprite> _bannerLookup;

    private void Awake()
    {
        // Build a lookup from enum → sprite
        _bannerLookup = projectConfigs
            .Where(cfg => cfg != null)
            .ToDictionary(cfg => cfg.projectType, cfg => cfg.projectBanner);
    }

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
        if (_bannerLookup.TryGetValue(project, out var sprite))
        {
            bannerImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"[ProjectBannerController] No banner found for {project}");
        }
    }
}
