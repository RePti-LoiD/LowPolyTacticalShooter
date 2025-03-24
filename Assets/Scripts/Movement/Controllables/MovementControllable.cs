using UnityEngine;
using UnityEngine.Events;

public abstract class MovementControllable : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private BoolEvent OnIsGroundedChanged;
    [SerializeField] private UnityEvent<float> OnHorizonalSpeedChanged;
    [SerializeField] private Vector3Event OnCurrentDirectionChanged;

    [Space]
    [SerializeField] private UnityEvent Jumped;
    [SerializeField] private UnityEvent Landed;

    private bool isGrounded;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            if (isGrounded == value) return;

            if (value)
                Land();
            else
                Jump();
            
            OnIsGroundedChanged.Invoke(value);

            isGrounded = value;
        }
    }

    private float horizontalSpeed;
    public float HorizontalSpeed
    {
        get => horizontalSpeed;
        set
        {
            horizontalSpeed = value;
            OnHorizonalSpeedChanged?.Invoke(value);
        }
    }

    private Vector3 currentDirection;
    public Vector3 CurrentDirection
    {
        get => currentDirection;
        set
        {
            currentDirection = value;
            OnCurrentDirectionChanged.Invoke(value);
        }
    }

    protected virtual void Update()
    {
        IsGrounded = GroundCheck();
        HorizontalSpeed = HorizontalSpeedCalculate();
    }

    protected virtual void Land()
    {
        Landed?.Invoke();
    }

    protected virtual void Jump()
    {
        Jumped?.Invoke();

    }

    public abstract void OnMove(Vector2 direction);
    public abstract void OnDash();
    public abstract void OnDashStop();
    public abstract void OnJump();
    public abstract void OnCrouch();

    protected abstract bool GroundCheck();
    protected abstract float HorizontalSpeedCalculate();
}