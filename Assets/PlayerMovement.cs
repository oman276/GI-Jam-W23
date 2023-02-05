using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 25f;

    float moveLimiter = 2f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    void Start ()
    {
    rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);


        
    }

    void FixedUpdate() {
            
            if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                movement.x *= moveLimiter;
                movement.y *= moveLimiter;
            } 

            rb.velocity = movement;
    }
}