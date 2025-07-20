using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UXUIProject", menuName = "UXUI/Project")]
public class UXUIProjectSO : ScriptableObject
{
    public EnumUXUIProjects projectType;
    public Sprite projectBanner;

    public string projectName;
    public string projectSubtext;

    public string projectRole;
    public string projectDate;
    public string projectTools;

    [Space]

    public LocalizedString projectDescription;
    public LocalizedString projectRetrospective;
}
