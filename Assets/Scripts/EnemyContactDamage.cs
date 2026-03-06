using System;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionStay2D(Collision2D other)
    {

        if (!other.gameObject.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            bool wasInvincible = playerHealth.IsInvincible;
            playerHealth.TakeDamage(damage);

            if (!wasInvincible)
            {
                PlayerHitReaction reaction = other.gameObject.GetComponent<PlayerHitReaction>();
                if (reaction != null)
                {
                    reaction.knockback((transform.position));
                }
            }
        }
    }
}
