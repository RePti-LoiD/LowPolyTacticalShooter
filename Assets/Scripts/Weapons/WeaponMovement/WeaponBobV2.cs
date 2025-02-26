using UnityEngine;

public class WeaponBobV2 : MonoBehaviour
{
    [Header("Sway")]
    [SerializeField] private float step = 0.01f;
    [SerializeField] private float maxStepDistance = 0.06f;
    private Vector3 swayPos;

    [Header("Sway Rotation")]
    [SerializeField] private float rotationStep = 4f;
    [SerializeField] private float maxRotationStep = 5f;
    private Vector3 swayEulerRot;

    [SerializeField] private float smooth = 10f;
    [SerializeField] private float smoothRot = 12f;

    [Header("Bobbing")]
    [SerializeField] private float speedCurve;

    [SerializeField] private Vector3 travelLimit = Vector3.one * 0.025f;
    [SerializeField] private Vector3 bobLimit = Vector3.one * 0.01f;
    [SerializeField] private float bobExaggeration;
    
    private Vector3 bobPosition;

    [Header("Bob Rotation")]
    [SerializeField] private Vector3 multiplier;
    private Vector3 bobEulerRotation;

    private Vector3 startPosition;
    private Quaternion startRotation;

    Vector2 walkInput;

    private float currentSpeed;
    private bool isGrounded;
    
    private float curveSin { get => Mathf.Sin(speedCurve); }
    private float curveCos { get => Mathf.Cos(speedCurve); }


    public void OnMove(Vector2 input) =>
        walkInput = input.normalized;

    public void OnGroundedChanged(bool isGrounded) =>
        this.isGrounded = isGrounded;

    public void OnMovementSpeedChange(float speed) =>
        currentSpeed = speed;

    private void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        BobOffset();
        BobRotation();

        CompositePositionRotation();
    }

    private void CompositePositionRotation()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, bobPosition * (isGrounded ? 1 : 0) * (walkInput != Vector2.zero ? 1f : 0), Time.deltaTime * smooth);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(bobEulerRotation * (isGrounded ? 1 : 0) * (walkInput != Vector2.zero ? 1f : 0)), Time.deltaTime * smoothRot);
    }

    private void BobOffset()
    {
        speedCurve += (isGrounded ? (Mathf.Clamp(Mathf.Abs(walkInput.x) + Mathf.Abs(walkInput.y), -1, 1)) * bobExaggeration : 1f) * currentSpeed * Time.deltaTime;

        bobPosition.x = (curveCos * bobLimit.x) - (Mathf.Abs(walkInput.x) * travelLimit.x);
        bobPosition.y = (Mathf.Abs(curveSin) * bobLimit.y) - (Mathf.Abs(walkInput.y) * travelLimit.y);
        bobPosition.z = -(walkInput.y * travelLimit.z);
    }

    private void BobRotation()
    {
        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : 0);
        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * curveCos : 0);
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * curveCos * walkInput.x : 0);
    }
}