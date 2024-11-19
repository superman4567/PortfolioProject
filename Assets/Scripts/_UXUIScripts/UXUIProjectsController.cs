using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UXUIProjectsController : MonoBehaviour
{
    [SerializeField] private List<UXUIProjectSO> projects;
    [SerializeField] private GameObject UXUITitleprojectPrefab;
    [SerializeField] private Transform projectsTitlesContainer;

    [SerializeField] private UXUITitleScrollHandler scrollHandler;
    [SerializeField] private UXUIGridLayoutHandler dynamicGridLayout;

    private void OnEnable()
    {
        scrollHandler.OnChangeActiveProject += OnChangeActiveProject;
    }

    private void OnDisable()
    {
        scrollHandler.OnChangeActiveProject -= OnChangeActiveProject;
    }

    void Start()
    {
        InitializeUXUITitles();
    }

    private void InitializeUXUITitles()
    {
        foreach (UXUIProjectSO project in projects)
        {
            GameObject instantiatedproject = Instantiate(UXUITitleprojectPrefab, projectsTitlesContainer);
        }
    }

    private void OnChangeActiveProject(int index)
    {
        UpdateShownGrid(projects[index]);
    }

    private void UpdateShownGrid(UXUIProjectSO project)
    {
        dynamicGridLayout.GenerateGrid(project);
    }

}
