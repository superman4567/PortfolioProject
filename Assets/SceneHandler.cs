using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [Header("First Time Entering")]
    [SerializeField] private CanvasGroup pressSpaceToStart;
    [SerializeField] private Button pressSpaceToStartButton;
    [SerializeField] private Transform startPosMM;
    [SerializeField] private Transform endPosMM;

    [Header("Entering")]
    [SerializeField] private GameObject[] CategoryButtons;
    
    void Start()
    {
        ShowPressSpaceToStart();

        AssignOnclicks();
    }

    private void AssignOnclicks()
    {
        pressSpaceToStartButton.onClick.AddListener(ShowCatergoryOptions);
    }

    private void ShowPressSpaceToStart()
    {
        pressSpaceToStart.alpha = 1.0f;
        pressSpaceToStart.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        pressSpaceToStart.interactable = true;
    }

    private void ShowCatergoryOptions()
    {

    }

    private void LerpToBase()
    {

    }
}
