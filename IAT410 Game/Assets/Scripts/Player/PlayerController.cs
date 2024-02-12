using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.9f;
    public float jumpForce = 1.4f;
    public Tilemap groundTilemap;
    public float gravity = 6f;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;
    public Transform spawnPoint;

    public PlayerInput playerInput;
    private Skunk skunk;
    protected bool isPlayerActive = true;
    protected bool isSkunkActive = false;

    protected Rigidbody rb;
    protected bool isGrounded;
    protected Vector3 fixedEulerRotation = new Vector3(45f, 0f, 0f);

    //protected bool controlsEnabled = true; // enable/disable player movement

    protected void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        input.actions.FindAction("Jump").Disable();
        //playerInput.Disable();

        if(isPlayerActive){
            // Disable SkunkMove action and enable PlayerMove action
            input.actions.FindAction("PlayerMove").Enable();
            input.actions.FindAction("SkunkMove").Disable();
            
        }
    }

    protected void DisableJump(){
        playerInput.actions["Jump"].Disable();
    }

    public void EnablePlayerInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isPlayerActive = true;
        input.actions.FindAction("PlayerMove").Enable();
        input.actions.FindAction("SkunkMove").Disable();
    }

    public void DisablePlayerInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isPlayerActive = false;
        input.actions.FindAction("PlayerMove").Disable();
    }

    protected void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);

        if (!isGrounded)
        {
            isGrounded = CheckGrounded();
            rb.velocity += Vector3.down * gravity * Time.deltaTime;
        }

        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }

    }

    protected void FixedUpdate()
    {
        isGrounded = CheckGrounded();
    }

    // protected void EnableControls()
    // {
    //     controlsEnabled = true;
    // }

    // protected void DisableControls()
    // {
    //     controlsEnabled = false;
    // }

    protected void OnPlayerMove(InputValue value)
    {
        //if (!controlsEnabled) return;

        Vector3 moveInput = value.Get<Vector3>();
        Vector3 movement = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, moveInput.z * moveSpeed);
        rb.velocity = movement;
    }

    protected bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
        {
            return true;
        }
        return false;
    }
}




    // public void OnJump(InputValue value)
    // {
    //     if (!controlsEnabled || !isGrounded) return;
        
    //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    // }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Tilemap"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Tilemap"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Tilemap"))
    //     {
    //         // Player is no longer colliding with tilemap
    //         isGrounded = false;
    //     }
    // }
