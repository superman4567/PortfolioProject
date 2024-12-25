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
    
    [SerializeField] private TextMeshProUGUI modelFaces;
    [SerializeField] private TextMeshProUGUI modelVerts;
    [SerializeField] private Image uVLayoutTexture;

    [Space]

    [SerializeField] private Color inactiveButtonColor = new Color(0.7f, 0.7f, 0.7f);
    private List<GameObject> spawnedPrefabs = new();
    private GameObject activePrefab;

    private void Start()
    {
        SpawnAllContentItems();
        InitializeFirstItem();
        SetSpecRelatedData();
    }

    private void SpawnAllContentItems()
    {
        foreach (var mediaItem in projectContent.MediaItems)
        {
            GameObject prefabInstance = Instantiate(imagePrefab, parent);
            spawnedPrefabs.Add(prefabInstance);

            Button button = prefabInstance.GetComponent<Button>();
            if (button != null)
            {
                MediaItem localMediaItem = mediaItem;
                button.onClick.AddListener(() => OnPrefabClicked(prefabInstance, localMediaItem.Image));
                button.image.sprite = localMediaItem.Image;
            }
        }
    }

    private void InitializeFirstItem()
    {
        if (spawnedPrefabs.Count > 0 && projectContent.MediaItems.Count > 0)
        {
            SetActiveProject(spawnedPrefabs[0]);
            SetPreviewImage(projectContent.MediaItems[0].Image);
        }
        else
        {
            Debug.LogWarning("No spawned prefabs or media items to initialize.");
        }
    }

    private void OnPrefabClicked(GameObject prefab, Sprite image)
    {
        if (prefab == activePrefab) return;

        SetActiveProject(prefab);
        SetPreviewImage(image);
    }

    private void SetActiveProject(GameObject prefab)
    {
        if (activePrefab != null)
        {
            activePrefab.GetComponent<Image>().color = inactiveButtonColor;
        }

        activePrefab = prefab;
        activePrefab.GetComponent<Image>().color = Color.white;
    }

    private void SetPreviewImage(Sprite image)
    {
        previewImage.sprite = image;
    }

    private void SetSpecRelatedData()
    {
        modelName.text = projectContent.modelName ?? "UNKNOWN NAME";
        modelStyle.text = projectContent.modelStyle ?? "UNKNOWN STYLE";
        modelFaces.text = projectContent.modelFaces.ToString();
        modelVerts.text = projectContent.modelVerts.ToString();
        SetTextureOrHide(uVLayoutTexture, projectContent.uVLayoutTexture);
    }

    private void SetTextureOrHide(Image image, Sprite texture)
    {
        if (image == null) return;
        if (texture != null)
        {
            image.sprite = texture;
            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }
}
