using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;

    void Update()
    {
        // Deactivate all pop-ups first
        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }

        // Then, only activate the current pop-up
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
        }

        if (popUpIndex == 0) // wasd movements
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                wPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                aPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                sPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressed = true;
            }

            // Check if all keys have been pressed
            if (wPressed && aPressed && sPressed && dPressed)
            {
                popUpIndex++; // Move to the next pop-up
            }
        }

        if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        
        if (popUpIndex == 2)
        {
            if (Input.GetKeyDown("space"))
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                popUpIndex++;
            }
        }
    }
}