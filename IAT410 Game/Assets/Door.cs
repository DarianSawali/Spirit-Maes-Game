using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Collider doorCollider; // Collider to unlock
    public int totalButtonCount; // Total number of buttons required to unlock the door
    private int buttonCount = 0; // Counter for pressed buttons
    private bool isLocked = true;

    public string sceneToLoad;

    public GameObject objectToSwitch;
    public Sprite newSprite;
    // public Animation doorAnimation;
    // public AnimationClip openAnimation;
    public Animator doorAnimator;
    // Function called when a button is pressed
    public CameraFollowVertical cameraScript;

    private ButtonTrigger button;

    private HealthManager health;

    protected void Start()
    {
        button = FindObjectOfType<ButtonTrigger>();

        health = FindObjectOfType<HealthManager>(); // to reset health every difficulty increase
    }

    public void ButtonPressed()
    {
        if (buttonCount < 1)
        {
            cameraScript.PanToGate();
        }
        buttonCount++;

        // Check if all buttons are pressed
        if (buttonCount >= totalButtonCount)
        {
            UnlockDoor();
            cameraScript.PanToGate();
            PlayOpenAnimation();
        }
    }

    public int getButtonPressedCount()
    {
        return buttonCount;
    }

    private void UnlockDoor()
    {
        isLocked = false;
        doorCollider.isTrigger = true;
    }

    private void PlayOpenAnimation()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Assuming "Open" is the trigger parameter in your Animator Controller
        }
        else
        {
            Debug.LogError("Door Animator not assigned!");
        }
    }

    // Function to unlock the door
    // private void UnlockDoor()s
    // {
    //     isLocked = false;
    //     doorCollider.isTrigger = true; 
    //     if (doorAnimation != null && openAnimation != null)
    //     {
    //         doorAnimation.clip = openAnimation;
    //         doorAnimation.Play();
    //     }
    //     else
    //     {
    //         Debug.LogError("Door Animation or Open Animation Clip not set.");
    //     }
    // }

    public void ResetDoor()
    {
        isLocked = true;
        doorCollider.isTrigger = false;
        buttonCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal") || other.CompareTag("Player") || other.CompareTag("Skunk") || other.CompareTag("Pigeon") || other.CompareTag("Fish"))
        {
            if (doorCollider.isTrigger)
            {
                Debug.Log("door test working");

                if ((SceneManager.GetActiveScene().buildIndex + 1) == 6 || (SceneManager.GetActiveScene().buildIndex + 1) == 10)
                {
                    health.ResetHealth();
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("door closed");
            }
        }
    }

    public void SwitchSprite()
    {
        if (objectToSwitch != null)
        {
            SpriteRenderer spriteRenderer = objectToSwitch.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite Renderer component not found on the object.");
            }
        }
        else
        {
            Debug.LogError("Object to switch is not assigned.");
        }
    }


}
