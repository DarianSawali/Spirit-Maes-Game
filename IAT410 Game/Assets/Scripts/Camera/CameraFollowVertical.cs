using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowVertical : MonoBehaviour
{
    public Transform target; // Assign the initial target, e.g., the player, here
    public float followSpeed = 2f;
    public float zOffset = 0.75f; // Set this to whatever Z-offset you want from the player

    public float orthoZoomSpeed = 1f; // Speed at which the camera zooms in and out
    public float targetOrthoSize = 1.5f; // Target orthographic size for the zoom level

    private Camera cam; // Reference to the Camera component

    private void Start()
    {
        cam = Camera.main; // Cache the main camera
    }

    private void LateUpdate()
    {
        // Follow player's Z-axis position
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + zOffset);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        // Adjust camera orthographic size to zoom in or out
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrthoSize, orthoZoomSpeed * Time.deltaTime);
        }
    }

    // Method to adjust the camera's current target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Call this method to adjust zoom level dynamically (e.g., when certain events occur)
    public void AdjustZoom(float newTargetSize)
    {
        targetOrthoSize = newTargetSize;
    }

    public void AdjustXAxis()
    {
        // Create a new Vector3 with the desired x value and current y and z values
        Vector3 newPosition = new Vector3(-3.5f, transform.position.y, transform.position.z);
        transform.position = newPosition; // Assign the new position back to the camera
    }

    public void RevertXAxis()
    {
        // Create a new Vector3 with the desired x value and current y and z values
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition; // Assign the new position back to the camera
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Debug.Log("Level 4.5");
            if (target.position.x < -2f)
            {
                AdjustXAxis(); // Adjust the camera's x position
            }
        }
    }

}
