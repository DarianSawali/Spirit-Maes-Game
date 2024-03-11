using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Skunk : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Tilemap groundTilemap;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;
    public Transform spawnPoint;

    public PlayerInput playerInput;
    protected bool isSkunkActive = false;

    protected Rigidbody rb;
    protected bool isGrounded;

    protected bool controlsEnabled = true;

    public GameObject playerModel;

    private PlayerController player;


    protected void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // freeze rotations
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        DisableSkunkInput();

        player = FindObjectOfType<PlayerController>();
    }

    public void OnDispossess(InputValue value)
    {
        Debug.Log("OnDispossess called");
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindAction("SkunkMove").Disable();
        input.actions.FindAction("SkunkJump").Disable();
        input.actions.FindAction("Dispossess").Disable();


        playerModel.SetActive(true); // Show the player model again

        player.DispossessAnimal();
        isSkunkActive = false;
    }

    protected void Update()
    {
        // if skunk fell, respawn at respawn position

        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }

    }

    protected void OnSkunkMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        Vector3 horizontalMoveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        horizontalMoveDirection.Normalize();
        float verticalVelocity = rb.velocity.y;

        Vector3 movement = horizontalMoveDirection * moveSpeed + Vector3.up * verticalVelocity;
        rb.velocity = movement;
    }

    protected void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void EnableSkunkInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isSkunkActive = true;
        input.actions.FindAction("PlayerMove").Disable();
        input.actions.FindAction("SkunkMove").Enable();
        input.actions.FindAction("SkunkJump").Enable();
        input.actions.FindAction("Dispossess").Enable();
    }

    public void DisableSkunkInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isSkunkActive = false;
        input.actions.FindAction("PlayerMove").Enable();
        input.actions.FindAction("SkunkMove").Disable();
        input.actions.FindAction("SkunkJump").Disable();
        input.actions.FindAction("Dispossess").Disable();
    }

    protected bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
        {
            return true;
        }
        return false;
    }

    // to handle collisions so the animal will be 'invincible' and not
    // bounce off when the player bumps into it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pigeon") || other.CompareTag("Player") || other.CompareTag("Fish"))
        {
            Debug.Log("player is near");
            //testing for invincibility when player approach
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pigeon") || other.CompareTag("Fish"))
        {
            Debug.Log("player is not near");
            //testing for invincibility when player approach
        }
    }
}
