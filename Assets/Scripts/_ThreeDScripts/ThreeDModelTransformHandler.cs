using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThreeDModelTransformHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private bool canInteract = false;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        canInteract = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canInteract = false;
    }
}
