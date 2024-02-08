using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Skunk : AnimalPossession
{
    public Tilemap groundTilemap;
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
        rb.useGravity = true; // Enable gravity
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);
    }

    private void FixedUpdate()
    {
        // Check if the skunk is grounded
        isGrounded = CheckGrounded();

        // Apply friction to stop gliding when not pushed
        if (isGrounded)
        {
            // Apply friction only if the skunk is not being pushed
            if (Mathf.Approximately(rb.velocity.magnitude, 0f))
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private bool CheckGrounded()
    {
        // Raycast down to check for collisions with the groundTilemap
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            return true;
        }
        return false;
    }


}