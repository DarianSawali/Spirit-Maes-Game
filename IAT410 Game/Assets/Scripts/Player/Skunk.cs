using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Skunk : MonoBehaviour
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



    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     rb.useGravity = false;
    //     rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    //     rb.isKinematic = false;
    // }

    // // Update is called once per frame
    // private void Update()
    // {
    //     transform.rotation = Quaternion.Euler(fixedEulerRotation);

    //     if (!isGrounded)
    //     {
    //         isGrounded = CheckGrounded();
    //         rb.velocity += Vector3.down * gravity * Time.deltaTime;
    //     }

    //     if (isGrounded)
    //     {
    //         Debug.Log("Grounded");
    //         ApplyFriction();
    //     }
    // }

    // private bool CheckGrounded()
    // {
    //     if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // // Apply friction to gradually slow down the skunk's movement
    // private void ApplyFriction()
    // {
    //     // Reduce velocity using damping factor
    //     rb.velocity *= dampingFactor;
    // }

    // // Apply force to move the skunk
    // public void ApplyForce(Vector3 force)
    // {
    //     rb.AddForce(force, ForceMode.Impulse);
    // }





}