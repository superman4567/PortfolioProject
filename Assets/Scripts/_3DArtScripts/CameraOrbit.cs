using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraOrbit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform target;
    public RectTransform UIRect;
    public Camera targetCamera;
    public float rotationSpeed = 10f;
    public float verticalSpeed = 10f;
    public float zoomSpeed = 10f;
    public float minDistance = 2f;
    public float maxDistance = 20f;
    public float minHeight = -5f;
    public float maxHeight = 5f;

    private float currentDistance;
    private float verticalOffset;
    private float currentAngleY;

    private bool canInteract = false;

    void Start()
    {
        if (target == null || targetCamera == null)
            return;

        currentDistance = Vector3.Distance(targetCamera.transform.position, target.position);
        currentAngleY = targetCamera.transform.eulerAngles.y;
        verticalOffset = 0f;
    }

    void Update()
    {
        if (canInteract)
        {
            HandleScrollInput();
        }
    }

    private void HandleScrollInput()
    {
        if (target == null || targetCamera == null)
            return;

        if (Input.GetMouseButton(0))
        {
            currentAngleY += Input.GetAxis("Mouse X") * rotationSpeed;
            verticalOffset -= Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;
        }

        verticalOffset = Mathf.Clamp(verticalOffset, minHeight, maxHeight);

        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance - scroll, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(0, currentAngleY, 0);
        Vector3 position = target.position - rotation * Vector3.forward * currentDistance;
        position.y = target.position.y + verticalOffset;

        targetCamera.transform.position = position;
        targetCamera.transform.LookAt(target);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        verticalOffset = 0f;
        currentDistance = Vector3.Distance(targetCamera.transform.position, target.position);
        currentAngleY = targetCamera.transform.eulerAngles.y;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canInteract = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canInteract = false;
    }
}
