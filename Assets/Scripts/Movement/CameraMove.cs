using UnityEngine;

public class CameraMove : MouseMovementControllable
{
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] [Range(0, 180)] private int maxVerticalCameraAngle = 90;
    [SerializeField] private float zTiltCenterSpeed;
    [SerializeField] private int maxZTilt;

    [SerializeField] private Transform cameraTransform;
    private Vector2 currentRotation;

    private float zTilt;

    private void Start()
    {    
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnCameraMove(Vector2 mouseInputs)
    {
        mouseInputs *= movementSettings.Sensitivity;

        currentRotation.x += mouseInputs.x;
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseInputs.y, -maxVerticalCameraAngle, maxVerticalCameraAngle);

        playerRb.MoveRotation(Quaternion.Euler(new Vector3(0, currentRotation.x, 0)));
        
        if (movementSettings.ZTiltCameraEnabled)
            CalculateZTilt(mouseInputs);

        cameraTransform.localRotation = Quaternion.Euler(new Vector3(-currentRotation.y, 0f, zTilt));
    }

    private void CalculateZTilt(Vector2 mouseInputs)
    {
        zTilt = Mathf.Clamp(zTilt - mouseInputs.x, -maxZTilt, maxZTilt);
        zTilt = Mathf.Lerp(zTilt, 0, Time.deltaTime * zTiltCenterSpeed);
    }
}