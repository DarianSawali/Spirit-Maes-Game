using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerup : MonoBehaviour
{

    public HealthManager health;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("powerup hit");
            health.addHealth();
            Destroy(gameObject);
        }
    }
}
