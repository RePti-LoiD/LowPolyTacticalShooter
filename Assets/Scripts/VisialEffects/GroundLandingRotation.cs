using UnityEngine;

public class GroundLandingRotation : MonoBehaviour
{
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private RecoilRotationSender jumpRotationSender;
    [SerializeField] private RecoilRotationSender landingRotationSender;
    [SerializeField] private float landingMultiplier;

    private Vector2 input;
    private float lastGroundedTime;

    public void OnMovement(Vector2 input)
    {
        this.input = input;
    }

    public void OnIsGroundedChanged(bool isGrounded)
    {
        if (!movementSettings.JumpLandingCameraRotationEnabled) return;

        if (isGrounded)
        {
            landingRotationSender.SendRotation (
                new Vector3 (
                    (Time.time - lastGroundedTime) * landingMultiplier,
                    0,
                    landingMultiplier * (-input.x)
                )
            );

            lastGroundedTime = Time.time;
        }
        else
        {
            jumpRotationSender.SendRotation();
        }
    }
}