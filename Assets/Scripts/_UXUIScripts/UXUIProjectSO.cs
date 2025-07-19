using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "UXUIProject", menuName = "UXUI/Project")]
public class UXUIProjectSO : ScriptableObject
{
    public string projectName;
    public string projectSubtext;

    public string projectRole;
    public string projectDate;
    public string projectTools;

    [Space]

    public LocalizedString projectDescription;
    public LocalizedString projectRetrospective;
}
