using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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
    private Animator animator;

    private bool beingPossessed = false;

    private Transform teleportTarget;

    private bool canDig = false;
    public bool hasDug = false;

    public HealthManager health; // reduce health when falling

    // for colour changing
    Color originalColor;
    SpriteRenderer spriteRenderer;

    protected bool isDamaged = false; // handle damage animation
    private float damageDuration = 1f; // damage animation duration

    public AudioManager audioManager;
    public AudioClip digSound;

    private bool digPressed;

    // public DigTrigger digTrigger;


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

        setSkunkPossessedFlagOff();

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

        // make skunk dance when in reward room 1
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            animator.SetBool("isDancing", true);
        }
        else
        {
            animator.SetBool("isDancing", false);
        }

        digPressed |= Input.GetKeyDown(KeyCode.F);
    }

    protected void OnSkunkMove()
    {
        Vector2 moveInput = new((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? 1 : 0),
                                (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? 1 : 0));
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

        if (digPressed) OnDig();
        digPressed = false;
    }

    protected void FixedUpdate()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (beingPossessed) OnSkunkMove();
    }

    public void OnDig()
    {
        if (canDig && teleportTarget != null) // Check if the skunk can dig and has already dug
        {
            audioManager.PlaySoundEffect(digSound);
            TeleportToDigLocation(teleportTarget.position);
            Debug.Log("Teleporting after dig");

            // digTrigger.TriggerAnimation();
        }
    }

    // private void TriggerAnimatedTile()
    // {
    //     if (animatedTilemap != null)
    //     {
    //         Vector3Int digPosition = animatedTilemap.WorldToCell(transform.position);

    //         animatedTilemap.SetTileFlags(digPosition, TileFlags.None);
    //         animatedTilemap.SetTile(digPosition, null); 

    //     }
    // }

    // public void ResumeAnimationAtPosition(Vector3Int position)
    // {
    //     TileBase tile = animatedTilemap.GetTile(position);
    //     if (tile != null && tile is AnimatedTile)
    //     {
    //         AnimatedTile animatedTile = (AnimatedTile)tile;
            
    //         if (animatedTile.flags == TileFlags.AnimationPaused)
    //         {
    //             animatedTile.flags = TileFlags.LockTransform | TileFlags.LockColor | TileFlags.Animation;
                
    //             animatedTilemap.RefreshTile(position);
    //         }
    //     }
    // }

    // public void ResumeAnimation()
    // {
    //     Vector3Int position = new Vector3Int(0, 0, 0);

    //     ResumeAnimationAtPosition(position);
    // }

    // public void OnDig()
    // {
    //     if (canDig && teleportTarget != null && hasDug)
    //     {
    //         TeleportToDigLocation(teleportTarget.position);
    //         Debug.Log("Dig");
    //     }
    // }

    public void TeleportToDigLocation(Vector3 position)
    {
        transform.position = position;
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
            Pigeon pigeonComponent = nearbyAnimal.GetComponent<Pigeon>();
            if (!pigeonComponent.getPigeonPossessedStatus())
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

        if (other.CompareTag("DigTrigger"))
        {
            canDig = true;
            teleportTarget = other.GetComponent<DigTrigger>().teleportLocation;
            Debug.Log("canDig");

        }

        if (other.CompareTag("Player")) // indicate if skunk can be possessed
        {
            Color possessionColor = HexToColor("#94DFFF");
            spriteRenderer.color = possessionColor;
        }

        if (getSkunkPossessedStatus() && (other.CompareTag("ArrowLeft") || other.CompareTag("ArrowRight")
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
        if (other.CompareTag("Fish"))
        {
            nearbyAnimal = other.gameObject;
            nearbyAnimal.GetComponent<CapsuleCollider>().enabled = true;
            nearbyAnimal = null;
        }
        DigTrigger digTrigger = other.GetComponent<DigTrigger>();
        if (other.CompareTag("DigTrigger"))
        {
            canDig = false;
            teleportTarget = null;
        }

        if (other.CompareTag("Player")) // return skunk to original colour
        {
            spriteRenderer.color = originalColor;
        }
    }

    // to set beingPossessed flag
    public void setSkunkPossessedFlagOn()
    {
        beingPossessed = true;

        animator.SetBool("isPossessed", true);
    }

    public void setSkunkPossessedFlagOff()
    {
        beingPossessed = false;

        animator.SetBool("isPossessed", false);
    }

    public bool getSkunkPossessedStatus()
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
