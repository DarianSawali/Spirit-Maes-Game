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

    // Function called when a button is pressed
    public void ButtonPressed()
    {
        buttonCount++;

        // Check if all buttons are pressed
        if (buttonCount >= totalButtonCount)
        {
            UnlockDoor();
            SwitchSprite();
        }
    }

    // Function to unlock the door
    private void UnlockDoor()
    {
        isLocked = false;
        doorCollider.isTrigger = true; 
    }

    public void ResetDoor()
    {
        isLocked = true;
        // pressedButtonCount = 0;
        doorCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Animal") || other.CompareTag("Player") || other.CompareTag("Skunk") || other.CompareTag("Pigeon") || other.CompareTag("Fish"))
        {
            if(doorCollider.isTrigger){
                Debug.Log("door test working");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else {
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
