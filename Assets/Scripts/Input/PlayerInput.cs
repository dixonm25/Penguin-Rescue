using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;

    public Vector2 LookInput { get; private set; } = Vector2.zero;

    public bool InvertMouseY { get; private set; } = true;

    public float ZoomCameraInput { get; private set; } = 0.0f;
    public bool InvertScroll {  get; private set; } = true;

    public bool RunIsPressed {  get; private set; } = false;

    public bool JumpIsPressed {  get; private set; } = false;

    public bool CrouchIsPressed { get; private set; } = false;

    public bool InteractionWasPressedThisFrame { get; private set; } = false;

    public bool MenuOpenCloseWasPressedThisFrame { get; private set; } = false;

    public bool ChangeCameraWasPressedThisFrame { get; private set; } = false;

    InputActions _input = null;

    private void OnEnable()
    {
        _input = new InputActions();
        _input.Movement.Enable();

        _input.Movement.Move.performed += SetMove;
        _input.Movement.Move.canceled += SetMove;

        _input.Movement.Look.performed += SetLook;
        _input.Movement.Look.canceled += SetLook;

        _input.Movement.ZoomCamera.started += SetZoomCamera;
        _input.Movement.ZoomCamera.canceled += SetZoomCamera;

        _input.Movement.Run.started += SetRun;
        _input.Movement.Run.canceled += SetRun;

        _input.Movement.Jump.started += SetJump;
        _input.Movement.Jump.canceled += SetJump;

        _input.Movement.Crouch.started += SetCrouch;
        _input.Movement.Crouch.canceled += SetCrouch;
    }

    private void OnDisable()
    {
        _input.Movement.Move.performed -= SetMove;
        _input.Movement.Move.canceled -= SetMove;

        _input.Movement.Look.performed -= SetLook;
        _input.Movement.Look.canceled -= SetLook;

        _input.Movement.ZoomCamera.started -= SetZoomCamera;
        _input.Movement.ZoomCamera.canceled -= SetZoomCamera;

        _input.Movement.Run.started -= SetRun;
        _input.Movement.Run.canceled -= SetRun;

        _input.Movement.Jump.started -= SetJump;
        _input.Movement.Jump.canceled -= SetJump;

        _input.Movement.Crouch.started -= SetCrouch;
        _input.Movement.Crouch.canceled -= SetCrouch;

        _input.Movement.Disable();
    }

    private void Update()
    {
        ChangeCameraWasPressedThisFrame = _input.Movement.ChangeCamera.WasPressedThisFrame();
        MenuOpenCloseWasPressedThisFrame = _input.Movement.MenuOpenClose.WasPressedThisFrame();
        InteractionWasPressedThisFrame = _input.Movement.Interaction.WasPressedThisFrame();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetRun(InputAction.CallbackContext ctx)
    {
        RunIsPressed = ctx.started;
    }

    private void SetJump(InputAction.CallbackContext ctx)
    {
        JumpIsPressed = ctx.started;
    }

    private void SetCrouch(InputAction.CallbackContext ctx)
    {
        CrouchIsPressed = ctx.started;
    }

    private void SetZoomCamera(InputAction.CallbackContext ctx)
    {
        ZoomCameraInput = ctx.ReadValue<float>();
    }
}
