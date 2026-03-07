using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("Hallabong UI")]
    public Image hallabongOverlay;
    public TMP_Text hallabongText;

    [Header("Chicken UI")]
    public Image chickenOverlay;
    public TMP_Text chickenText;

    [Header("Player Combat")]
    public PlayerCombat playerCombat;

    void Update()
    {
        if (playerCombat == null) return;

        UpdateSkill(
            playerCombat.GetHallabongCooldownRemaining(),
            playerCombat.projectileCooldonw,
            hallabongOverlay,
            hallabongText
        );

        UpdateSkill(
            playerCombat.GetChickenCooldownRemaining(),
            playerCombat.aoeCooldown,
            chickenOverlay,
            chickenText
        );
    }

    void UpdateSkill(float remain, float maxCooldown, Image overlay, TMP_Text text)
    {
        if (overlay == null) return;

        if (remain <= 0f)
        {
            overlay.fillAmount = 0f;
            if (text != null) text.text = "";
            return;
        }

        overlay.fillAmount = remain / maxCooldown;

        if (text != null)
        {
            text.text = remain.ToString("F1");
        }
    }
}