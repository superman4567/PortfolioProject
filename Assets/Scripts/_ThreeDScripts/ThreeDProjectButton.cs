using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ThreeDProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ThreeDProjectSO projectSO;

    [Space]
    [Header("Button Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonTextName;
    [SerializeField] private TextMeshProUGUI buttonTextTriangleCount;
    [SerializeField] private Image buttonImage;

    [Space]
    [Header("Hover Effects")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;
    [SerializeField] private Color normalColor = new Color(0xBA / 255f, 0xBA / 255f, 0xBA / 255f);
    [SerializeField] private Color hoverColor = Color.white;

    private Vector3 originalScale;
    private Tween scaleTween;
    private Tween colorTween;

    public static event Action<EnumThreeDProjects> OnProjectButtonClicked;
    public static event Action<EnumThreeDProjects> OnProjectButtonHovered;

    private const float clickBlockDuration = 2f;

    private static bool hoverGloballyBlocked = false;
    private static Coroutine blockHoverCoroutine;

    private void Awake()
    {
        originalScale = transform.localScale;
        buttonTextName.text = projectSO.projectName;
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        scaleTween?.Kill();
        colorTween?.Kill();
    }

    private void OnClick()
    {
        OnProjectButtonClicked?.Invoke(projectSO.projectType);
        StartGlobalHoverBlock();
    }

    private void StartGlobalHoverBlock()
    {
        if (!hoverGloballyBlocked)
        {
            hoverGloballyBlocked = true;
            if (blockHoverCoroutine != null)
                StopCoroutine(blockHoverCoroutine); // should be stopped on a MonoBehaviour

            blockHoverCoroutine = StartCoroutine(GlobalHoverCooldown());
        }
    }

    private IEnumerator GlobalHoverCooldown()
    {
        yield return new WaitForSeconds(clickBlockDuration);
        hoverGloballyBlocked = false;
        blockHoverCoroutine = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverGloballyBlocked) return;

        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale * hoverScale, tweenDuration)
            .SetEase(Ease.OutBack);

        colorTween?.Kill();
        colorTween = buttonImage
            .DOColor(hoverColor, tweenDuration)
            .SetEase(Ease.OutQuad);

        ChangeDisplayedProject();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale, tweenDuration)
            .SetEase(Ease.OutBack);

        colorTween = buttonImage
            .DOColor(normalColor, tweenDuration)
            .SetEase(Ease.OutQuad);
    }

    private void ChangeDisplayedProject()
    {
        OnProjectButtonHovered?.Invoke(projectSO.projectType);
    }
}
