using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;

    public Vector2 LookInput { get; private set; } = Vector2.zero;

    public bool InvertMouseY { get; private set; } = true;

    public float ZoomCameraInput { get; private set; } = 0.0f;
    public bool InvertScroll { get; private set; } = true;

    public bool RunIsPressed { get; private set; } = false;

    public bool JumpIsPressed { get; private set; } = false;

    public bool CrouchIsPressed { get; private set; } = false;

    public bool InteractionWasPressedThisFrame { get; private set; } = false;

    public bool MenuOpenCloseWasPressedThisFrame { get; private set; } = false;

    public bool ChangeCameraWasPressedThisFrame { get; private set; } = false;

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _crouchAction;
    private InputAction _zoomCameraAction;
    private InputAction _changeCameraAction;
    private InputAction _lookAction;
    private InputAction _interactionAction;
    private InputAction _menuOpenCloseAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        
        SetUpInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetUpInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _runAction = _playerInput.actions["Run"];
        _crouchAction = _playerInput.actions["Crouch"];
        _zoomCameraAction = _playerInput.actions["ZoomCamera"];
        _changeCameraAction = _playerInput.actions["ChangeCamera"];
        _lookAction = _playerInput.actions["Look"];
        _interactionAction = _playerInput.actions["Interaction"];
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInputs()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero);
        JumpIsPressed = _jumpAction.IsPressed();
        RunIsPressed = _runAction.IsPressed();
        CrouchIsPressed = _crouchAction.IsPressed();
        ZoomCameraInput = _zoomCameraAction.ReadValue<float>();
        ChangeCameraWasPressedThisFrame = _changeCameraAction.WasPressedThisFrame();
        LookInput = _lookAction.ReadValue<Vector2>();
        InteractionWasPressedThisFrame = _interactionAction.WasPressedThisFrame();
        MenuOpenCloseWasPressedThisFrame = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
