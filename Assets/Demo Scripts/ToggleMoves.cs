using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleMoves : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerHealthNum;
    [SerializeField] private TMP_Text Dialogue;
    [SerializeField] private TMP_Text SanityNum;

    [SerializeField] private GameObject Limb1;
    [SerializeField] private GameObject Limb2;
    [SerializeField] private GameObject Limb3;

    private float PlayerMaxHealth = 100;
    private float PlayerCurrentHealth = 100;
    private float SanityMax = 100;
    private float SanityCurrent= 100;
    private float KickDamage = 10;
    private float ScreechSanityDamage = 20;
    private float Sanity75percentDamage = 8;
    private float Sanity50percentDamage = 16;

    [SerializeField] private InCombatButtonSelect InCombatButtonSelect;

    private void CheckHealth()
    {
        if (PlayerCurrentHealth == 0)
        {
            Dialogue.text = "You Died.";
            return;
        }
    }

    private void CheckSanity()
    {
        if (SanityCurrent <= 75 && SanityCurrent > 50)
        {
            Dialogue.text = "Your low sanity caused you to take damage...";
            PlayerCurrentHealth -= Sanity75percentDamage;
            SanityNum.text = $"{SanityCurrent}%";
        }

        if (SanityCurrent <= 50)
        {
            Dialogue.text = "Your low sanity caused you to take severe damage...";
            PlayerCurrentHealth -= Sanity50percentDamage;
            SanityNum.text = $"{SanityCurrent}%";
        }
        
    }

    public void RandomizeMoves()
    {
        Dialogue.text = "You attacked the enemy...";

        int randomNum = Random.Range(1, 3);

        if ( randomNum == 1) 
        { 
            ToggleBasicKick();
        }
        else
        {
            ToggleScreech();
        }

    }

    //Toggle Enemy Moves
    private void ToggleBasicKick()
    {
        Dialogue.text = $"Enemy kicked you for {KickDamage}";
        PlayerCurrentHealth -= KickDamage;
        PlayerHealthNum.text = $"{PlayerCurrentHealth}";

        CheckHealth();

        Limb1.SetActive(false);
        Limb2.SetActive(false);
        Limb3.SetActive(false);

        InCombatButtonSelect.CurrentMenu = InCombatButtonSelect.CombatMenu.MainMenu;
        InCombatButtonSelect.CurrentSelection = InCombatButtonSelect.CombatButtonSelection.None;
        InCombatButtonSelect._combatSelection = 1;
    }

    private void ToggleScreech()
    {
        Dialogue.text = "Enemy let out a screech...Your sanity decreased...";
        SanityCurrent -= ScreechSanityDamage;
        SanityNum.text = $"{SanityCurrent}%";

        CheckSanity();
        CheckHealth();

        Limb1.SetActive(false);
        Limb2.SetActive(false);
        Limb3.SetActive(false);

        InCombatButtonSelect.CurrentMenu = InCombatButtonSelect.CombatMenu.MainMenu;
        InCombatButtonSelect.CurrentSelection = InCombatButtonSelect.CombatButtonSelection.None;
        InCombatButtonSelect._combatSelection = 1;
    }

    //Toggle Player Moves
    private void ToggleMaxHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
    }

    private void ToggleSanityMax()
    {
        SanityCurrent = SanityMax;
    }
}
