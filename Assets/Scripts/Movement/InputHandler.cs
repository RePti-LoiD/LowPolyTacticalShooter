using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public UnityEvent OnJump;
    [SerializeField] public UnityEvent OnDash;
    [SerializeField] public UnityEvent DashStop;
    [SerializeField] public UnityEvent Crouch;
    [SerializeField] public UnityEvent CrouchStop;

    [Header("Mouse")]
    [SerializeField] public Vector2Event OnMouseMove;
    [SerializeField] public Vector2Event OnMove;

    [Header("Gun")]
    [SerializeField] public UnityEvent OnShotStart;
    [SerializeField] public UnityEvent OnShotStop;

    [Space]
    [SerializeField] public UnityEvent OnReload;
    [SerializeField] public UnityEvent OnAdditionalAction;
    [SerializeField] public UnityEvent OnAdditionalActionStop;
    [SerializeField] public UnityEvent OnInspect;

    [Space]
    [SerializeField] public UnityEvent Drop;
    [SerializeField] public UnityEvent Interact;

    [Space]
    [SerializeField] public UnityEvent Modify;
    [SerializeField] public UnityEvent ModifyCanceled;

    [Space]
    [SerializeField] public UnityEvent LastSelect;
    [SerializeField] public UnityEvent<int> IndexSelected;

    private PlayerInputs inputs;

    private bool isInputEnabled;
    
    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.PlayerMap.Jump.performed += JumpHandler;
        inputs.PlayerMap.Dash.performed += DashHandler;
        inputs.PlayerMap.Dash.canceled += DashCanceledHandler;
        inputs.PlayerMap.Crouch.performed += OnCrouchHandler;
        inputs.PlayerMap.Crouch.canceled += OnCrouchStopHandler;

        inputs.PlayerMap.Shot.started += ShotStartHandler;
        inputs.PlayerMap.Shot.canceled += ShotStopHandler;

        inputs.PlayerMap.Reload.performed += ReloadHandler;
        inputs.PlayerMap.Inspect.performed += InspectHandler;

        inputs.PlayerMap.OnAdditionalAction.performed += AdditionalActionHandler;
        inputs.PlayerMap.OnAdditionalAction.canceled += AdditionalActionStopHandler;

        inputs.PlayerMap.PrevSelection.performed += OnPrevSelection;

        inputs.PlayerMap.WeaponSelection.performed += OnWeaponSelection;
        inputs.PlayerMap.Drop.performed += OnDrop;
        inputs.PlayerMap.Interact.performed += OnInteract;

        inputs.PlayerMap.ModifyKey.performed += OnModify;
        inputs.PlayerMap.ModifyKey.canceled += OnModifyCanceled;

        isInputEnabled = true;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.PlayerMap.Jump.performed -= JumpHandler;
        inputs.PlayerMap.Dash.performed -= DashHandler;
        inputs.PlayerMap.Dash.canceled -= DashCanceledHandler;
        inputs.PlayerMap.Crouch.performed -= OnCrouchHandler;
        inputs.PlayerMap.Crouch.canceled -= OnCrouchStopHandler;

        inputs.PlayerMap.Shot.started -= ShotStartHandler;
        inputs.PlayerMap.Shot.canceled -= ShotStopHandler;

        inputs.PlayerMap.Reload.performed -= ReloadHandler;
        inputs.PlayerMap.Inspect.performed -= InspectHandler;

        inputs.PlayerMap.OnAdditionalAction.performed -= AdditionalActionHandler;
        inputs.PlayerMap.OnAdditionalAction.canceled -= AdditionalActionStopHandler;


        inputs.PlayerMap.PrevSelection.performed -= OnPrevSelection;

        inputs.PlayerMap.WeaponSelection.performed -= OnWeaponSelection;
        inputs.PlayerMap.Drop.performed -= OnDrop;
        inputs.PlayerMap.Interact.performed -= OnInteract;
        isInputEnabled = false;
    }

    private void Update()
    {
        if (!isInputEnabled) return;

        HandleMovement();
        HandleCameraMovement();
    }

    #region Movement
    private void JumpHandler(InputAction.CallbackContext obj) =>
        OnJump.Invoke();
    
    private void DashHandler(InputAction.CallbackContext obj) =>
        OnDash.Invoke();

    private void DashCanceledHandler(InputAction.CallbackContext obj) =>
        DashStop?.Invoke();

    private void OnCrouchHandler(InputAction.CallbackContext obj) =>
        Crouch?.Invoke();

    private void OnCrouchStopHandler(InputAction.CallbackContext obj) =>
        CrouchStop?.Invoke();
    #endregion


    #region Mouse
    private void HandleMovement() =>
        OnMove.Invoke(inputs.PlayerMap.Move.ReadValue<Vector2>());

    private void HandleCameraMovement() =>
        OnMouseMove.Invoke(inputs.PlayerMap.MouseMove.ReadValue<Vector2>());
    #endregion


    #region Weapon
    private void ShotStartHandler(InputAction.CallbackContext context) =>
        OnShotStart?.Invoke();
    private void ShotStopHandler(InputAction.CallbackContext context) =>
        OnShotStop?.Invoke();

    private void ReloadHandler(InputAction.CallbackContext obj) =>
        OnReload?.Invoke();

    private void InspectHandler(InputAction.CallbackContext obj) =>
        OnInspect?.Invoke();

    private void AdditionalActionHandler(InputAction.CallbackContext obj) =>
        OnAdditionalAction?.Invoke();

    private void AdditionalActionStopHandler(InputAction.CallbackContext obj) =>
        OnAdditionalActionStop?.Invoke();

    private void OnWeaponSelection(InputAction.CallbackContext obj) =>
        IndexSelected?.Invoke(int.Parse(obj.control.name) - 1);

    private void OnPrevSelection(InputAction.CallbackContext obj) =>
        LastSelect?.Invoke();

    private void OnInteract(InputAction.CallbackContext obj) =>
        Interact?.Invoke();

    private void OnDrop(InputAction.CallbackContext obj) =>
        Drop?.Invoke();

    private void OnModify(InputAction.CallbackContext obj) =>
        Modify?.Invoke();

    private void OnModifyCanceled(InputAction.CallbackContext obj) =>
        ModifyCanceled?.Invoke();
    #endregion
}