using System;
using System.Linq.Expressions;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHp = 5;
    public int currentHp;

    public EnemyHealthUI hpBarPrefab;
    private EnemyHealthUI hpBarUI;


    void Awake()
    {
        currentHp = maxHp;
    }

    private void Start()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas != null & hpBarPrefab != null)
        {
            hpBarUI = Instantiate(hpBarPrefab, canvas.transform);
            hpBarUI.SetTarget(transform);
            hpBarUI.SetHP(currentHp, maxHp);
        }
    }

    public void SetHPBar(EnemyHealthUI bar)
    {
        hpBarUI = bar;
        hpBarUI.SetTarget(transform);
        hpBarUI.SetHP(currentHp, maxHp);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        currentHp = Mathf.Max(currentHp, 0);
        Debug.Log("now HP: "+ currentHp);

        if (hpBarUI != null)
        {
            hpBarUI.SetHP(currentHp, maxHp);
        }
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (hpBarUI != null)
        {
            hpBarUI.DestroyBar();
        }

        Destroy(gameObject);
    }
}
