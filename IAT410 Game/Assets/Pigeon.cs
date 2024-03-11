using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Pigeon : MonoBehaviour
{
    public float moveSpeed = 0.9f;
    public Tilemap groundTilemap;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;
    public Transform spawnPoint;

    public PlayerInput playerInput;

    protected Rigidbody rb;
    protected bool isGrounded;

    protected bool controlsEnabled = true;

    public GameObject playerModel; // Assign your player model in the inspector
    private GameObject nearbyAnimal = null; // to turn on/off nearby animal collider

    private PlayerController player;


    protected void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // freeze rotations
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        DisablePigeonInput();

        player = FindObjectOfType<PlayerController>();
    }

    public void OnDispossess(InputValue value)
    {
        Debug.Log("OnDispossess called");
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindAction("PigeonMove").Disable();
        input.actions.FindAction("PigeonJump").Disable();
        input.actions.FindAction("Dispossess").Disable();


        playerModel.SetActive(true); // Show the player model again

        player.DispossessAnimal();

    }

    protected void Update()
    {
        // if pigeon fell, respawn at respawn position
        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }
    }

    protected void OnPigeonMove(InputValue value)
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

    public void EnablePigeonInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        input.actions.FindAction("PlayerMove").Disable();
        input.actions.FindAction("PigeonMove").Enable();
        input.actions.FindAction("PigeonJump").Enable();
        input.actions.FindAction("Dispossess").Enable();
    }

    public void DisablePigeonInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        input.actions.FindAction("PlayerMove").Enable();
        input.actions.FindAction("PigeonMove").Disable();
        input.actions.FindAction("PigeonJump").Disable();
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
        if (other.CompareTag("Skunk"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
        }
        if (other.CompareTag("Fish"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Skunk"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = true;
            nearbyAnimal = null;

        }
        if (other.CompareTag("Fish"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = true;
            nearbyAnimal = null;
        }
    }
}

