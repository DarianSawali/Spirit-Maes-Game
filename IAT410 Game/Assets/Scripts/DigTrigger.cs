using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigTrigger : MonoBehaviour
{
    public DigLocation digLocation;
    private bool canDig = false;

    public Transform teleportLocation;
    
    protected void start(){
        PlayerInput input = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // PlayerController player = other.GetComponent<PlayerController>();
        // Debug.Log("Player has dug.");

        if (other.CompareTag("Animal") || other.CompareTag("Player"))
        {
            canDig = true;
            // TilemapSwitch tilemapSwitch = GetComponent<TilemapSwitch>();
            // tilemapSwitch.SwitchTilemaps();
            //Debug.Log("Dig");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canDig = false;
        }
    }

    // private void OnDigPerformed(InputAction.CallbackContext context)
    // {
    //     if (canDig)
    //     {
    //         digLocation.OnDig();
    //     }
    // }

    // public void OnDig()
    // {
    //     if (canDig && digLocation != null)
    //     {
    //         TeleportToDigLocation(digLocation.position);
    //         Debug.Log("Dig");
    //     }
    // }

    // public void TeleportToDigLocation(Vector3 position)
    // {
    //     transform.position = position;
    // }

    // private void OnEnable()
    // {
    //     // Enable the Dig action
    //     PlayerInputController.Instance.PlayerControls.Player.Dig.performed += OnDigPerformed;
    // }

    // private void OnDisable()
    // {
    //     // Disable the Dig action
    //     PlayerInputController.Instance.PlayerControls.Player.Dig.performed -= OnDigPerformed;
    // }



    
    // private void Update()
    // {
    //     if (canDig && Input.GetKeyDown(KeyCode.F)) // Assuming 'F' is your dig input
    //     {
    //         digLocation.OnDig();
    //     }
    // }
    
}

