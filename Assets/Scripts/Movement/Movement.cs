using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MovementControllable
{
    [SerializeField] private UnityEvent OnMovementUpdate;
    [SerializeField] private Vector3Event OnLinearVelocityChanged;
    [SerializeField] private DashEvent OnDashParametrized;
    [SerializeField] private UnityEvent Sprint;
    [SerializeField] private UnityEvent SprintStop;
    [SerializeField] private UnityEvent<bool> IsSprintChanged;
    [SerializeField] private UnityEvent<float> AbsoluteSpeedChanged;
    [Space]

    [Header("Links")]
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private CoroutineQueue CoroutineQueue;
    private Rigidbody playerRb;

    [Header("Ground check")]
    [SerializeField] private float groundCheckRaycastLength;

    [Header("Jump")]
    [SerializeField] private float additionalImpulsFading;
    [SerializeField] private float additionalLinearVelocityFading;

    private bool sprintKey;

    private bool isSprint;
    public bool IsSprint
    {
        get => isSprint;
        set
        {
            if (isSprint != value)
            {
                if (value)
                    Sprint?.Invoke();
                else
                    SprintStop?.Invoke();

                IsSprintChanged?.Invoke(IsSprint);
            }

            isSprint = value;
        }
    }

    private float currentSprintTime;
    public float SprintTime
    {
        get => currentSprintTime; set
        {
            currentSprintTime = value;

            OnDashParametrized?.Invoke(new DashEventArgs
            {
                CurrentSprintTime = SprintTime,
                TimeToRefill = movementSettings.TimeToRefillSprint
            });
        }
    }

    private float absoluteSpeed;
    public float AbsoluteSpeed
    {
        get => absoluteSpeed; 
        private set
        {
            absoluteSpeed = value;
            AbsoluteSpeedChanged?.Invoke(absoluteSpeed);
        }
    }

    private float additionalHorizontalImpuls;
    private Vector3 additionalLinearVelocity;

    public float CurrentSpeed { get; protected set; }

    private Vector3 moveVector;
    private float currentLerpValue;

    private IEnumerator sprintCoroutine;
    private Vector3 prevPosition;

    public void ZeroMoveVector()
    {
        moveVector = Vector3.zero;
    }

    public void ZeroLinearVelocity()
    {
        playerRb.linearVelocity = Vector3.zero;
    }

    public void AddLinearVelocity(Vector3 velocity)
    {
        additionalLinearVelocity += velocity;
    }

    public void SetCurrentSpeed(float newSpeed)
    {
        if (newSpeed < 0) return;

        CurrentSpeed = newSpeed;
    }

    public void AddCurrentSpeed(float additionalSpeed)
    {
        if (CurrentSpeed + additionalSpeed < 0) return;

        CurrentSpeed += additionalSpeed;
    }

    public void SetAdditionalHorizonalImpulse(float newHorizonalImpluls) =>
        additionalHorizontalImpuls = newHorizonalImpluls;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SprintTime = movementSettings.SprintTime;
    }

    protected override void Update()
    {
        base.Update();

        AdditionalImpulseFading();
        AdditionalLinearVelocityFading();
        RefillSprint();
        CalculateAbsoluteSpeed();

        CurrentSpeed = movementSettings.DefaultSpeed;
        
        OnMovementUpdate.Invoke();
        OnLinearVelocityChanged.Invoke(playerRb.linearVelocity);
    }

    protected override bool GroundCheck() =>
        Physics.Raycast(transform.position, -transform.up, groundCheckRaycastLength);

    protected override float HorizontalSpeedCalculate() =>
        Mathf.Sqrt(Mathf.Pow(playerRb.linearVelocity.x, 2) + Mathf.Pow(playerRb.linearVelocity.z, 2));

    private void CalculateAbsoluteSpeed()
    {
        AbsoluteSpeed = Mathf.Abs((transform.position - prevPosition).magnitude / Time.deltaTime);

        prevPosition = transform.position;
    }

    private void AdditionalImpulseFading() =>
        additionalHorizontalImpuls = Mathf.Lerp(additionalHorizontalImpuls, 1, additionalImpulsFading * Time.deltaTime);

    private void AdditionalLinearVelocityFading() =>
        additionalLinearVelocity = Vector3.Lerp(additionalLinearVelocity, Vector3.zero, additionalLinearVelocityFading * Time.deltaTime);

    public void IsGroundedChanged(bool isGrounded)
    {
        if (isGrounded)
            currentLerpValue = movementSettings.GroundKeyboardLerpValue;
        else
            currentLerpValue = movementSettings.AirKeyboardLerpValue;
    }

    public override void OnJump()
    {
        if (!IsGrounded) return;

        SetJumpVelocity(movementSettings.JumpForce);
        CurrentDirection = new Vector3(CurrentDirection.x, movementSettings.JumpForce, CurrentDirection.z);

        additionalHorizontalImpuls = movementSettings.AdditionalJumpImpuls.x;
    }

    public void SetJumpVelocity(float jumpForce)
    {
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, jumpForce, playerRb.linearVelocity.z);
    }

    public override void OnMove(Vector2 inputs)
    {
        var transformedInput = transform.TransformDirection(new Vector3(inputs.x, 0, inputs.y).normalized) * CurrentSpeed;

        if (AbsoluteSpeed < movementSettings.SprintSpeedTreshold)
            IsSprint = false;

        else if (sprintKey)
            IsSprint = true;

        if (movementSettings.KeyboardLerpEnabled)
            moveVector = Vector3.Lerp(moveVector, transformedInput, Time.deltaTime * currentLerpValue);
        else
            moveVector = transformedInput;

        playerRb.linearVelocity = new Vector3(moveVector.x * additionalHorizontalImpuls, playerRb.linearVelocity.y, moveVector.z * additionalHorizontalImpuls) + additionalLinearVelocity;
        CurrentDirection = new Vector3(inputs.x, CurrentDirection.y, inputs.y);
    }

    public override void OnDash()
    {
        if (SprintTime < movementSettings.SprintTimeTreshold) return;
        if (sprintCoroutine != null) return;

        sprintKey = true;
        IsSprint = true;

        sprintCoroutine = SprintCoroutine(SprintTime);
        StartCoroutine(sprintCoroutine);
    }

    public override void OnDashStop()
    {
        sprintKey = false;

        if (sprintCoroutine == null) return;
        IsSprint = false;
            
        StopCoroutine(sprintCoroutine);
        sprintCoroutine = null;
    }

    private void RefillSprint()
    {
        if (IsSprint) return;
        if (SprintTime >= movementSettings.SprintTime) return;

        SprintTime += Time.deltaTime * (movementSettings.SprintTime / movementSettings.TimeToRefillSprint);
    }

    private IEnumerator SprintCoroutine(float sprintTime)
    {
        float currentTime = 0f;

        while (currentTime < sprintTime)
        {
            currentTime += Time.deltaTime;
            SprintTime -= Time.deltaTime;

            additionalHorizontalImpuls = movementSettings.DashSpeed;
            
            yield return null;
        }

        OnDashStop();
        sprintCoroutine = null;
    }

    public override void OnCrouch()
    {
        if (!IsGrounded)
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, -movementSettings.OnFlyCrouchVelocitySpeed, playerRb.linearVelocity.z);
    }
}