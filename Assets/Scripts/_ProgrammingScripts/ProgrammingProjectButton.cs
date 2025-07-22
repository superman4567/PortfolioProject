using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProgrammingProjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ProgrammingProjectSO projectSO;

    [Space]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonTextName;
    [SerializeField] private CanvasGroup checkmarkGroup;  // <-- now a CanvasGroup

    [Header("Hover Effects")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float tweenDuration = 0.2f;
    [SerializeField] private Color normalColor = new Color(0xBA / 255f, 0xBA / 255f, 0xBA / 255f);
    [SerializeField] private Color hoverColor = Color.white;

    private Vector3 originalScale;
    private Tween scaleTween, colorTween;

    public static event Action<EnumProgrammingProjects> OnProjectButtonClicked;

    private void Awake()
    {
        originalScale = transform.localScale;
        buttonTextName.text = projectSO.projectName;
        button.onClick.AddListener(() => OnProjectButtonClicked?.Invoke(projectSO.projectType));

        if (projectSO.projectType == EnumProgrammingProjects.BoundForest)
            return;

        checkmarkGroup.alpha = 0f;
        checkmarkGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        OnProjectButtonClicked += HandleProjectClicked;
    }

    private void OnDisable()
    {
        OnProjectButtonClicked -= HandleProjectClicked;
    }

    private void HandleProjectClicked(EnumProgrammingProjects clickedProject)
    {
        bool isThis = clickedProject == projectSO.projectType;
        // fade instantly (or you can DOTween it if you like):
        checkmarkGroup.alpha = isThis ? 1f : 0f;
        checkmarkGroup.blocksRaycasts = isThis;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // scale up
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale * hoverScale, tweenDuration)
            .SetEase(Ease.OutBack);

        // text color
        colorTween?.Kill();
        colorTween = buttonTextName
            .DOColor(hoverColor, tweenDuration)
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // scale back
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale, tweenDuration)
            .SetEase(Ease.OutBack);

        // text color back
        colorTween?.Kill();
        colorTween = buttonTextName
            .DOColor(normalColor, tweenDuration)
            .SetEase(Ease.OutQuad);
    }
}
