using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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

    public GameObject playerModel; // Assign your player model in the inspector
    private GameObject nearbyAnimal = null; // to turn on/off nearby animal collider

    private PlayerController player;

    // for animations
    private Animator animator;

    // to determine collision on/off
    private bool beingPossessed = false;

    public HealthManager health; // reduce health when falling

    protected bool isDamaged = false; // handle damage animation
    private float damageDuration = 1f; // damage animation duration

    // for colour changing
    Color originalColor;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        // freeze rotations
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        player = FindObjectOfType<PlayerController>();

        // sprite renderer colour
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnDispossess()
    {
        Debug.Log("OnDispossess called");

        setPigeonPossessedFlagOff();

        playerModel.SetActive(true); // Show the player model again

        player.DispossessAnimal();

    }

    protected void Update()
    {
        // if pigeon fell, respawn at respawn position
        if (transform.position.y < -2f)
        {
            transform.position = spawnPoint.position;
            if (beingPossessed)
            {
                health.decreaseHealth();
                // Access the singleton instance of HealthManager and call decreaseHealth
                // HealthManager.instance.decreaseHealth();
                isDamaged = true;
                animator.SetBool("Damaged", true);
            }
        }
        if (isDamaged) // to handle damage animation duration
        {
            damageDuration -= Time.deltaTime;
            if (damageDuration <= 0)
            {
                animator.SetBool("Damaged", false);
                isDamaged = false;
                damageDuration = 1f;
            }
        }

        // make pigeon dance when in reward room 1
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            animator.SetBool("isDancing", true);
        }
        else
        {
            animator.SetBool("isDancing", false);
        }
    }

    protected void OnPigeonMove()
    {
        Vector2 moveInput = new((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? 1 : 0),
                                (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)    ? 1 : 0) - (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? 1 : 0));
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
        
        if (Input.GetKey(KeyCode.Q)) OnDispossess();
    }

    protected void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (beingPossessed) OnPigeonMove();
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
            Skunk skunkComponent = nearbyAnimal.GetComponent<Skunk>();
            if (!skunkComponent.getSkunkPossessedStatus()) // if in possession, dont turn off
            {
                nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        if (other.CompareTag("Fish"))
        {
            nearbyAnimal = other.gameObject;
            Fish fishComponent = nearbyAnimal.GetComponent<Fish>();
            if (!fishComponent.getFishPossessedStatus())
            {
                nearbyAnimal.GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        if (getPigeonPossessedStatus() && (other.CompareTag("ArrowLeft") || other.CompareTag("ArrowRight")
        || other.CompareTag("ArrowUp") || other.CompareTag("ArrowDown"))) // if being possessed and hit by arrow, decrease health
        {
            health.decreaseHealth();
            // Access the singleton instance of HealthManager and call decreaseHealth
            // HealthManager.instance.decreaseHealth();
            isDamaged = true;
            animator.SetBool("Damaged", true);
        }

        if (other.CompareTag("Player")) // indicate if pigeon can be possessed
        {
            Color possessionColor = HexToColor("#94DFFF");
            spriteRenderer.color = possessionColor;
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

        if (other.CompareTag("Player")) // return pigeon to original colour
        {
            spriteRenderer.color = originalColor;
        }

        
    }

    // to set beingPossessed flag
    public void setPigeonPossessedFlagOn()
    {
        beingPossessed = true;
        // Color possessedColor = HexToColor("#94DFFF");
        // spriteRenderer.color = possessedColor;

        animator.SetBool("isPossessed", true);
    }

    public void setPigeonPossessedFlagOff()
    {
        beingPossessed = false;
        // spriteRenderer.color = originalColor;

        animator.SetBool("isPossessed", false);
    }

    public bool getPigeonPossessedStatus()
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

