using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class UXUIProjectHandler : LocalizedMonoBehaviour
{
    [SerializeField] private UXUIProjectSO uXUIProjectSO;

    [Space]

    [SerializeField] private LocalizeStringEvent projectName;
    [SerializeField] private LocalizeStringEvent projectSubtext;
    [SerializeField] private TextMeshProUGUI projectRole;
    [SerializeField] private TextMeshProUGUI projectDate;
    [SerializeField] private TextMeshProUGUI projectTools;
    [SerializeField] private LocalizeStringEvent projectDescription;
    [SerializeField] private LocalizeStringEvent projectRetrospective;

    private void Awake()
    {
        Initialize(uXUIProjectSO);
    }

    public void Initialize(UXUIProjectSO data)
    {
        projectName.StringReference = data.projectName;
        projectSubtext.StringReference = data.projectSubtext;
        projectRole.text = data.projectRole;
        projectDate.text = data.projectDate;
        projectTools.text = data.projectTools;
        projectDescription.StringReference = data.projectDescription;
        projectRetrospective.StringReference = data.projectRetrospective;
    }
}
