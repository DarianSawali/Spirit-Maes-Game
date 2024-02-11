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
        if (Input.GetKeyDown(KeyCode.E) && targetAnimal != null)
        {
            PossessAnimal(targetAnimal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal")) // Make sure your animals have the "Animal" tag
        {
            targetAnimal = other.gameObject;
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
        playerModel.SetActive(false); // Hide the player model
        animal.GetComponent<AnimalController>().enabled = true; // Enable the animal control script
        this.enabled = false; // Optionally disable this script to prevent re-possession while already possessing an animal
    }
}
