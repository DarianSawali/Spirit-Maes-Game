using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 0.9f;
    public Tilemap groundTilemap;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;
    public Transform spawnPoint;
    public PlayerInput playerInput;

    protected Rigidbody rb;

    protected bool isFishActive = false;

    public GameObject playerModel;
    private GameObject nearbyAnimal = null; // to turn on/off nearby animal collider
    private PlayerController player;

    // for animations
    private Animator animator;

    private bool beingPossessed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        DisableFishInput();

        player = FindObjectOfType<PlayerController>();
    }

    protected void Update()
    {
        // if fish fell, respawn at respawn position
        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }

    }

    protected void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    protected void OnFishMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        Vector3 horizontalMoveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        horizontalMoveDirection.Normalize();
        float verticalVelocity = rb.velocity.y;

        Vector3 movement = horizontalMoveDirection * moveSpeed + Vector3.up * verticalVelocity;
        rb.velocity = movement;

        if (horizontalMoveDirection.x != 0 || horizontalMoveDirection.z != 0)
        {
            animator.SetFloat("X", horizontalMoveDirection.x);
            animator.SetFloat("Y", horizontalMoveDirection.z);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void OnDispossess(InputValue value)
    {
        Debug.Log("OnDispossess called");
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindAction("FishMove").Disable();
        input.actions.FindAction("Dispossess").Disable();

        playerModel.SetActive(true);

        setFishPossessedFlagOff();

        player.DispossessAnimal();
        isFishActive = false;
    }

    public void EnableFishInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isFishActive = true;
        input.actions.FindAction("PlayerMove").Disable();
        input.actions.FindAction("FishMove").Enable();
        input.actions.FindAction("Dispossess").Enable();
    }

    public void DisableFishInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        isFishActive = false;
        input.actions.FindAction("PlayerMove").Enable();
        input.actions.FindAction("FishMove").Disable();
        input.actions.FindAction("Dispossess").Disable();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pigeon"))
        {
            nearbyAnimal = other.gameObject;
            Pigeon pigeonComponent = nearbyAnimal.GetComponent<Pigeon>();
            if (!pigeonComponent.getPigeonPossessedStatus()) // if in possession, dont turn off
            {
                nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        if (other.CompareTag("Skunk"))
        {
            nearbyAnimal = other.gameObject;
            Skunk skunkComponent = nearbyAnimal.GetComponent<Skunk>();
            if (!skunkComponent.getSkunkPossessedStatus()) // if in possession, dont turn off
            {
                nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        if (other.CompareTag("Ground"))
        {
            moveSpeed = 0.1f;
        }
        if (other.CompareTag("Water"))
        {
            moveSpeed = 0.9f;
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
        if (other.CompareTag("Skunk"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = true;
            nearbyAnimal = null;
        }
        if (other.CompareTag("Ground"))
        {
            moveSpeed = 0.9f;
        }
        if (other.CompareTag("Water"))
        {
            moveSpeed = 0.1f;
        }
    }

    public void OnTriggerStay(Collider other){
        if (other.CompareTag("Water")){
            moveSpeed = 0.9f;
        }
        if (other.CompareTag("Ground")){
            moveSpeed = 0.1f;
        }
    }

    // to set beingPossessed flag
    public void setFishPossessedFlagOn()
    {
        beingPossessed = true;
    }

    public void setFishPossessedFlagOff()
    {
        beingPossessed = false;
    }

    public bool getFishPossessedStatus()
    {
        if (beingPossessed)
        {
            return true;
        }
        return false;
    }
}
