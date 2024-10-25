using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Variables")]
    public float Speed;
    public float WalkSpeed = 13f;
    public float JumpHeight = 3f;
    public float gravity;
    public float normGravity = -9.81f * 2;
    public float floatGravity = -2f * 2;
    public float jumpGrace;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [Header("BoolCheckers")]
    public bool IsMoving = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Update is called once per frame
    void Update()
    {
        Movement();
        JumpAndGravity();
    }

    void Movement()
    {
        IsMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.Space));

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }

        if (IsMoving)
        {
            Speed = WalkSpeed;
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonPressedTime = Time.time;
        }

        //check if the player is on the ground so he can jump
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time - lastGroundedTime <= jumpGrace))
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
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            gravity = floatGravity;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
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
}