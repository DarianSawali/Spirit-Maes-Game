using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalController : PlayerController
{
    public float moveSpeed = 5f; // Movement speed
    public float gravity = 9.81f;
    public float jumpForce = 0.01f;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;

    protected Rigidbody rb;
    protected bool isGrounded;
    protected Vector3 fixedEulerRotation = new Vector3(45f, 0f, 0f);
    protected const float dampingFactor = 0.9f;

    // private PlayerAnimalInput inputActions; // Assuming PlayerInputActions is the generated class for your input actions


    protected void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Ensure gravity is enabled
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


    }

    protected void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);

        if (!isGrounded)
        {
            isGrounded = CheckGrounded();
            rb.velocity += Vector3.down * gravity * Time.deltaTime;
        }

        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }

    }

    protected void EnableJump(){
        playerInput.actions["Jump"].Enable();
    }

    // protected void OnJump(InputValue value)
    // {
    //     if (!controlsEnabled || !isGrounded) return;
        
    //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    // }


    // private void FixedUpdate()
    // {
    //     // Check if the animal is grounded
    //     isGrounded = CheckGrounded();
    // }

    // private void HandleMovement()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //     Move(movement);
    // }

    // private void Move(Vector3 direction)
    // {
    //     // Normalize the direction vector to ensure consistent movement speed
    //     direction.Normalize();
    //     rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    // }

    // private bool CheckGrounded()
    // {
    //     RaycastHit hit;
    //     // Adjust the raycast distance to something reasonable
    //     bool hitDetected = Physics.Raycast(transform.position, Vector3.down, out hit, groundedCheckDist + 0.1f, groundLayer);
    //     return hitDetected;
    // }
}
