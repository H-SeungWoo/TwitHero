using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerHitFlash : MonoBehaviour
{
    public SpriteRenderer sr;
    public float blinkInterval = 0.1f;

    private PlayerHealth playerHealth;
    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(sr==null)
            sr = GetComponent<SpriteRenderer>();

        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr == null || playerHealth == null) return;

        if (playerHealth.IsInvincible)
        {
            timer += Time.deltaTime;
            if (timer >= blinkInterval)
            {
                timer = 0f;
                Color c = sr.color;
                c.a = (c.a == 1f) ? 0.4f : 1f;
                sr.color = c;
            }
        }
        else if (sr.color.a == 0.4f)
        {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
    }
}
