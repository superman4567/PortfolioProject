using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CorperateProjectCollection : MonoBehaviour
{
    [Header("Scriptable object")]
    [SerializeField] private CorperateProjectSO projectSO;

    [Header("Tweening")]
    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private TextMeshProUGUI roleText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI toolsText;
    [SerializeField] private LocalizeStringEvent descriptionText;

    void Start()
    {
        Initialize();
        
    }

    private void Initialize()
    {
        projectName.text = projectSO.projectName;
        roleText.text = projectSO.projectRole;
        dateText.text = projectSO.projectDate;
        toolsText.text = projectSO.projectTools;
        descriptionText.StringReference = projectSO.projectDescription;
    }
}
