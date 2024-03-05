using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigTrigger : MonoBehaviour
{
    public Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TeleportToDigLocation();
        }
    }
}
