using UnityEngine;

public class PlayerHitReaction : MonoBehaviour
{
    public float knockbackForceX = 5f;

    public float knockbackForceY = 2.5f;

    public float stunTime = 0.2f;

    private Rigidbody2D rb;

    public bool IsHitStunned { get; private set; }

    private float stunTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
        float knockDir = Mathf.Sign(dir);

        if (knockDir == 0f)
            knockDir = 1f;

        IsHitStunned = true;
        stunTimer = stunTime;

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        rb.AddForce(new Vector2(knockDir * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
    }
}
