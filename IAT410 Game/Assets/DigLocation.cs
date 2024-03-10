using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigLocation : MonoBehaviour
{
    public Transform teleportTarget;

    // public void OnDig()
    // {
    //     if (teleportTarget != null)
    //     {
    //         Debug.Log("alvin");
    //         PlayerController playerController = GetComponent<PlayerController>();
    //         if (playerController != null)
    //         {
    //             playerController.TeleportToDigLocation(teleportTarget.position);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("PlayerController not found in the scene.");
    //         }
    //     }

    //     else {
    //         Debug.LogWarning("Teleport target not set for this dig location.");
    //     }
    // }
}
