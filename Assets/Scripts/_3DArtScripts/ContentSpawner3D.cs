using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static ProjectContent3DSO;
using TMPro;

public class ContentSpawner3D : MonoBehaviour
{
    [SerializeField] private ProjectContent3DSO projectContent;
    [SerializeField] private GameObject imagePrefab; 
    [SerializeField] private Image previewImage;
    [SerializeField] private RectTransform parent;

    [Space]

    [SerializeField] private TextMeshProUGUI modelName;
    [SerializeField] private TextMeshProUGUI modelStyle;
    [SerializeField] private TextMeshProUGUI modelDescription;

    [SerializeField] private TextMeshProUGUI modelFaces;
    [SerializeField] private TextMeshProUGUI modelVerts;

    [SerializeField] private Image albedoTexture;
    [SerializeField] private Image normalMapTexture;
    [SerializeField] private Image emmisionMapTexture;
    [SerializeField] private Image roughnessMapTexture;

    [SerializeField] private Image uVLayoutTexture;


    private List<GameObject> spawnedPrefabs = new();
    private GameObject activePrefab; // Current active prefab

    private void OnEnable()
    {
        if (spawnedPrefabs.Count > 0)
        {
            SetActivePrefab(spawnedPrefabs[0]); // Activate the first prefab
            SetPreviewImage(projectContent.MediaItems[0].Image); // Set the preview image to the first item's image
        }
    }

    private void Start()
    {
        SpawnAllContentItems();
        SetSpecRelatedData();
    }

    private void SpawnAllContentItems()
    {
        foreach (var mediaItem in projectContent.MediaItems)
        {
            GameObject prefabInstance = Instantiate(imagePrefab, parent);
            spawnedPrefabs.Add(prefabInstance);

            // Assign button functionality
            Button button = prefabInstance.GetComponent<Button>();
            if (button != null)
            {
                // Capture the local scope item to avoid closure issues
                MediaItem localMediaItem = mediaItem;
                button.onClick.AddListener(() => OnPrefabClicked(prefabInstance, localMediaItem.Image));
                button.image.sprite = localMediaItem.Image;
            }
        }
    }

    private void OnPrefabClicked(GameObject prefab, Sprite image)
    {
        SetActivePrefab(prefab); // Update the active prefab
        SetPreviewImage(image); // Update the preview image
    }

    private void SetActivePrefab(GameObject prefab)
    {
        if (activePrefab != null)
        {
            // Disable the current active prefab (e.g., change visuals)
            activePrefab.GetComponent<Image>().color = Color.white; // Reset color as an example
        }

        activePrefab = prefab;

        // Enable the new active prefab (e.g., highlight it)
        activePrefab.GetComponent<Image>().color = Color.yellow; // Highlight active prefab as an example
    }

    private void SetPreviewImage(Sprite image)
    {
        previewImage.sprite = image; // Update the preview image
    }

    private void SetSpecRelatedData()
    {
        if (this.modelName != null && projectContent != null)
            this.modelName.text = projectContent.modelName;

        if (this.modelStyle != null && projectContent != null)
            this.modelStyle.text = projectContent.modelStyle;

        if (this.modelDescription != null && projectContent != null)
            this.modelDescription.text = projectContent.modelDescription;

        if (this.modelFaces != null && projectContent != null)
            this.modelFaces.text = projectContent.modelFaces.ToString();

        if (this.modelVerts != null && projectContent != null)
            this.modelVerts.text = projectContent.modelVerts.ToString();

        if (this.albedoTexture != null && projectContent != null)
            this.albedoTexture.sprite = projectContent.albedoTexture;

        if (this.normalMapTexture != null && projectContent != null)
            this.normalMapTexture.sprite = projectContent.normalMapTexture;

        if (this.emmisionMapTexture != null && projectContent != null)
            this.emmisionMapTexture.sprite = projectContent.emmisionMapTexture;

        if (this.roughnessMapTexture != null && projectContent != null)
            this.roughnessMapTexture.sprite = projectContent.RoughnessMapTexture;

        if (this.uVLayoutTexture != null && projectContent != null)
            this.uVLayoutTexture.sprite = projectContent.uVLayoutTexture;
    }

}
