using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ProgrammingProjectsHandler : MonoBehaviour
{
    [SerializeField] private ProgrammingProjectSO[] programmingProjectSO;
    [SerializeField] private GameObject[] programmingGalleries;

    [Space]

    [SerializeField] private Image projectThumbnail;
    [SerializeField] private Image projectBanner;
    [SerializeField] private TextMeshProUGUI projectRole;
    [SerializeField] private TextMeshProUGUI projectDate;
    [SerializeField] private TextMeshProUGUI projectTools;
    [SerializeField] private LocalizeStringEvent projectDescription;
    [SerializeField] private LocalizeStringEvent projectRetrospective;


    private void OnEnable()
    {
        ProgrammingProjectButton.OnProjectButtonClicked += SetProjectByEnum;
    }

    private void OnDisable()
    {
        ProgrammingProjectButton.OnProjectButtonClicked -= SetProjectByEnum;
    }

    private void Start()
    {
        SetProjectByEnum(EnumProgrammingProjects.BoundForest);
    }

    public void SetProjectByEnum(EnumProgrammingProjects projectEnum)
    {
        int index = (int)projectEnum;

        if (index < 0 || index >= programmingProjectSO.Length || index >= programmingGalleries.Length)
        {
            Debug.LogWarning($"Invalid project enum index: {index}", this);
            return;
        }

        // Initialize project data
        Initialize(programmingProjectSO[index]);

        // Disable all galleries
        foreach (var gallery in programmingGalleries)
        {
            if (gallery != null) gallery.SetActive(false);
        }

        // Enable the selected gallery
        if (programmingGalleries[index] != null)
        {
            programmingGalleries[index].SetActive(true);
        }
    }

    public void Initialize(ProgrammingProjectSO data)
    {
        projectThumbnail.sprite = data.projectThumbnail;
        projectBanner.sprite = data.projectBanner;
        projectRole.text = data.projectRole;
        projectDate.text = data.projectDate;
        projectTools.text = data.projectTools;
        projectDescription.StringReference = data.projectDescription;
        projectRetrospective.StringReference = data.projectRetrospective;
    }
}
