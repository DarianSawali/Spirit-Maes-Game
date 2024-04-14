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

    protected bool isDamaged = false; // handle damage animation
    private float damageDuration = 1f; // damage animation duration

    // for colour changing
    Color originalColor;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

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
    }

    protected void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (beingPossessed) OnFishMove();
        else rb.velocity = Vector3.zero;
    }

    protected void OnFishMove()
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

    public void OnDispossess()
    {
        Debug.Log("OnDispossess called");

        playerModel.SetActive(true);

        setFishPossessedFlagOff();

        player.DispossessAnimal();
        isFishActive = false;
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

        if (other.CompareTag("Player")) // indicate if fish can be possessed
        {
            Color possessionColor = HexToColor("#94DFFF");
            spriteRenderer.color = possessionColor;
        }

        if (getFishPossessedStatus() && (other.CompareTag("ArrowLeft") || other.CompareTag("ArrowRight")
        || other.CompareTag("ArrowUp") || other.CompareTag("ArrowDown"))) // if being possessed and hit by arrow, decrease health
        {
            health.decreaseHealth();
            isDamaged = true;
            animator.SetBool("Damaged", true);
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

        if (other.CompareTag("Player")) // return fish to original colour
        {
            spriteRenderer.color = originalColor;
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

        animator.SetBool("isPossessed", true);
    }

    public void setFishPossessedFlagOff()
    {
        beingPossessed = false;

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
