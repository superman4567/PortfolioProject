using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDProjectHandler : MonoBehaviour
{
    [Serializable]
    private class ButtonGameObjectPair
    {
        [SerializeField] private Button threeDProjectButton;
        [SerializeField] private GameObject threeDGalleryObject;
        [SerializeField] private GameObject threeDModel;
    }

    [SerializeField] private ButtonGameObjectPair[] threeDProjectButtonGameObjectPairs;

    [Space]

    [SerializeField] private CanvasGroup buttonList;
    [SerializeField] private CanvasGroup galleryContainer;



}
