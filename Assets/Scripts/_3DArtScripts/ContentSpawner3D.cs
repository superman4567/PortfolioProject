using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static ProjectContent3DSO;

public class ContentSpawner3D : MonoBehaviour
{
    [SerializeField] private ProjectContent3DSO projectContent; // ScriptableObject holding MediaItems
    [SerializeField] private GameObject imagePrefab; // Prefab for each media item
    [SerializeField] private Image previewImage; // UI Image for preview
    [SerializeField] private RectTransform parent; // Parent for spawned prefabs

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // List of spawned prefabs
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
}
