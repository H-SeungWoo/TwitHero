using System.Collections;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color flashColor = Color.darkRed;
    public float flashTime = 0.08f;

    private Color originalColor;
    private Coroutine flashCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(sr == null) sr = GetComponent<SpriteRenderer>();
        if(sr != null) originalColor = sr.color;
    }

    public void Flash()
    {
        if (sr == null) return;
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        sr.color = originalColor;
        flashCoroutine = null;
    }
}
