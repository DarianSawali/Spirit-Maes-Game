using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool isPlayerActive = true;
    private bool isSkunkActive = false;
    private bool isPidgeonActive = false;
    private bool isFishActive = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void EnablePlayerInput()
    {
        isPlayerActive = true;
        playerInput.actions.FindAction("PlayerMove").Enable();
        playerInput.actions.FindAction("SkunkMove").Disable();
        playerInput.actions.FindAction("SkunkJump").Disable();
    }

    public void DisablePlayerInput()
    {
        isPlayerActive = false;
        playerInput.actions.FindAction("PlayerMove").Disable();
        playerInput.actions.FindAction("SkunkMove").Enable();
        playerInput.actions.FindAction("SkunkJump").Enable();
    }

    public void EnableSkunkInput()
    {
        isPlayerActive = false; // Assume skunk is active
        playerInput.actions.FindAction("PlayerMove").Disable();
        playerInput.actions.FindAction("SkunkMove").Enable();
        playerInput.actions.FindAction("SkunkJump").Enable();
        playerInput.actions.FindAction("Dispossess").Enable();
    }

    public void DisableSkunkInput()
    {
        isPlayerActive = true; // Assume player is active
        playerInput.actions.FindAction("SkunkMove").Disable();
        playerInput.actions.FindAction("SkunkJump").Disable();
        playerInput.actions.FindAction("Dispossess").Disable();
    }

    public void EnablePidgeonInput(){

    }

    public void DisablePidgeonInput(){

    }

    public void EnableFishInput(){

    }

    public void DisableFishInput(){

    }
    
}