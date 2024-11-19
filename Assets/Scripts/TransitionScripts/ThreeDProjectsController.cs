using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDProjectsController : MonoBehaviour
{
    [Header("References")]
    public List<ThreeDProjectsSO> projects;
    [SerializeField] private Transform model3DSpawnLocation;
    [SerializeField] private ThreeDProjectThumbnailHandler threeDProjectThumbnailHandler;

    [Header("Model Tab")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI styleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI dataText;
    [Space]
    [SerializeField] private Image albedoImage;
    [SerializeField] private Image normalImage;
    [SerializeField] private Image roughnessImage;
    [SerializeField] private Image emissionImage;

    [Header("Animations Tab")]


    private Dictionary<ThreeDProjectsSO, GameObject> spawnedModels = new();

    private void OnEnable()
    {
        threeDProjectThumbnailHandler.OnChangeActiveProject += OnChangeActiveModel;
    }

    private void OnDisable()
    {
        threeDProjectThumbnailHandler.OnChangeActiveProject -= OnChangeActiveModel;
    }

    private void Start()
    {
        Initialize3DModels();
        SetData(projects[0]);
        SetFirstModel();
    }

    private void Initialize3DModels()
    {
        foreach (ThreeDProjectsSO modelSO in projects)
        {
            GameObject instantiatedModel = Instantiate(modelSO.model, model3DSpawnLocation);
            instantiatedModel.SetActive(false);

            spawnedModels[modelSO] = instantiatedModel;
        }
    }

    private void SetFirstModel()
    {
        Transform firstChild = model3DSpawnLocation.GetChild(0);
        firstChild.gameObject.SetActive(true);
    }

    private void SetData(ThreeDProjectsSO project)
    {
        headerText.text = project.projectName;
        styleText.text = project.ProjectStyle.ToString();
        descriptionText.text = project.projectDescription;
        dataText.text = $"{project.facesCount} / {project.vertsCount}"; 
    }

    private void OnChangeActiveModel(ThreeDProjectsSO project)
    {
        UpdatePreviewModel(project);
    }

    private void UpdatePreviewModel(ThreeDProjectsSO project)
    {
        foreach (var model in spawnedModels.Values)
        {
            model.SetActive(false);
        }

        if (spawnedModels.ContainsKey(project))
        {
            spawnedModels[project].SetActive(true);
            SetData(project);

            Debug.Log($"Displaying model: {project.projectName}");
        }
        else
        {
            Debug.LogWarning($"No model found for project: {project.projectName}");
        }
    }
}
