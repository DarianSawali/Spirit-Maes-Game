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

    private Animator animator; // for jumping animation
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

    private bool IsGrounded() // check if fish is grounded
    {
        float raycastDistance = 0.1f; // Adjust this distance based on your character's size
                                      // Define points for raycasting: center, left edge, right edge, top, and bottom
        Vector3 center = transform.position;
        Vector3 left = center - (transform.right * 0.5f); // Adjust based on character width
        Vector3 right = center + (transform.right * 0.5f); // Adjust based on character width
        Vector3 top = center + (transform.up * 0.5f); // Adjust based on character height
        Vector3 bottom = center - (transform.up * 0.5f); // Adjust based on character height

        // Combine all points in an array for easier iteration
        Vector3[] points = new Vector3[] { center, left, right, top, bottom };

        foreach (var point in points)
        {
            RaycastHit hit;
            // Cast a ray downwards from each point
            if (Physics.Raycast(point, Vector3.down, out hit, raycastDistance, groundLayer))
            {
                if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("DigTrigger") || hit.collider.CompareTag("Water"))
                {
                    return true; // Grounded if any ray hits a ground object
                }
            }
            // Additionally, for top and bottom points, cast rays in the character's forward direction
            // This is useful if your character moves in all four directions and you need to check for ground ahead or behind
            if (point == top || point == bottom)
            {
                if (Physics.Raycast(point, transform.forward, out hit, raycastDistance, groundLayer) ||
                    Physics.Raycast(point, -transform.forward, out hit, raycastDistance, groundLayer))
                {
                    if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("DigTrigger") || hit.collider.CompareTag("Water"))
                    {
                        return true; // Grounded if forward or backward ray hits a ground object
                    }
                }
            }
        }
        return false; // Not grounded if none of the rays hit a ground object
    }
}
