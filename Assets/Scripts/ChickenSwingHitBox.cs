using System.Collections.Generic;
using UnityEngine;

public class ChickenSwingHitbox : MonoBehaviour
{
    public int damage = 3;

    private readonly HashSet<Health> hitTargets = new HashSet<Health>();
    private bool isActive = false;

    public void BeginSwing(int newDamage)
    {
        damage = newDamage;
        hitTargets.Clear();
        isActive = true;
        gameObject.SetActive(true);
    }

    public void EndSwing()
    {
        isActive = false;
        hitTargets.Clear();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        Health hp = other.GetComponent<Health>();
        if (hp == null) return;

        if (hitTargets.Contains(hp)) return;

        hitTargets.Add(hp);
        hp.TakeDamage(damage);

        EnemyHitReaction reaction = other.GetComponent<EnemyHitReaction>();
        EnemyHitFlash flash = other.GetComponent<EnemyHitFlash>();

        if (reaction != null)
            reaction.knockback(transform.position);

        if (flash != null)
            flash.Flash();

        Debug.Log($"치킨 스매쉬 적중: {other.name}");
    }

}