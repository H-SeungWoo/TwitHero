using System;
using UnityEngine;

public class EnemyHitReaction : MonoBehaviour
{
    public float knockbackForceX = 4f;
    public float knockbackForceY = 2f;

    public float knockbackDuration = 0.12f;
    public float hitStunTime = 2f;

    private Rigidbody2D rb;

    public bool IsHitStunned { get; private set; }
    private float stunTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsHitStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                IsHitStunned = false;
            }
        }
    }

    public void knockback(Vector2 hitFromPosition)
    {
        if (rb == null) return;

        float dir = transform.position.x - hitFromPosition.x;
        float knockDir = Math.Sign(dir);

        if (knockDir == 0f) knockDir = 1f;

        IsHitStunned = true;
        stunTimer = hitStunTime;

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        rb.AddForce(new Vector2(knockDir * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
    }

}
