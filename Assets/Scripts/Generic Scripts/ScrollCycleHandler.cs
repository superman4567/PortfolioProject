using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollCycleHandler : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float scrollThreshold = 0.1f; // How much scroll is needed to trigger a cycle
    public float scrollCooldown = 0.2f;  // Cooldown between cycles on PC

    private List<RectTransform> buttonList = new List<RectTransform>();
    private float timeSinceLastScroll = 0f;
    private float buttonHeight;

    void Start()
    {
        if (scrollRect == null) scrollRect = GetComponent<ScrollRect>();
        if (content == null) content = scrollRect.content;

        foreach (Transform child in content)
        {
            if (child.GetComponent<Button>())
                buttonList.Add(child as RectTransform);
        }

        if (buttonList.Count > 0)
        {
            buttonHeight = buttonList[0].rect.height;
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandlePCInput();
#endif
        timeSinceLastScroll += Time.unscaledDeltaTime;
    }

    void HandlePCInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > scrollThreshold && timeSinceLastScroll >= scrollCooldown)
        {
            CycleUp();
            timeSinceLastScroll = 0f;
        }
        else if (scroll < -scrollThreshold && timeSinceLastScroll >= scrollCooldown)
        {
            CycleDown();
            timeSinceLastScroll = 0f;
        }
    }

    void CycleUp()
    {
        if (buttonList.Count == 0) return;

        RectTransform first = buttonList[0];
        buttonList.RemoveAt(0);
        buttonList.Add(first);

        first.SetAsLastSibling();
        content.anchoredPosition += new Vector2(0, buttonHeight);
    }

    void CycleDown()
    {
        if (buttonList.Count == 0) return;

        RectTransform last = buttonList[buttonList.Count - 1];
        buttonList.RemoveAt(buttonList.Count - 1);
        buttonList.Insert(0, last);

        last.SetAsFirstSibling();
        content.anchoredPosition -= new Vector2(0, buttonHeight);
    }
}
