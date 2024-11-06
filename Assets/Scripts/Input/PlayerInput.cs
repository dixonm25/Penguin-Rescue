using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;

    public Vector2 LookInput { get; private set; } = Vector2.zero;

    public bool InvertMouseY { get; private set; } = true;

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
    }

    private void DisEnable()
    {
        _input.Movement.Move.performed -= SetMove;
        _input.Movement.Move.canceled -= SetMove;

        _input.Movement.Look.performed -= SetLook;
        _input.Movement.Look.canceled -= SetLook;

        _input.Movement.Disable();
    }

    private void Update()
    {
        ChangeCameraWasPressedThisFrame = _input.Movement.ChangeCamera.WasPressedThisFrame();
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
}
