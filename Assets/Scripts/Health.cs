using System.Linq.Expressions;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHp = 5;

    public int hp;

    void Awake()
    {
        hp = maxHp;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Debug.Log("now HP: "+ hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
