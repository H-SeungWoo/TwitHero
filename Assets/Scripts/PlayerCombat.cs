using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")] public PlayerController2D controller;

    [Header("Basic Attack - Hallabong")] 
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileSpeed = 12f;
    public float projectileCooldonw = 0.2f;
    public float spawnOffsetX = 0.6f;


    [Header("Skill - Chicken Leg AoE")] 
    public float aoeRadius = 1.2f;
    public int aoeDamage = 3;
    public float aoeCooldown = 1.5f;
    public Transform aoeCenter;
    public LayerMask enemyLayer;

    private float nextProjectileTime;
    private float nextAoeTime;

    void Start()
    {   
        if(controller == null) controller = GetComponent<PlayerController2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextProjectileTime)
        {
            FireProjectile();
            nextProjectileTime = Time.time + projectileCooldonw;
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextAoeTime)
        {
            DoChickenLegSwing();
            nextAoeTime = Time.time + aoeCooldown;
        }
    }

    void FireProjectile()
    {
        if (!projectilePrefab || !projectileSpawn)
        {
            Debug.Log("no Prefab or Trnasform");
            return;
        }

        Vector3 spawnPos = transform.position;
        spawnPos.x += controller.FacingDir * spawnOffsetX;
        if (projectileSpawn != null) spawnPos.y = projectileSpawn.position.y;
        GameObject go = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile2D proj = go.GetComponent<Projectile2D>();
        if (proj != null)
        {
            proj.Init(controller.FacingDir, projectileSpeed);
        }
    }

    void DoChickenLegSwing()
    {
        if (!aoeCenter) aoeCenter = transform;

        Collider2D[] hits = Physics2D.OverlapCircleAll(aoeCenter.position, aoeRadius, enemyLayer);
        foreach (var h in hits)
        {
            Health hp = h.GetComponent<Health>();
            if (hp) hp.TakeDamage(aoeDamage);
        }

        Debug.Log("황올 스매쉬!!");
    }

    void onDrawnGizmosSelected()
    {
        if (!aoeCenter) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aoeCenter.position, aoeRadius);
    }
}
