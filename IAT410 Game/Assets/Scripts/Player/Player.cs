using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
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
    protected Vector3 fixedEulerRotation = new Vector3(0f, 0f, 0f);

    //protected bool controlsEnabled = true; // enable/disable player movement

    public GameObject playerModel; // Assign your player model in the inspector
    private GameObject targetAnimal = null;
    private bool isPossessing = false;

    // public CamFollow cameraFollowScript;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //playerInput.Disable();

        skunk = FindObjectOfType<Skunk>();
    }

    protected void Update()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);

        if (!isGrounded)
        {
            isGrounded = CheckGrounded();
            rb.velocity += Vector3.forward * gravity * Time.deltaTime;
        }

        if (transform.position.z < -2f)
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

    protected bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
        {
            return true;
        }
        return false;
    }

    protected void OnPossess(InputValue value)
    {
        Debug.Log("OnPossess called");
        if (targetAnimal != null && !isPossessing)
        {
            PossessAnimal(targetAnimal);
            isPlayerActive = false;
            isPossessing = true;
            // cameraFollowScript.SetTarget(skunk.transform); // Make the camera follow the skunk
            // CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
            // cameraManager.SetCameraTarget(skunk.transform);

        }
    }

    public void OnDispossess(InputValue value)
    {
        Debug.Log("OnDispossess called");
        if (isPossessing)
        {
            DispossessAnimal();
            isPossessing = false;
            isPlayerActive = true;
        }
    }

    public void PossessAnimal(GameObject animal)
    {
        Debug.Log("Possessing animal");

        playerModel.SetActive(false);

        // cameraFollowScript.SetTarget(targetAnimal.transform); // Make the camera follow the skunk

    }

    public void DispossessAnimal()
    {
        Debug.Log("Dispossessing animal");

        playerModel.SetActive(true); // Show the player model again

        targetAnimal = null; // Clear the target animal

    }

    // to check which animal player is colliding with
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            targetAnimal = other.gameObject;
            Debug.Log("trigger called");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal") && other.gameObject == targetAnimal)
        {
            targetAnimal = null;
        }
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
