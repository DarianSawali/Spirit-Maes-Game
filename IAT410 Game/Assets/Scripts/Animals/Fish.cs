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

    public HealthManager health; // reduce health when falling

    // for colour changing
    Color originalColor;
    SpriteRenderer spriteRenderer;

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

        // sprite renderer colour
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected void Update()
    {
        // if fish fell, respawn at respawn position
        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
            if (beingPossessed)
            {
                health.decreaseHealth();
            }
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

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            moveSpeed = 0.9f;
        }
        if (other.CompareTag("Ground"))
        {
            moveSpeed = 0.1f;
        }
    }

    // to set beingPossessed flag
    public void setFishPossessedFlagOn()
    {
        beingPossessed = true;
        // Color possessedColor = HexToColor("#94DFFF");
        // spriteRenderer.color = possessedColor;

        animator.SetBool("isPossessed", true);
    }

    public void setFishPossessedFlagOff()
    {
        beingPossessed = false;
        // spriteRenderer.color = originalColor;

        animator.SetBool("isPossessed", false);
    }

    public bool getFishPossessedStatus()
    {
        if (beingPossessed)
        {
            return true;
        }
        return false;
    }

    // setup hexadecimal colour to rgb
    private static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", ""); // In case the string is formatted as 0xFFFFFF
        hex = hex.Replace("#", ""); // In case the string is formatted as #FFFFFF
        byte a = 255; // assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // Check if alpha is specified in hex
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
}
