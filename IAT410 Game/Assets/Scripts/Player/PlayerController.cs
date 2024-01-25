using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.velocity = moveDirection.normalized * moveSpeed;

        // Add jumping functionality
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {

        return true;
    }
}










































// public class PlayerController : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public float jumpForce = 10f;

//     private Rigidbody2D rb;
//     private Vector2 moveDirection;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     private void FixedUpdate(){
//         rb.velocity = moveDirection.normalized * moveSpeed;

//         if(Input.GetButtonDown)
//     }

//     private void Update(){
//         moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//     }

//     // void OnMove(InputValue movementValue){
//     //     movementInput = movementValue.Get<Vector2>();
//     // }
// }
