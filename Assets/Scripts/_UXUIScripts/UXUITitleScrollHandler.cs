using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UXUITitleScrollHandler : MonoBehaviour
{
    public event Action<int> OnChangeActiveProject;

    public List<Image> imagesList;
    public List<Transform> positions;
    public float transitionDuration = 0.5f;
    public int middleIndex = 2;
    public Color activeColor = Color.green;
    public Color normalColor = Color.white;
    public Color activeTextColor = Color.yellow; 
    public Color normalTextColor = Color.gray;  
    public float scrollDelay = 0.2f;

    private int activeIndex = 0;
    private bool canScroll = true;
    [SerializeField] private RectTransform rectTransformMouseDetection;
    [SerializeField] private RectTransform projectsTitlesContainer;

    private void Start()
    {
        InitialzieList();
        Invoke(nameof(UpdateVisibleButtons), 0.1f);
    }

    private void InitialzieList()
    {
        foreach (Transform child in projectsTitlesContainer)
        {
            Image image = child.GetComponent<Image>();
            imagesList.Add(image);
        }
    }

    private void Update()
    {
        if (canScroll && IsMouseOver())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) ScrollUp();
            else if (scroll < 0f) ScrollDown();
        }
    }

    private bool IsMouseOver()
    {
        Vector2 localMousePosition = rectTransformMouseDetection.InverseTransformPoint(Input.mousePosition);
        bool isInside = rectTransformMouseDetection.rect.Contains(localMousePosition);
        return isInside;
    }

    public void ScrollUp()
    {
        if (canScroll)
        {
            activeIndex = (activeIndex + 1) % imagesList.Count;
            UpdateVisibleButtons();
            StartCoroutine(ScrollCooldown());
        }
    }

    public void ScrollDown()
    {
        if (canScroll)
        {
            activeIndex = (activeIndex - 1 + imagesList.Count) % imagesList.Count;
            UpdateVisibleButtons();
            StartCoroutine(ScrollCooldown());
        }
    }

    private IEnumerator ScrollCooldown()
    {
        canScroll = false; // Prevent further scrolling
        yield return new WaitForSeconds(scrollDelay); // Wait for the specified delay
        canScroll = true; // Allow scrolling again
    }

    private int GetPositionIndex(int imageIndex, int visibleStart)
    {
        int relativeIndex = (imageIndex - visibleStart + imagesList.Count) % imagesList.Count;
        return relativeIndex < positions.Count ? relativeIndex : -1;
    }

    private void UpdateVisibleButtons()
    {
        int visibleStart = (activeIndex - middleIndex + imagesList.Count) % imagesList.Count;

        for (int i = 0; i < imagesList.Count; i++)
        {
            int positionIndex = GetPositionIndex(i, visibleStart);

            if (positionIndex == 2)
            {
                OnChangeActiveProject?.Invoke(i);
            }

            if (positionIndex != -1)
            {
                imagesList[i].gameObject.SetActive(true);
               
                AnimateButtonToPosition(imagesList[i], positions[positionIndex]);

                UpdateButtonOpacity(imagesList[i], positionIndex);
                UpdateButtonColor(imagesList[i], i == activeIndex);
            }
            else
            {
                PrepareAndFadeOutButton(imagesList[i], i < visibleStart ? 0 : 4);
            }
        }
    }

    private void AnimateButtonToPosition(Image image, Transform targetPosition)
    {
        image.transform.DOMove(targetPosition.position, transitionDuration).SetEase(Ease.InOutQuad);
        image.transform.DOScale(targetPosition.localScale, transitionDuration).SetEase(Ease.InOutQuad);
    }

    private void UpdateButtonOpacity(Image button, int positionIndex)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();

        float targetAlpha = positionIndex switch
        {
            0 => 0.33f,
            1 => 0.66f,
            2 => 1f,
            3 => 0.66f,
            4 => 0.33f,
            _ => 0f
        };

        canvasGroup.DOFade(targetAlpha, transitionDuration);
    }

    private void UpdateButtonColor(Image image, bool isActive)
    {
        Color colors = image.color;
        colors = isActive ? activeColor : normalColor;
        image.color = colors;

        TextMeshProUGUI buttonText = image.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.color = isActive ? activeTextColor : normalTextColor;
        }
    }

    private void PrepareAndFadeOutButton(Image button, int targetPositionIndex)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        Transform targetPosition = positions[targetPositionIndex];

        button.transform.position = targetPosition.position;
        button.transform.localScale = targetPosition.localScale;

        canvasGroup.DOFade(0f, transitionDuration).OnComplete(() => button.gameObject.SetActive(false));
    }
}