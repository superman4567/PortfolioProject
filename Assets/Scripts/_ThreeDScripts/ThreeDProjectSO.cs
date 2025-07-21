using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ProjectContent3DSO", menuName = "Scriptable Objects/ProjectContent3DSO")]
public class ThreeDProjectSO : ScriptableObject
{
    public EnumThreeDProjects projectType;
    public string projectName;
    public string modelName;
    public int modelTriangles;
    public LocalizedString modelDescription;
}
