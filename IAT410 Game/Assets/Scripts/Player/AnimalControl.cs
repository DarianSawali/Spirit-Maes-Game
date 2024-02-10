using UnityEngine;

public class AnimalControl : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Add movement logic here. Example for direct control:
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}
