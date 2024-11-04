using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Variables")]
    [SerializeField] float Speed = 0f;
    [SerializeField] float walkSpeed = 7f;
    [SerializeField] float crouchSpeed = 3.5f;
    [SerializeField] float JumpHeight = 5f;
    [SerializeField] float gravity;
    [SerializeField] float normGravity = -9.81f * 2;
    [SerializeField] float floatGravity = -4f * 2;
    [SerializeField] float jumpGrace;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [Header("BoolCheckers")]
    public bool IsMoving = false;
    public bool IsCrouching = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        JumpAndGravity();
        CrouchToggled();
    }

    void Movement()
    {
        if (IsCrouching)
        {
            Speed = crouchSpeed;
        }
        else
        {
            Speed = walkSpeed;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = UnityEngine.Quaternion.Euler(0f, angle, 0f);
            UnityEngine.Vector3 moveDir = UnityEngine.Quaternion.Euler(0f, targetAngle, 0f) * UnityEngine.Vector3.forward;

            controller.Move(moveDir.normalized * Speed * Time.deltaTime);

        }
    }

    void JumpAndGravity()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && (Time.time - lastGroundedTime <= jumpGrace))
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);

            if (Time.time - jumpButtonPressedTime <= jumpGrace)
            {
                gravity = normGravity;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }

        }
        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            gravity = floatGravity;
        }
        if (Input.GetButtonUp("Jump") && !isGrounded)
        {
            gravity = normGravity;
        }
        if (isGrounded)
        {
            gravity = normGravity;
        }
        //checking if the jump key is being pressed again while falling to see if we can float 

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void CrouchToggled()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouching =  true;

            controller.height = 1f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouching = false;

            controller.height = 1.9f;
        }
    }
}

