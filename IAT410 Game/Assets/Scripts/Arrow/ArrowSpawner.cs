using System.Collections;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab; // Assign the arrow prefab

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public float spawnRate = 2f; // Time between spawns
    private float timeSinceSpawned = 0f;

    public float arrowSpeed = 10f; // Speed of the spawned arrows

    private Camera cam; // Reference to the main camera

    private void Start()
    {
        cam = Camera.main; // Get the main camera
        // StartCoroutine(SpawnArrows()); // Start spawning arrows
    }

    void Update()
    {
        timeSinceSpawned += timeSinceSpawned.deltaTime
        
        if (timeSinceSpawned >= spawnTime)
        {
            Instantiate(arrowPrefab, spawnLocation.position, spawnRotation);
        }
    }

    // private IEnumerator SpawnArrows()
    // {
    //     while (true) // Infinite loop to keep spawning arrows
    //     {
    //         yield return new WaitForSeconds(spawnRate); // Wait for the specified spawn rate

    //         // Calculate spawn position (right edge of the screen, random Y)
    //         Vector3 spawnPosition = cam.ViewportToWorldPoint(new Vector3(1, Random.Range(0.1f, 0.9f), cam.nearClipPlane));

    //         // Instantiate the arrow
    //         GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);

    //         // Adjust the arrow's speed (you might need to adjust the direction based on your setup)
    //         Arrow arrowScript = arrow.GetComponent<Arrow>();
    //         if (arrowScript != null)
    //         {
    //             arrowScript.speed = arrowSpeed; // Set the arrow's speed
    //         }

    //         // Optionally, you might want to flip the arrow's direction if you spawn it from the left side
    //         // arrow.transform.localScale = new Vector3(-1, 1, 1); // Flip the arrow if needed
    //     }
    // }
}
