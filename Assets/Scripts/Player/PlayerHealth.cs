using System.Linq.Expressions;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;

    public bool IsInvincible { get; private set; }

    [Header("Hit")] 
    public float invincibleTime = 1.0f;

    private float invincibleTimer;

    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                IsInvincible = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsInvincible) return;

        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"Player HP: {currentHP}");

        IsInvincible = true;
        invincibleTimer = invincibleTime;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");

        GameOverUI gameOverUI = FindFirstObjectByType<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }
    }
}
