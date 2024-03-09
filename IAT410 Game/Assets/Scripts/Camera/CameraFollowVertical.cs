using UnityEngine;

public class CameraFollowVertical : MonoBehaviour
{
    public Transform player; // Assign your player transform here
    public float followSpeed = 2f;
    public float zOffset = -10f; // Set this to whatever Z-offset you want from the player

    private void LateUpdate()
    {
        // Assuming your camera is looking down the Y-axis (typical in 2D games),
        // and you want to follow the player's Z-axis position.
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + zOffset);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
