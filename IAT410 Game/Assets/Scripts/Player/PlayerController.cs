using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer; // Set in the Inspector
    public float stopFallingAtZ = -1f; // Z-coordinate to stop falling
    public float gravity = 9.81f; // Adjust gravity as needed

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Disable Rigidbody2D's built-in gravity
    }

    private void Update()
    {
        if (!isGrounded && transform.position.z >= stopFallingAtZ)
        {
            // Apply gravity manually along the positive z-axis
            transform.position += Vector3.forward * gravity * Time.deltaTime;
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        Vector2 movement = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        rb.velocity = movement;
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGrounded();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGrounded();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        // Check if the player's z-position is less than or equal to stopFallingAtZ
        isGrounded = transform.position.z <= stopFallingAtZ;
    }



    // public float moveSpeed = 5f;
    // public float jumpForce = 10f;
    // public LayerMask groundLayer = 0;
    // public float gravity = 9.81f; // Adjust gravity as needed

    // private Rigidbody2D rb;
    // private bool isGrounded;

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     rb.gravityScale = 0f; // Disable Rigidbody2D's built-in gravity
    // }

    // private void Update()
    // {
    //     if (!isGrounded)
    //     {
    //         // Apply gravity manually along the z-axis
    //         transform.position += Vector3.back * gravity * Time.deltaTime;
    //     }
    // }

    // public void OnMove(InputValue value)
    // {
    //     Vector2 moveInput = value.Get<Vector2>();
    //     Vector2 movement = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    //     rb.velocity = movement;
    // }

    // public void OnJump(InputValue value)
    // {
    //     if (isGrounded)
    //     {
    //         rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     CheckGrounded();
    // }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     CheckGrounded();
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     CheckGrounded();
    // }

    // private void CheckGrounded()
    // {
    //     isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
    //     if (isGrounded)
    //     {
    //         rb.gravityScale = 0f; // Disable gravity when grounded
    //     }
    //     else
    //     {
    //         rb.gravityScale = 1f; // Enable gravity when not grounded
    //     }
    // }







}


