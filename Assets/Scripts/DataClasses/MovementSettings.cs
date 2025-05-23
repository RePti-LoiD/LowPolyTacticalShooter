using UnityEngine;

[CreateAssetMenu(fileName = "Create movement settings asset", menuName = "Scriptable Objects/MovementSettings")]
class MovementSettings : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private float groundKeyboardLerpValue;
    [SerializeField] private float airKeyboardLerpValue;
    
    [Header("Camera movement")]
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float zTiltCenterSpeed;
    [SerializeField] private int maxZTilt;

    [Space]
    [Space]
    [Header("Body movement")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 additionalJumpImpuls;
    [SerializeField] private int jumpCount;
    [SerializeField] private float sprintTime;
    [SerializeField] private float sprintTimeTreshold;
    [SerializeField] private float sprintSpeedTreshold;
    [SerializeField] private float timeToRefillSprint;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float onFlyCrouchVelocitySpeed;

    [Header("Wall run")]
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Vector2 wallRunJumpAdditionalImpuls;

    [Header("Enables")]
    [SerializeField] private bool jumpLandingCameraRotationEnabled;
    [SerializeField] private bool keyboardLerpEnabled;
    [SerializeField] private bool zTiltCameraEnabled;
    
    public float GroundKeyboardLerpValue { get => groundKeyboardLerpValue; set => groundKeyboardLerpValue = value; }
    public float AirKeyboardLerpValue { get => airKeyboardLerpValue; set => airKeyboardLerpValue = value; }
    
    
    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
    public float ZTiltCenterSpeed { get => zTiltCenterSpeed; set => zTiltCenterSpeed = value; }
    public int MaxZTilt { get => maxZTilt; set => maxZTilt = value; }

    public float DefaultSpeed { get => defaultSpeed; set => defaultSpeed = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public Vector2 AdditionalJumpImpuls { get => additionalJumpImpuls; set => additionalJumpImpuls = value; }
    public int JumpCount { get => jumpCount; set => jumpCount = value; }
    public float SprintTime { get => sprintTime; set => sprintTime = value; }
    public float SprintTimeTreshold { get => sprintTimeTreshold; set => sprintTimeTreshold = value; }
    public float SprintSpeedTreshold { get => sprintSpeedTreshold; set => sprintSpeedTreshold = value; }
    public float TimeToRefillSprint { get => timeToRefillSprint; set => timeToRefillSprint = value; }


    public float OnFlyCrouchVelocitySpeed { get => onFlyCrouchVelocitySpeed; set => onFlyCrouchVelocitySpeed = value; }
    public float CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    
    
    public float WallRunSpeed { get => wallRunSpeed; set => wallRunSpeed = value; }
    public Vector2 WallRunJumpAdditionalImpuls { get => wallRunJumpAdditionalImpuls; set => wallRunJumpAdditionalImpuls = value; }


    public bool JumpLandingCameraRotationEnabled { get => jumpLandingCameraRotationEnabled; set => jumpLandingCameraRotationEnabled = value; }
    public bool KeyboardLerpEnabled { get => keyboardLerpEnabled; set => keyboardLerpEnabled = value; }
    public bool ZTiltCameraEnabled { get => zTiltCameraEnabled; set => zTiltCameraEnabled = value; }
}