using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThreeDButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private Image thubmnail;

    private ThreeDProjectsSO threeDProjectsSO;
    private ThreeDProjectThumbnailHandler threeDProjectThumbnailHandler;

    public void Initialize(ThreeDProjectsSO threeDProjectsSO, ThreeDProjectThumbnailHandler handler)
    {
        this.buttonText.text = threeDProjectsSO.projectName;
        this.threeDProjectsSO = threeDProjectsSO;
        this.thubmnail.sprite = threeDProjectsSO.projectThumbnail;
        this.threeDProjectThumbnailHandler = handler;

        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }

    private void OnClick()
    {
        threeDProjectThumbnailHandler.HandleButtonClick(threeDProjectsSO);
    }
}
