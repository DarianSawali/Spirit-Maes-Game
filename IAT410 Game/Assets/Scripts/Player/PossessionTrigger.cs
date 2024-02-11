using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PossessionTrigger : MonoBehaviour
{
    public GameObject playerModel; // Assign your player model in the inspector
    private GameObject targetAnimal = null;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E) && targetAnimal != null)
        // {
        //     Debug.Log("e pressed down");
        //     PossessAnimal(targetAnimal);
        // }

        if (targetAnimal != null)
        {
            PossessAnimal(targetAnimal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal")) // Make sure your animals have the "Animal" tag
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

    void PossessAnimal(GameObject animal)
    {
        Debug.Log("Possessing animal");

        // Optionally, disable the player's movement and control scripts
        GetComponent<PlayerController>().enabled = false; // Assuming you have a PlayerController script

        playerModel.SetActive(false); // Hide the player model

        // Enable the animal control script
        var animalControl = animal.GetComponent<AnimalController>();
        if (animalControl != null)
        {
            animalControl.enabled = true;
        }
        else
        {
            Debug.LogError("AnimalControl script not found on the target animal");
        }

        // Transfer 'camera focus' or any other player-centric components to the animal
        // This might involve setting the camera's target to the animal or enabling animal-specific UI elements
    }
}
