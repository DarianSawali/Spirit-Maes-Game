using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkunkJump : MonoBehaviour
{
    public float jumpForce = 1.7f;
    public float gravity = 5f;
    public float gravityScale = 0.2f;
    public PlayerInput playerInput;
    public Rigidbody rb;
    public LayerMask groundLayer;
    protected bool isGrounded;

    private Animator animator; // For jumping animation

    public Transform platform; // Assign the reference to the platform in the inspector

    protected bool isDamaged = false; // handle damage animation
    private float damageDuration = 1f; // damage animation duration

    public AudioManager audioManager;
    public AudioClip jump;

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
            // rb.AddForce(Vector3.down * gravityScale * gravity, ForceMode.Acceleration);
            rb.AddForce(Vector3.down * gravityScale * gravity * Time.deltaTime, ForceMode.Acceleration);
            // Debug.Log("Land");
        }

        if (Input.GetKey(KeyCode.Space)) OnSkunkJump();
    }

    public void OnSkunkJump()
    {
        if (isGrounded)
        {
            // rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioManager.PlaySoundEffect(jump);
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }

    private bool IsGrounded()
    {
        float raycastDistance = 0.1f;

        Vector3 center = transform.position;
        Vector3 left = center - (transform.right * 0.1f);
        Vector3 right = center + (transform.right * 0.1f);
        Vector3 top = center + (transform.up * 0.1f);
        Vector3 bottom = center - (transform.up * 0.1f);

        // Combine all points in an array for easier iteration
        Vector3[] points = new Vector3[] { center, left, right, top, bottom };

        foreach (var point in points)
        {
            RaycastHit hit;
            // Cast a ray downwards from each point
            if (Physics.Raycast(point, Vector3.down, out hit, raycastDistance, groundLayer))
            {
                if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("DigTrigger"))
                {
                    return true; // Grounded if any ray hits a ground object
                }
            }


            if (point == top || point == bottom)
            {
                if (Physics.Raycast(point, transform.forward, out hit, raycastDistance, groundLayer) ||
                    Physics.Raycast(point, -transform.forward, out hit, raycastDistance, groundLayer))
                {
                    if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("DigTrigger"))
                    {
                        return true; // Grounded if forward or backward ray hits a ground object
                    }
                }
            }
        }
        return false; // Not grounded if none of the rays hit a ground object
    }

}
