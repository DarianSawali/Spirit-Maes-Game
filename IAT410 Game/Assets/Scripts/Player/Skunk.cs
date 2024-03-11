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
    private GameObject nearbyAnimal = null; // to turn on/off nearby animal collider

    private PlayerController player;


    // for animations
    private Vector2 movement;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            animator.SetFloat("X", moveInput.x);
            animator.SetFloat("Z", moveInput.y);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pigeon"))
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
        if (other.CompareTag("Pigeon"))
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
