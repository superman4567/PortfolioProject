using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class ThreeDProjectHandler : MonoBehaviour
{
    [SerializeField] private ThreeDProjectSO[] threeDProjectSO;
    [SerializeField] private GameObject[] threeDGalleries;

    [Space]

    [SerializeField] private TextMeshProUGUI modelName;
    [SerializeField] private LocalizeStringEvent modelDescription;

    private void Awake()
    {
        SetProjectByEnum(EnumThreeDProjects.BoundForest); 
    }

    private void OnEnable()
    {
        ThreeDProjectButton.OnProjectButtonClicked += SetProjectByEnum;
        ThreeDProjectButton.OnProjectButtonHovered += SetProjectByEnum;
    }

    private void OnDisable()
    {
        ThreeDProjectButton.OnProjectButtonClicked -= SetProjectByEnum;
        ThreeDProjectButton.OnProjectButtonHovered -= SetProjectByEnum;
    }

    public void SetProjectByEnum(EnumThreeDProjects projectEnum)
    {
        int index = (int)projectEnum;

        if (index < 0 || index >= threeDProjectSO.Length || index >= threeDGalleries.Length)
        {
            Debug.LogWarning($"Invalid project enum index: {index}", this);
            return;
        }

        // Initialize project data
        Initialize(threeDProjectSO[index]);

        // Disable all galleries
        foreach (var gallery in threeDGalleries)
        {
            if (gallery != null) gallery.SetActive(false);
        }

        // Enable the selected gallery
        if (threeDGalleries[index] != null)
        {
            threeDGalleries[index].SetActive(true);
        }
    }

    public void Initialize(ThreeDProjectSO data)
    {
        modelName.text = data.modelName;
        modelDescription.StringReference = data.modelDescription;
    }
}
