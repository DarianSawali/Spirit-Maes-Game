using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour // Assuming you handle possession elsewhere
{
    public float moveSpeed = 5f; // Movement speed
    public float gravity = 9.81f; 
    public LayerMask groundLayer;
    private float groundedCheckDist = 0.1f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 fixedEulerRotation = new Vector3(45f, 0f, 0f);
    private const float dampingFactor = 0.9f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Ensure gravity is enabled
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation); // Keep the skunk upright

        // Handle movement input
        HandleMovement();
    }

    private void FixedUpdate()
    {
        // Check if the skunk is grounded
        isGrounded = CheckGrounded();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Move(movement);
    }

    private void Move(Vector3 direction)
    {
        // Normalize the direction vector to ensure consistent movement speed
        direction.Normalize();
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        // Adjust the raycast distance to something reasonable
        bool hitDetected = Physics.Raycast(transform.position, Vector3.down, out hit, groundedCheckDist + 0.1f, groundLayer);
        return hitDetected;
    }
}
