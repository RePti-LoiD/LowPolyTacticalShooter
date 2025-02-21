using UnityEngine;

public class WeaponShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeFreq;
    [SerializeField] private float inputWeight;
    [SerializeField] private float mouseInputWeight;
    [SerializeField] private float lerpValue;

    [Space]
    [SerializeField] private float returnSpeed;

    private float time;
    private float Cos { get => Mathf.Abs(Mathf.Cos(shakeFreq * time)) * shakeAmount; }
    private float Sin { get => Mathf.Sin(shakeFreq * time) * shakeAmount; }

    private Vector2 mouseInput;
    private bool isGrounded;

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * returnSpeed);
    }

    public void OnIsGroundedChanged(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    public void OnMove(Vector2 input)
    {
        if (!enabled) return;
        if (isGrounded && input != Vector2.zero) time += Time.deltaTime;

        input = -input;
        
        MoveWeapon(input);
    }

    private void MoveWeapon(Vector2 input)
    {
        transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
                    new Vector3(
                        Sin + input.x * inputWeight + mouseInput.x * mouseInputWeight,
                        Cos + mouseInput.y * mouseInputWeight,
                        input.y * inputWeight
                    ),
                    Time.deltaTime * lerpValue
                );
    }
}