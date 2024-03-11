using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 1.4f;
    public float gravity = 5f;
    public float gravityScale = 0.2f;
    public PlayerInput playerInput;
    public Rigidbody rb;
    public LayerMask groundLayer;
    protected bool isGrounded;

    private PlayerController playerControl;
    private Skunk skunk;
    
    protected void Start(){
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        PlayerController playerControl = GetComponent<PlayerController>();
        Skunk skunk = GetComponent<Skunk>();
    }

    protected void Update()
    {
        isGrounded = IsGrounded();
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    public void OnJump()
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
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                // The player is considered grounded
                return true;
            }
        }
        return false;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Water"))
    //     {
    //         rb.enabled = false;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Water"))
    //     {
    //         rb.enabled = true;
    //     }
    // }

    public void FallThroughWater()
    {
        int waterLayer = LayerMask.NameToLayer("Water");
        rb.gameObject.layer = waterLayer;
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    
}
