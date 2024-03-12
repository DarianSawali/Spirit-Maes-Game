using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerup : MonoBehaviour
{

    public HealthManager health;

    // Start is called before the first frame update
    private void onTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            health.addHealth();
            Destroy(gameObject);
        }
    }
}
