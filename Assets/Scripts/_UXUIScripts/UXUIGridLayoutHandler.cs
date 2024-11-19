using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXUIGridLayoutHandler : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform tempContainer;

    [Space]

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GridLayoutGroup gridLayout;

    [Space]

    [SerializeField] private GameObject thumbnailPrefab;
    [SerializeField] private float spacing = 10f;
   
   

    private void Start()
    {
        gridLayout.spacing = new Vector2(spacing, spacing);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 3;

        SetGridCellSize();
    }

    private void SetGridCellSize()
    {
        float containerWidth = container.rect.width;
        float cellWidth = (containerWidth - (2 * spacing)) / 3;
        float cellHeight = cellWidth * 9 / 16;
        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }

    public void GenerateGrid(UXUIProjectSO project)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < project.uXUIProjectContent.Count; i++)
        {
            UXUIProjectContent data = project.uXUIProjectContent[i];

            GameObject cell = Instantiate(thumbnailPrefab, container);

            Transform child = cell.transform.GetChild(0);
            Transform childOfChild = child.transform.GetChild(0);

            Image img = childOfChild.GetComponent<Image>(); 
            img.sprite = data.image;
            img.preserveAspect = true;

            UXUIButtonHandler handler = cell.GetComponent<UXUIButtonHandler>();
            handler.Initialize(container, tempContainer, scrollRect, mainCanvas);

        }
    }
}
