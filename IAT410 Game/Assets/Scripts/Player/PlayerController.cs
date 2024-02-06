using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 0.01f;
    public Tilemap groundTilemap;
    public float gravity = 9.81f; 
    public LayerMask groundLayer;
    private float groundedCheckDist = 0.1f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 fixedEulerRotation = new Vector3(45f, 0f, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);

        if (!isGrounded)
        {
            isGrounded = CheckGrounded();
            rb.velocity += Vector3.down * gravity * Time.deltaTime;
        }

        if(isGrounded){
            Debug.Log("ground");
        }
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGrounded();
    }

    public void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector3>();
        Vector3 movement = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, moveInput.z * moveSpeed);
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
            isGrounded = true;
            Debug.Log("enter");
            }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            isGrounded = true;
            Debug.Log("stay");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tilemap"))
        {
        // Player is no longer colliding with tilemap
            isGrounded = false;
            Debug.Log("exit");
        }
    }


    private bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
        {
            return true;
        }
        return false;
    }

}
