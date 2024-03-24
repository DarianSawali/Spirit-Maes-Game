using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkunkJump : MonoBehaviour
{
    public float jumpForce = 1.4f;
    public float gravity = 5f;
    public float gravityScale = 0.2f;
    public PlayerInput playerInput;
    public Rigidbody rb;
    public LayerMask groundLayer;
    protected bool isGrounded;

    private Animator animator; // For jumping animation

    public Transform platform; // Assign the reference to the platform in the inspector

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    protected void Update()
    {
        isGrounded = IsGrounded();

        // Update the animator with whether or not the skunk is grounded.
        animator.SetBool("isGrounded", isGrounded);

        // Trigger the falling animation if not grounded and below the platform
        animator.SetBool("isFalling", !isGrounded && transform.position.y < platform.position.y);

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityScale * gravity, ForceMode.Acceleration);
        }
    }

    public void OnSkunkJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }

    private bool IsGrounded()
    {
        float raycastDistance = 0.1f; // Adjust this distance based on your player's size
        Vector3[] points = new Vector3[]
        {
            transform.position, // Center
            transform.position - (transform.right * 0.5f), // Left
            transform.position + (transform.right * 0.5f)  // Right
        };

        foreach (var point in points)
        {
            RaycastHit hit;
            if (Physics.Raycast(point, Vector3.down, out hit, raycastDistance, groundLayer))
            {
                if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("DigTrigger"))
                {
                    return true; // Grounded if any ray hits the ground or dig trigger
                }
            }
        }
        return false; // Not grounded if none of the rays hit the ground
    }
}
