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
            Destroy(gameObject);
        }
    }
}
