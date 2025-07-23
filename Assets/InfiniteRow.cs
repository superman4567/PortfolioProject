using UnityEngine;
using System.Collections.Generic;

public class InfiniteChildren : MonoBehaviour
{
    public RectTransform viewportRect;
    public List<RectTransform> itemsList;
    public float scrollSpeed = 50f;
    public float spacing = 32f;

    private float itemWidth;
    private float totalRowWidth;
    private List<float> initialX;
    private float offset;

    public void Start()
    {
        int count = itemsList.Count;
        if (count == 0) return;
        itemWidth = itemsList[0].rect.width;
        totalRowWidth = count * (itemWidth + spacing);
        initialX = new List<float>(count);
        for (int i = 0; i < count; i++)
        {
            float x = i * (itemWidth + spacing);
            initialX.Add(x);
            itemsList[i].anchoredPosition = new Vector2(x, itemsList[i].anchoredPosition.y);
        }
    }

    private void Update()
    {
        offset = (offset + scrollSpeed * Time.deltaTime) % totalRowWidth;
        float viewWidth = viewportRect.rect.width;
        for (int i = 0; i < itemsList.Count; i++)
        {
            float x = initialX[i] + offset;
            if (x > viewWidth)
                x -= totalRowWidth;
            else if (x < -itemWidth)
                x += totalRowWidth;
            itemsList[i].anchoredPosition = new Vector2(x, itemsList[i].anchoredPosition.y);
        }
    }
}
