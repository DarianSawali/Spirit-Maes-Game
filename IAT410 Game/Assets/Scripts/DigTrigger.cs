using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigTrigger : MonoBehaviour
{
    public DigLocation digLocation;
    private bool canDig = false;
    private bool dug = false; 

    public Transform teleportLocation;
    
    protected void start(){
        PlayerInput input = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
    
}

