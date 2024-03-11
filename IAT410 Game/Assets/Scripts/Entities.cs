using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    private float groundedCheckDist = 0.1f;
    public float gravity = 6f;
    public Rigidbody rb;
    public LayerMask groundLayer;
    protected bool isGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();

        if (!isGrounded)
        {
            rb.velocity += Vector3.down * gravity * Time.deltaTime;
        }
    }

    protected bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDist, groundLayer))
        {
            return true;
        }
        return false;
    }
}
