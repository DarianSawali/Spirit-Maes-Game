using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlatform : MonoBehaviour
{
    private Collider platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is not of a specific tag
        if (!collision.collider.CompareTag("Fish"))
        {
            platformCollider.enabled = false;
            Invoke("EnableCollider", 1.0f); 
        }
    }

    private void EnableCollider()
    {
        platformCollider.enabled = true;
    }
}
