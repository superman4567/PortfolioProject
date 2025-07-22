using UnityEngine;

public class PlateuRotatorMouse : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minYAngle = -15f;
    [SerializeField] private float maxYAngle = 15f;

    private float currentYRotation = 0f;
    private bool isAllowedToRotate = false;

    private void OnEnable()
    {
        ThreeDMouseInputChecker.OnHoverStateChanged += SetRotationAllowed;
    }

    private void OnDisable()
    {
        ThreeDMouseInputChecker.OnHoverStateChanged -= SetRotationAllowed;
    }

    void Start()
    {
        // Initialize with current local Y rotation
        currentYRotation = transform.localEulerAngles.y;

        // Handle angles over 180 correctly (Unity returns 0-360)
        if (currentYRotation > 180f)
            currentYRotation -= 360f;
    }

    void Update()
    {
        if (!isAllowedToRotate)
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float deltaRotation = mouseX * rotationSpeed * Time.deltaTime;

        // Apply rotation delta within clamp range
        currentYRotation = Mathf.Clamp(currentYRotation + deltaRotation, minYAngle, maxYAngle);

        // Apply rotation to the object
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y = currentYRotation;
        transform.localEulerAngles = newRotation;
    }

    private void SetRotationAllowed(bool isAllowed)
    {
        isAllowedToRotate = isAllowed;
    }
}
