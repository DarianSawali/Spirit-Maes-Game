using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Tilemap groundTilemap;
    public float stopFallingAtZ = -1f; 
    public float gravity = 9.81f; 

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable Unity's built-in gravity
    }

    private void Update()
    {
        if (!isGrounded && transform.position.z <= stopFallingAtZ)
        {
            // Apply gravity manually along the z-axis
            rb.velocity += Vector3.forward * gravity * Time.deltaTime;
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        Vector3 movement = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, 0f);
        rb.velocity = movement;
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Tilemap"))
    {
        // Player collided with tilemap
        isGrounded = true;
    }
}

private void OnCollisionStay(Collision collision)
{
    if (collision.gameObject.CompareTag("Tilemap"))
    {
        // Player is still colliding with tilemap
        isGrounded = true;
    }
}

private void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Tilemap"))
    {
        // Player is no longer colliding with tilemap
        isGrounded = false;
    }
}

    private void CheckGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to the groundTilemap
            if (collider.GetComponent<Tilemap>() == groundTilemap)
            {
                isGrounded = true;
                return;
            }
        }

        isGrounded = false;
    }



        // // Cast a ray downwards to check for collision with the ground layer
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        // {
        //     // Check if the hit object is part of the groundTilemap
        //     if (hit.collider.GetComponent<Tilemap>() == groundTilemap)
        //     {
        //         isGrounded = true;
        //         return;
        //     }
        // }

        // Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        // isGrounded = false;
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










