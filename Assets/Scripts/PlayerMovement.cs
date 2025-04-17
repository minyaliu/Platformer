using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 8f;
    private Vector2 movement = Vector2.zero;
    private float jumpingPower = 12f;
    private bool isJump;
    private bool jumpRelease;
    private bool isRight = true;

    private int numJumps;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        // if (value.isPressed && IsGrounded() && numJumps < 2)
        if (value.isPressed && numJumps < 1)

        {
            isJump = true;          // jumping if the jump button is pressed and the player is currently on the ground
            numJumps++;
        }
        else if (!value.isPressed)
        {
            jumpRelease = true;     // jump button is released
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move(movement.x, movement.y);
        horizontal = movement.x;
        Flip();
    }

    private void Move(float x, float z)
    {
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
    }

    private void FixedUpdate() 
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (IsGrounded())
        {
            numJumps = 0;
        }
        if (isJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);     // jump height becomes the x value and the jump power
            isJump = false;
        }
        if (jumpRelease && rb.linearVelocity.y > 0f)        // jump height if the button is released during the jump
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpRelease = false;
        }
    }

    private bool IsGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isRight && horizontal < 0f || !isRight && horizontal > 0f) 
        {
            isRight = !isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
