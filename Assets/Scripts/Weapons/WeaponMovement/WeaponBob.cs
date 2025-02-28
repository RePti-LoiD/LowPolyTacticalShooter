using UnityEngine;

public class WeaponBob : MonoBehaviour
{
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

    private Vector2 walkInput;

    private float bobWeight = 1f;
    private float currentSpeed;
    private bool isGrounded;
    
    private float CurveSin { get => Mathf.Sin(speedCurve); }
    private float CurveCos { get => Mathf.Cos(speedCurve); }
    public float BobWeight 
    { 
        get => bobWeight; 
        set
        {
            bobWeight = Mathf.Clamp(value, 0, 1);
        }
    }

    public void SetBobWeight(float weight) =>
        BobWeight = weight;

    public void OnMove(Vector2 input) =>
        walkInput = input.normalized;

    public void OnGroundedChanged(bool isGrounded) =>
        this.isGrounded = isGrounded;

    public void OnMovementSpeedChange(float speed) =>
        currentSpeed = speed;

    private void Update()
    {
        BobOffset();
        BobRotation();

        CompositePositionRotation();
    }

    private void CompositePositionRotation()
    {

        print(BobWeight);

        transform.localPosition = (Vector3.Lerp(transform.localPosition, bobPosition * (isGrounded ? 1 : 0) * (walkInput != Vector2.zero ? 1f : 0), Time.deltaTime * smooth)) * BobWeight;

        transform.localRotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Slerp(transform.localRotation, Quaternion.Euler(bobEulerRotation * (isGrounded ? 1 : 0) * (walkInput != Vector2.zero ? 1f : 0)), Time.deltaTime * smoothRot), BobWeight);
    }

    private void BobOffset()
    {
        speedCurve += (isGrounded ? (Mathf.Clamp(Mathf.Abs(walkInput.x) + Mathf.Abs(walkInput.y), -1, 1)) * bobExaggeration : 1f) * currentSpeed * Time.deltaTime;

        bobPosition.x = (CurveCos * bobLimit.x) - (-walkInput.x * travelLimit.x);
        bobPosition.y = (Mathf.Abs(CurveSin) * bobLimit.y) - (Mathf.Abs(walkInput.y) * travelLimit.y);
        bobPosition.z = -(walkInput.y * travelLimit.z);
    }

    private void BobRotation()
    {
        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : 0);
        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * CurveCos : 0);
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * CurveCos * walkInput.x : 0);
    }
}