using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ProjectContentProgrammingSO", menuName = "Scriptable Objects/ProjectContentProgrammingSO")]
public class ProgrammingProjectSO : ScriptableObject
{
    public EnumProgrammingProjects projectType;
    public Sprite projectThumbnail;
    public Sprite projectBanner;

    public string projectName;

    public string projectRole;
    public string projectDate;
    public string projectTools;

    [Space]

    public LocalizedString projectDescription;
    public LocalizedString projectRetrospective;
}
