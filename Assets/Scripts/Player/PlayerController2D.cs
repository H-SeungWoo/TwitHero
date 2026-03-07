using System;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Move")] 
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")] 
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float x;
    private bool isGrounded;
    private PlayerHitReaction hitReaction;

    public int FacingDir { get; private set; } = 1; // 1:Right, -1: Left;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hitReaction = GetComponent<PlayerHitReaction>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        if (x > 0.01f)
        {
            FacingDir = 1;
            if (sr) sr.flipX = false;
        }
        else if (x < -0.01f)
        {
            FacingDir = -1;
            if (sr) sr.flipX = true;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (hitReaction != null && hitReaction.IsHitStunned)
        {
            return;
        }
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
