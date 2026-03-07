using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Slider slider;
    public Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);

    private Transform target;
    private Camera mainCam;
    private RectTransform rectTransform;

    void Awake()
    {
        mainCam = Camera.main;
        rectTransform = GetComponent<RectTransform>();

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetHP(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            DestroyBar();
            return;
        }

        Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + worldOffset);

        if (screenPos.z < 0)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        rectTransform.position = screenPos;
    }

    public void DestroyBar()
    {
        Destroy(gameObject);
    }

}
