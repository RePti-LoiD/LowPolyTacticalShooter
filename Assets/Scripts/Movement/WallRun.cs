using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Movement))]
public class WallRun : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private MovementSettings movementSettings;

    [Header("Wall run")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private float wallCheckRaycastLength;
    [SerializeField] private float wallRunSpeedTreshold;

    public bool IsWallRun { get; protected set; } = false;
    private Vector3 currentWallNormal;
    public WallSide WallSide { get; protected set; }

    private Rigidbody playerRb;
    private Movement movement;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        movement = GetComponent<Movement>();
    }

    public void OnMovementUpdate()
    {
        currentWallNormal = GetWall();

        WallRunStateHandle();
    }

    private Vector3 GetWall()
    {
        Debug.DrawRay(transform.position, transform.right * wallCheckRaycastLength, Color.red);
        Debug.DrawRay(transform.position, -transform.right * wallCheckRaycastLength, Color.green);

        if (Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallCheckRaycastLength, mask) ^ Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallCheckRaycastLength, mask))
        {
            if (rightHit.collider == null)
                return leftHit.normal;
            else
                return rightHit.normal;
        }

        return Vector3.zero;
    }

    public void WallRunStateHandle()
    {
        IsWallRun = false;

        if (currentWallNormal == Vector3.zero) return;
        if (movement.HorizontalSpeed <= wallRunSpeedTreshold) return;
        if (movement.IsGrounded) return;

        IsWallRun = true;

        StartWallRun();
    }

    public void StartWallRun()
    {
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        movement.CurrentDirection *= movementSettings.WallRunSpeed;

        movement.SetCurrentJumpCount(0);
        movement.AddCurrentSpeed(movementSettings.WallRunSpeed);
    }

    public void OnJump()
    {
        if (!IsWallRun) return;

        movement.SetAdditionalHorizonalImpulse(movementSettings.WallRunJumpAdditionalImpuls.x);
    }
}