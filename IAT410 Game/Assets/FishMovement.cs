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
