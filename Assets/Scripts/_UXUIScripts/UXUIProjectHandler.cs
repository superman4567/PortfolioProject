using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UXUIProjectHandler : LocalizedMonoBehaviour
{
    [SerializeField] private UXUIProjectSO[] uXUIProjectSO;
    [SerializeField] private GameObject[] uXUIGalleries;

    [Space]

    [SerializeField] private Image projectImage;
    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private TextMeshProUGUI projectSubtext;
    [SerializeField] private TextMeshProUGUI projectRole;
    [SerializeField] private TextMeshProUGUI projectDate;
    [SerializeField] private TextMeshProUGUI projectTools;
    [SerializeField] private LocalizeStringEvent projectDescription;
    [SerializeField] private LocalizeStringEvent projectRetrospective;

    private void Awake()
    {
        SetProjectByEnum(EnumUXUIProjects.Braverz); // Default initialization
    }

    private void OnEnable()
    {
        UXUIProjectButton.OnProjectButtonClicked += SetProjectByEnum;
    }

    private void OnDisable()
    {
        UXUIProjectButton.OnProjectButtonClicked -= SetProjectByEnum;
    }

    public void SetProjectByEnum(EnumUXUIProjects projectEnum)
    {
        int index = (int)projectEnum;

        Debug.Log($"Setting project by enum: {projectEnum} (Index: {index})", this);

        if (index < 0 || index >= uXUIProjectSO.Length || index >= uXUIGalleries.Length)
        {
            Debug.LogWarning($"Invalid project enum index: {index}", this);
            return;
        }

        // Initialize project data
        Initialize(uXUIProjectSO[index]);

        // Disable all galleries
        foreach (var gallery in uXUIGalleries)
        {
            if (gallery != null) gallery.SetActive(false);
        }

        // Enable the selected gallery
        if (uXUIGalleries[index] != null)
        {
            uXUIGalleries[index].SetActive(true);
        }
    }

    public void Initialize(UXUIProjectSO data)
    {
        projectImage.sprite = data.projectBanner;
        projectName.text = data.projectName;
        projectSubtext.text = data.projectSubtext;
        projectRole.text = data.projectRole;
        projectDate.text = data.projectDate;
        projectTools.text = data.projectTools;
        projectDescription.StringReference = data.projectDescription;
        projectRetrospective.StringReference = data.projectRetrospective;
    }
}
