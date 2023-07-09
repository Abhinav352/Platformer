using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.5f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
   public bool isGrounded;
    private bool canDoubleJump;
    private float horizontalMovement;
    private int jumpsRemaining;
    private bool isKnockback;
    private bool isDashing;
    private bool facingRight = true;
    private bool isDashCooldown;

    private void Start()
    {
        Application.targetFrameRate = 144;
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = 2; // Initialize jumps remaining to 2
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        horizontalMovement = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            jumpsRemaining = 2; // Reset jumps remaining when grounded
            canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (jumpsRemaining > 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }
        }

        if (Input.GetButtonDown("Dash") && !isDashCooldown)
        {
            StartCoroutine(Dash());
        }

        // Flip character based on movement direction
        if (horizontalMovement > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FixedUpdate()
    {
        if (!isKnockback && !isDashing)
        {
            Vector2 targetVelocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply knockback effect
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            knockbackDirection.Normalize();
            StartCoroutine(Knockback(knockbackDirection));
        }
    }

    private IEnumerator Knockback(Vector2 knockbackDirection)
    {
        isKnockback = true;

        // Apply knockback force
        rb.velocity = knockbackDirection * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        isDashCooldown = true;

        Vector2 dashDirection = facingRight ? Vector2.right : Vector2.left;
        rb.velocity = dashDirection * dashDistance / dashDuration;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
        isDashCooldown = false;
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}