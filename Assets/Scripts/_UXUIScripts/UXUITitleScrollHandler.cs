using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UXUITitleScrollHandler : MonoBehaviour
{
    [SerializeField] private UXUIButtonHandler uXUIButtonHandler;

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
        InitializeTitlesList();
    }

    private void InitializeTitlesList()
    {
        foreach (Transform child in projectsTitlesContainer)
        {
            Image image = child.GetComponent<Image>();
            imagesList.Add(image);
        }

        UpdateVisibleButtons();
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
        return rectTransformMouseDetection.rect.Contains(localMousePosition);
    }

    public void ScrollUp()
    {
        if (canScroll)
        {
            activeIndex = (activeIndex - 1 + imagesList.Count) % imagesList.Count;
            UpdateVisibleButtons();
            StartCoroutine(ScrollCooldown());
        }
    }

    public void ScrollDown()
    {
        if (canScroll)
        {
            activeIndex = (activeIndex + 1) % imagesList.Count;
            UpdateVisibleButtons();
            StartCoroutine(ScrollCooldown());
        }
    }

    private IEnumerator ScrollCooldown()
    {
        canScroll = false;
        yield return new WaitForSeconds(scrollDelay);
        canScroll = true;
    }

    public void SetActiveIndex(int newIndex)
    {
        activeIndex = newIndex;
        UpdateVisibleButtons();
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
                var targetPanel = uXUIButtonHandler.buttonPanelPairs[i].panel;
                uXUIButtonHandler.ActivatePanel(targetPanel);
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
        image.color = isActive ? activeColor : normalColor;

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
