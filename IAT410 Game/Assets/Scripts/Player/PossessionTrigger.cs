using UnityEngine;

public class PossessionTrigger : MonoBehaviour
{
    private GameObject potentialHost; // The animal that can potentially be possessed
    private PossessionSystem possessionSystem;

    private void Start()
    {
        // Find and store a reference to the PossessionSystem script on the player
        possessionSystem = FindObjectOfType<PossessionSystem>(); // Adjust this if your architecture differs
    }

    private void Update()
    {
        // Check if 'E' is pressed and there is a potential host nearby
        if (Input.GetKeyDown(KeyCode.E) && potentialHost != null)
        {
            possessionSystem.Possess(potentialHost);
            potentialHost = null; // Clear the potential host after possession
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            // Store a reference to the animal GameObject
            potentialHost = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal") && other.gameObject == potentialHost)
        {
            // Clear the potential host if it leaves the trigger area
            potentialHost = null;
        }
    }
}
