using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    public int damage = 2;
    public float lifeTime = 2.0f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);

    }

    public void Init(int dir, float speed)
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(dir * speed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hp = other.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(damage);

            EnemyHitReaction reaction = other.GetComponent<EnemyHitReaction>();
            EnemyHitFlash flash = other.GetComponent<EnemyHitFlash>();
            if (reaction != null) reaction.knockback(transform.position);
            if(flash != null) flash.Flash();
            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }

    }
}
