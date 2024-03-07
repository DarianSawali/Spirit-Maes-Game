using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigTrigger : MonoBehaviour
{
    public Transform digLocation;
    private PlayerInput playerInput;
    private TilemapSwitch tilemapSwitch;
    
    private bool canDig = false;

    protected void start(){
        PlayerInput input = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        Debug.Log("Player has dug.");

        if (other.CompareTag("Animal") || other.CompareTag("Player"))
        {
            canDig = true;
            TilemapSwitch tilemapSwitch = GetComponent<TilemapSwitch>();
            tilemapSwitch.SwitchTilemaps();
            Debug.Log("Dig");
        }
    }

    public void OnDig()
    {
        if (canDig && digLocation != null)
        {
            TeleportToDigLocation(digLocation.position);
            Debug.Log("Dig");
        }
    }

    public void TeleportToDigLocation(Vector3 position)
    {
        transform.position = position;
    }
}

