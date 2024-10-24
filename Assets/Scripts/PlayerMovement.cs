using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Variables")]
    [SerializeField] float Speed;
    [SerializeField] float WalkSpeed = 13f;
    [SerializeField] float JumpHeight = 3f;
    [SerializeField] float gravity;
    public float normGravity = -9.81f * 2;
    public float floatGravity = -2f * 2;
    public float rotationSpeed;

    [Header("BoolCheckers")]
    public bool IsMoving = false;


    private Rigidbody RB;
    private CapsuleCollider PlayersCapsule;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        JumpAndGravity(); 
        Movement();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayersCapsule = GetComponent<CapsuleCollider>();
        RB = GetComponent<Rigidbody>();
    }


    void Movement()
    {
        IsMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||  Input.GetKeyDown(KeyCode.Space));

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



        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * Speed;
        movementDirection.Normalize();

        controller.Move(movementDirection * magnitude * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
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

        //check if the player is on the ground so he can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            gravity = normGravity;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded )
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

