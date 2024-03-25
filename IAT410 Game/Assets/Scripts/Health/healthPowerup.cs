using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerup : MonoBehaviour
{

    public HealthManager health;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("health up");
            health.addHealth();
        }

        if (other.CompareTag("Pigeon"))
        {
            Pigeon pigeonComponent = other.GetComponent<Pigeon>();
            if (!pigeonComponent.getPigeonPossessedStatus()) // if in possession, health up
            {
                health.addHealth();
            }
        }

        if (other.CompareTag("Skunk"))
        {
            Skunk skunkComponent = other.GetComponent<Skunk>();
            if (!skunkComponent.getSkunkPossessedStatus()) // if in possession, health up
            {
                health.addHealth();
            }
        }

        if (other.CompareTag("Fish"))
        {
            Fish fishComponent = other.GetComponent<Fish>();
            if (!fishComponent.getFishPossessedStatus()) // if in possession, health up
            {
                health.addHealth();
            }
        }

        Destroy(gameObject); // remove powerup
    }

}
