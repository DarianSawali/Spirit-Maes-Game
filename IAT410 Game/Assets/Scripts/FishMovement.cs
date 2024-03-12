using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishMovement : MonoBehaviour
{
    public float gravity = 5f;
    public float gravityScale = 0.2f;
    public PlayerInput playerInput;
    public Rigidbody rb;
    public LayerMask groundLayer;
    protected bool isGrounded;

    public Transform platform; // Assign the reference to the platform in the inspector

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PlayerController playerControl = GetComponent<PlayerController>();
    }

    protected void Update()
    {
        isGrounded = IsGrounded();

        // Update the animator with whether or not the pigeon is grounded.
        animator.SetBool("isGrounded", isGrounded);

        // Check if the pigeon is below the platform to trigger the falling animation
        if (!isGrounded && transform.position.y < platform.position.y)
        {
            // Pigeon is falling below the platform
            animator.SetBool("isFalling", true);
        }
        else
        {
            // Pigeon is not falling below the platform
            animator.SetBool("isFalling", false);
        }

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    private bool IsGrounded()
    {
        float raycastDistance = 0.1f;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Water"))
            {
                return true;
            }
        }
        return false;
    }
}
