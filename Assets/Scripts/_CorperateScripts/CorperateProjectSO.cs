using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "CorperateProject", menuName = "Corperate/Project")]
public class CorperateProjectSO : ScriptableObject
{
    public string projectRole;
    public string projectDate;
    public string projectTools;
    public LocalizedString projectDescription;
}
