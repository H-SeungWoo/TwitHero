using System;
using System.Collections;
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

    [Header("Chicken Swing Visual")]
    public GameObject chickenSwingEffectPrefab;
    public Transform chickenSwingSpawn;
    public float chickenSwingEffectLifeTime = 0.25f;
    public float chickenSwingOffsetX = 0.8f;

    [Header("Chicken Swing Setup")]
    public Transform chickenSwingPivot;
    public ChickenSwingHitbox chickenSwingHitbox;
    public float swingDuration = 0.18f;
    public float swingAngle = 120f;

    private float nextProjectileTime;
    private float nextAoeTime;
    private bool isSwinging = false;

    void Start()
    {   
        if(controller == null) controller = GetComponent<PlayerController2D>();
        if (chickenSwingHitbox != null)
            chickenSwingHitbox.gameObject.SetActive(false);
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
            StartCoroutine(DoChickenLegSwing());

            //DoChickenLegSwing();
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

    void DoChickenLegSwing2()
    {
        if (!aoeCenter) aoeCenter = transform;

        ShowChickenSwingEffect();

        Collider2D[] hits = Physics2D.OverlapCircleAll(aoeCenter.position, aoeRadius, enemyLayer);
        foreach (var h in hits)
        {
            // 전방 판정 필터
            Vector2 toEnemy = h.transform.position - transform.position;

            if (Mathf.Sign(toEnemy.x) != controller.FacingDir)
                continue;

            Health hp = h.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(aoeDamage);
            }
        }

        Debug.Log("황올 스매쉬!!");
    }

    private IEnumerator DoChickenLegSwing()
    {
        if (chickenSwingPivot == null || chickenSwingHitbox == null)
        {
            Debug.LogWarning("ChickenSwingPivot 또는 ChickenSwingHitbox가 연결되지 않았습니다.");
            yield break;
        }

        isSwinging = true;

        // 방향 반영
        Vector3 pivotScale = chickenSwingPivot.localScale;
        pivotScale.x = Mathf.Abs(pivotScale.x) * controller.FacingDir;
        chickenSwingPivot.localScale = pivotScale;

        // 시작 / 끝 각도
        float startZ = controller.FacingDir == 1 ? swingAngle * 0.5f : -swingAngle * 0.5f;
        float endZ = controller.FacingDir == 1 ? -swingAngle * 0.5f : swingAngle * 0.5f;

        chickenSwingPivot.localRotation = Quaternion.Euler(0f, 0f, startZ);

        chickenSwingHitbox.BeginSwing(aoeDamage);

        float elapsed = 0f;
        while (elapsed < swingDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / swingDuration;
            float currentZ = Mathf.Lerp(startZ, endZ, t);
            chickenSwingPivot.localRotation = Quaternion.Euler(0f, 0f, currentZ);
            yield return null;
        }

        chickenSwingHitbox.EndSwing();
        chickenSwingPivot.localRotation = Quaternion.identity;

        isSwinging = false;
    }

    void ShowChickenSwingEffect()
    {
        if (chickenSwingEffectPrefab == null)
            return;

        Vector3 spawnPos;

        if (chickenSwingSpawn != null)
        {
            spawnPos = chickenSwingSpawn.position;
        }
        else
        {
            spawnPos = transform.position;
            spawnPos.x += controller.FacingDir * chickenSwingOffsetX;
        }

        GameObject effect = Instantiate(chickenSwingEffectPrefab, spawnPos, Quaternion.identity);

        Vector3 scale = effect.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * controller.FacingDir;
        effect.transform.localScale = scale;

        Destroy(effect, chickenSwingEffectLifeTime);
    }
    void OnDrawGizmosSelected()
    {
        if (!aoeCenter) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aoeCenter.position, aoeRadius);
    }
    public float GetHallabongCooldownRemaining()
    {
        return Mathf.Max(0f, nextProjectileTime - Time.time);
    }

    public float GetChickenCooldownRemaining()
    {
        return Mathf.Max(0f, nextAoeTime - Time.time);
    }
}
