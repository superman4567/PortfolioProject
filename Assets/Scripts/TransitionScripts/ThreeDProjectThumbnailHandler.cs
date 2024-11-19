using System;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDProjectThumbnailHandler : MonoBehaviour
{
    public event Action<ThreeDProjectsSO> OnChangeActiveProject;

    [SerializeField] private ThreeDProjectsController threeDProjectsController;

    [Header("Thumbnails")]
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform buttonParent;

    [Header("Clips")]
    [SerializeField] private Button clipButtonPrefab;
    [SerializeField] private Transform buttonClipParent;

    private void Start()
    {
        InstantiateAllThumbnails();
        InstantiateAllClips(threeDProjectsController.projects[0]);
    }

    private void InstantiateAllThumbnails()
    {
        foreach (var projectSO in threeDProjectsController.projects)
        {
            Button newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.GetComponent<ThreeDButtonHandler>().Initialize(projectSO, this);
        }
    }

    private void InstantiateAllClips(ThreeDProjectsSO project)
    {
        foreach (Transform child in buttonClipParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < project.videos.Count; i++)
        {
            Button newButton = Instantiate(clipButtonPrefab, buttonClipParent);
            newButton.GetComponent<ThreeDClipButtonHandler>().Initialize(project, i);
        }
    }


    public void HandleButtonClick(ThreeDProjectsSO projectSO)
    {
        OnChangeActiveProject?.Invoke(projectSO);
        InstantiateAllClips(projectSO);
    }
}
