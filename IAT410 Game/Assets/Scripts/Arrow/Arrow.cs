using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 0.2f;

    public HealthManager health;

    public float timeToLive = 1f;
    private float timeSinceSpawned = 0f;

    protected Vector3 fixedEulerRotation = new Vector3(90f, 0f, 0f);

    // Direction of the arrow's movement
    private Vector3 direction;

    void Start()
    {
        transform.rotation = Quaternion.Euler(fixedEulerRotation);

        // Check the tag to set the direction
        if (gameObject.tag == "ArrowRight")
        {
            direction = Vector3.right;
        }
        else if (gameObject.tag == "ArrowLeft")
        {
            direction = Vector3.left;
        }
    }

    void Update()
    {
        // transform.rotation = Quaternion.Euler(fixedEulerRotation);

        transform.Translate(direction * speed * Time.deltaTime);

        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > timeToLive)
        {
            Destroy(gameObject);
        }

        // // Check if the arrow is off-screen
        // Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        // bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // if (!onScreen)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle player hit
            // You can check here if the player is in soul form or possessing an animal
            // PlayerController playerController = other.GetComponent<PlayerController>();
            // if (playerController != null)
            // {
            //     health.decreaseHealth();
            // }
            // health.decreaseHealth();

            Destroy(gameObject); // Destroy the arrow after hitting the player
        }

        if (other.CompareTag("Pigeon"))
        {
            Pigeon pigeonComponent = other.GetComponent<Pigeon>();
            if (pigeonComponent.getPigeonPossessedStatus()) // if in possession, health up
            {
                Destroy(gameObject); // remove powerup
            }
        }

        if (other.CompareTag("Skunk"))
        {
            Skunk skunkComponent = other.GetComponent<Skunk>();
            if (skunkComponent.getSkunkPossessedStatus()) // if in possession, health up
            {
                Destroy(gameObject); // remove powerup
            }
        }

        if (other.CompareTag("Fish"))
        {
            Fish fishComponent = other.GetComponent<Fish>();
            if (fishComponent.getFishPossessedStatus()) // if in possession, health up
            {
                Destroy(gameObject); // remove powerup
            }
        }
    }
}