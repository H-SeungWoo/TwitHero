using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")] public Transform player;

    [Header("Move")] 
    public float moveSpeed = 2f;
    public float detectRange = 6f;
    public float stopRange = 1.2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyHitReaction hitReaction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hitReaction = GetComponent<EnemyHitReaction>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (hitReaction != null && hitReaction.IsHitStunned)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > detectRange)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (dist <= stopRange)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float dir = MathF.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        if (sr != null)
        {
            //우방향 - 그대로 , 좌방향 - 뒤집기
            sr.flipX = dir < 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
