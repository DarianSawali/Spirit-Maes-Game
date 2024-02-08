using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -5f);
    private Vector3 velocity = Vector3.zero;
    [SerializeField] [Range(0.01f, 1f)] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 rotatedOffset = Quaternion.Euler(45f, 0f, 0f) * offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition + rotatedOffset, ref velocity, smoothSpeed);

        transform.LookAt(target.position);
        transform.rotation = Quaternion.Euler(45f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}