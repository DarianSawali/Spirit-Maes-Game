using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPossession : MonoBehaviour
{
    public GameObject player;
    public GameObject possessionPrompt;
    public float possessionRadius = 0.2f;

    private GameObject nearbyAnimal;
    private bool isPossessingAnimal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            nearbyAnimal = other.gameObject;
            possessionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            nearbyAnimal = null;
            possessionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyAnimal != null && !isPossessingAnimal)
        {
            // Check if the player is within the possession radius of the animal
            float distance = Vector3.Distance(player.transform.position, nearbyAnimal.transform.position);
            if (distance <= possessionRadius)
            {
                PossessAnimal();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPossessingAnimal)
        {
            DispossessAnimal();
        }
    }

    private void PossessAnimal()
    {
        isPossessingAnimal = true;
        possessionPrompt.SetActive(false);
        // Disable player control
        player.SetActive(false);
        // Enable control for the possessed animal
        // You'll need to implement this logic based on your setup
    }

    private void DispossessAnimal()
    {
        isPossessingAnimal = false;
        // Enable player control
        player.SetActive(true);
        // Disable control for the possessed animal
        // Return camera control to the player
        // You'll need to implement this logic based on your setup
    }

    // private void CheckPressurePlate()
    // {
    //     if (isPossessingAnimal && nearbyPressurePlate != null)
    //     {
    //         // Trigger the pressure plate
    //         // You'll need to implement this logic based on your pressure plate setup
    //     }
    // }
}
