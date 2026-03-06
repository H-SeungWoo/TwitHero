using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider hpSlider;
    public TMP_Text hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerHealth != null && hpSlider != null)
        {
            hpSlider.maxValue = playerHealth.maxHP;
            hpSlider.value = playerHealth.currentHP;
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (playerHealth == null) return;

        if (hpSlider != null)
        {
            hpSlider.value = playerHealth.currentHP;
        }

        if (hpText != null)
        {
            hpText.text = $"HP: {playerHealth.currentHP} / {playerHealth.maxHP}";
        }
    }
}
