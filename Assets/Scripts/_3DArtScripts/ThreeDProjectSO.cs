using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "ProjectContent3DSO", menuName = "Scriptable Objects/ProjectContent3DSO")]
public class ThreeDProjectSO : ScriptableObject
{
    public string modelName;
    public LocalizedString modelDescription;
    public GameObject modelPrefab;
    public int modelTriangles;
}
