using UnityEngine;

public class CameraRooms : MonoBehaviour
{
    public Transform player; // Assign your player transform here.
    public float followSpeed = 2f;
    public float yOffset = 0f; // Adjust if you want the camera to be higher or lower relative to the player.
    public float minZ, maxZ; // Set these to the lower and upper z bounds of your room.

    // Variables for zoom control
    public float orthoZoomSpeed = 1f; // Speed at which the camera zooms in and out.
    public float targetOrthoSize = 5f; // Target orthographic size for the zoom level.
    private Camera cam; // Reference to the Camera component.

    private void Start()
    {
        cam = GetComponent<Camera>(); // Cache the Camera component.
    }

    private void Update()
    {
        // Vertical follow based on player's z-axis position within room bounds.
        float targetZPos = Mathf.Clamp(player.position.z, minZ, maxZ);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, targetZPos + yOffset);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        // Zoom control based on the player's actions or other events.
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrthoSize, orthoZoomSpeed * Time.deltaTime);
        }
    }

    // Method to adjust the camera's zoom level.
    public void AdjustZoom(float newTargetSize)
    {
        targetOrthoSize = newTargetSize;
    }
}
