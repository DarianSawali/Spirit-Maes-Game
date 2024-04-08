using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraFollowVertical : MonoBehaviour
{
    public Transform target; // Assign the initial target, e.g., the player, here

    public float followSpeed = 2f;
    public float zOffset = 0.75f; // Set this to whatever Z-offset you want from the player

    public float orthoZoomSpeed = 1f; // Speed at which the camera zooms in and out
    public float targetOrthoSize = 1.5f; // Target orthographic size for the zoom level

    private Camera cam; // Reference to the Camera component

    private Vector3 initialPosition; // Variable to store the initial position of the camera

    public Transform gate; // Assign the gate's transform here
    public float panSpeed = 5f; // Speed for panning to gate
    public float panTime = .5f; // Time to keep the camera on the gate
    private bool isPanningToGate = false;

    private void Start()
    {
        cam = Camera.main; // Cache the main camera

        initialPosition = transform.position; // Save the initial position of the camera
    }

    private void LateUpdate()
    {
        if (!isPanningToGate)
        {
            // Follow player's Z-axis position
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }

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
            if (target.position.x < -2f)
            {
                // Move the camera to the new position when player's x position is less than -2
                transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
                Debug.Log("Move camera");
            }
            else if (target.position.x > -2f)
            {
                // Reset the camera to the initial position when player's x position is greater than -2
                transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
            }
        }
    }

    // This method is called when the player presses the button
    public void PanToGate()
    {
        StartCoroutine(PanToGateCoroutine());
    }

    private IEnumerator PanToGateCoroutine()
    {
        isPanningToGate = true;

        // Lerp towards the gate
        float startTime = Time.time;
        while (Time.time - startTime < panTime)
        {
            Vector3 gatePosition = new Vector3(gate.position.x, gate.position.y, initialPosition.z);
            transform.position = Vector3.Lerp(transform.position, gatePosition, panSpeed * Time.deltaTime);
            yield return null;
        }

        // Return to following the player after panning
        isPanningToGate = false;
    }
}
