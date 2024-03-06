using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    //public float jumpForce = 1.4f;
    public Tilemap groundTilemap;
    //public float gravity = 6f;
    public LayerMask groundLayer;
    protected float groundedCheckDist = 0.1f;
    public Transform spawnPoint;

    private float speed = 6;
    private float grav = -9.81f;

    private PlayerInput playerInput;
    private Skunk skunk;
    protected bool isPlayerActive = true;

    protected Rigidbody rb;
    protected bool isGrounded;
    //protected Vector3 fixedEulerRotation = new Vector3(45f, 0f, 0f);

    //protected bool controlsEnabled = true; // enable/disable player movement

    public GameObject playerModel; // Assign your player model in the inspector
    private GameObject targetAnimal = null;

    public PlayerJump playerJump;
    private Transform teleportTarget;

    private bool canDig = false;
    //public GameObject[] teleportTargets;
    // public CamFollow cameraFollowScript;

    protected void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        input.actions.FindAction("SkunkJump").Disable();
        //playerInput.Disable();

        skunk = FindObjectOfType<Skunk>();

        if (isPlayerActive)
        {
            // Disable SkunkMove action, enable PlayerMove action, enable possession
            input.actions.FindAction("PlayerMove").Enable();
            input.actions.FindAction("SkunkMove").Disable();
            input.actions.FindAction("Possess").Enable();
        }
    }

    protected void OnPlayerMove(InputValue value)
    {
        // Get the movement input vector
        Vector2 moveInput = value.Get<Vector2>();

        // Calculate the horizontal movement direction based on the input vector
        Vector3 horizontalMoveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        // Normalize the horizontal movement direction to maintain consistent speed regardless of input magnitude
        horizontalMoveDirection.Normalize();

        // Preserve the current vertical velocity to maintain gravity's effect
        float verticalVelocity = rb.velocity.y;

        // Combine the horizontal movement with the existing vertical velocity
        Vector3 movement = horizontalMoveDirection * moveSpeed + Vector3.up * verticalVelocity;

        // Apply the movement to the player's velocity
        rb.velocity = movement;
    }

    protected void DisableJump()
    {
        playerInput.actions["SkunkJump"].Disable();
    }

    public void EnablePlayerInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindAction("PlayerMove").Enable();
        input.actions.FindAction("SkunkMove").Disable();
        input.actions.FindAction("SkunkJump").Disable();

        input.actions.FindAction("Possess").Enable();
        input.actions.FindAction("Dispossess").Disable();
    }

    public void DisablePlayerInput()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindAction("PlayerMove").Disable();
        input.actions.FindAction("SkunkMove").Enable();

        input.actions.FindAction("Possess").Disable();
        input.actions.FindAction("Dispossess").Enable();

    }

    // public void EnablePlayerPossession()
    // {
    //     PlayerInput input = GetComponent<PlayerInput>();
    //     input.actions.FindAction("Possess").Enable();
    //     // input.actions.FindAction("Dispossess").Disable();
    // }

    // public void DisablePlayerPossession()
    // {
    //     PlayerInput input = GetComponent<PlayerInput>();
    //     input.actions.FindAction("Possess").Disable();
    //     input.actions.FindAction("Dispossess").Enable();
    // }

    protected void Update()
    {
        //transform.rotation = Quaternion.Euler(fixedEulerRotation);

        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
        }

        if (isPlayerActive)
        {
            EnablePlayerInput();
        }

        if(!isPlayerActive)
        {
            DisablePlayerInput();
        }

        // if (!isPossessing)
        // {
        //     EnablePlayerInput();
        //     EnablePlayerPossession();
        // }

        // if (isPossessing)
        // {
        //     DisablePlayerPossession();
        //     DisablePlayerInput();
        // }
    }

    //possessing mechanic
    protected void OnPossess(InputValue value)
    {
        Debug.Log("OnPossess called");
        if (targetAnimal != null && isPlayerActive)
        {
            PossessAnimal(targetAnimal);
            // cameraFollowScript.SetTarget(skunk.transform); // Make the camera follow the skunk
            // CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
            // cameraManager.SetCameraTarget(skunk.transform);

        }
    }

    public void OnDispossess(InputValue value)
    {
        Debug.Log("OnDispossess called");
        DispossessAnimal();
        // if (isPossessing)
        // {
        //     DispossessAnimal();
        //     isPossessing = false;
        // }
    }

    public void PossessAnimal(GameObject animal)
    {
        Debug.Log("Possessing animal");

        isPlayerActive = false;

        playerModel.SetActive(false);
        skunk.EnableSkunkInput();

        // cameraFollowScript.SetTarget(targetAnimal.transform); // Make the camera follow the skunk
    }

    public void DispossessAnimal()
    {
        Debug.Log("Dispossessing animal");

        playerModel.SetActive(true); // Show the player model again
        EnablePlayerInput();

        targetAnimal = null; // Clear the target animal

        isPlayerActive = true;

        // cameraFollowScript.SetTarget(player.transform); // Make the camera follow the player again

    }


    public void OnDig()
    {
        if (canDig && teleportTarget != null)
        {
            TeleportToDigLocation(teleportTarget.position);
            Debug.Log("Dig");
        }
    }

    public void TeleportToDigLocation(Vector3 position)
    {
        transform.position = position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            targetAnimal = other.gameObject;
            Debug.Log("trigger called");
        }

        if (other.CompareTag("DigTrigger"))
        {
            canDig = true;
            teleportTarget = other.GetComponent<DigTrigger>().teleportLocation;
            Debug.Log("canDig");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal") && other.gameObject == targetAnimal)
        {
            targetAnimal = null;
        }

        DigTrigger digTrigger = other.GetComponent<DigTrigger>();
        if (other.CompareTag("DigTrigger"))
        {
            canDig = false;
            teleportTarget = null;
        }

    }

    


    // private bool IsGrounded()
    // {
    //     float raycastDistance = 0.1f; // Adjust this distance based on your player's size
    //     RaycastHit hit;

    //     if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
    //     {
    //         if (hit.collider.CompareTag("Ground"))
    //         {
    //             // The player is considered grounded
    //             return true;
    //         }
    //     }
    //     return false;
    // }



    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //         Debug.Log("collision enter");
    //     }
    // }

    // private void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }


    // protected void OnPossess(InputValue value)
    // {
    //     Debug.Log("OnPossess called");
    //     if (targetAnimal != null && !isPossessing)
    //     {
    //         PossessAnimal(targetAnimal);
    //         isPlayerActive = false;
    //         isPossessing = true;
    //         // cameraFollowScript.SetTarget(skunk.transform); // Make the camera follow the skunk
    //         // CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
    //         // cameraManager.SetCameraTarget(skunk.transform);

    //     }
    // }

    // public void OnDispossess(InputValue value)
    // {
    //     Debug.Log("OnDispossess called");
    //     if (isPossessing)
    //     {
    //         DispossessAnimal();
    //         isPossessing = false;
    //         isPlayerActive = true;
    //     }
    // }

    // public void PossessAnimal(GameObject animal)
    // {
    //     Debug.Log("Possessing animal");

    //     playerModel.SetActive(false);
    //     DisablePlayerInput();
    //     skunk.EnableSkunkInput();

    //     // cameraFollowScript.SetTarget(targetAnimal.transform); // Make the camera follow the skunk

    // }


    // protected void FixedUpdate()
    // {
    //     isGrounded = CheckGrounded();
    // }
}


// protected void FixedUpdate()
// {
//     isGrounded = CheckGrounded();
// }

// protected void EnableControls()
// {
//     controlsEnabled = true;
// }

// protected void DisableControls()
// {
//     controlsEnabled = false;
// }



// public bool CheckGrounded()
// {
//     if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
//     {
//         return true;
//     }
//     return false;
// }



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

// public bool CheckGrounded()
// {
//     // RaycastHit hit;
//     // Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f; // Offset slightly above player's position
//     // if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, groundedCheckDist, groundLayer))
//     // {
//     //     Debug.Log("Hit ground: " + hit.collider.gameObject.name);
//     //     return true;
//     // }
//     return false;
// }

// public bool CheckGrounded()
// {

//     return rb.velocity.y == 0;
//     // RaycastHit hit;

//     // if (Physics.Raycast(transform.position, Vector3.down, out hit, groundedCheckDist, groundLayer))
//     // {
//     //     if (hit.collider.CompareTag("Ground"))
//     //     {
//     //         return true;
//     //     }
//     // }

//     // return false;
// }


//rb.velocity += Vector3.down * gravity * Time.deltaTime;

// if (!isGrounded)
// {
//     isGrounded = CheckGrounded();
//     Debug.Log("not ground");
//     //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
// }