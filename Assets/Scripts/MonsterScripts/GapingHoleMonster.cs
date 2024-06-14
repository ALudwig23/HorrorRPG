using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapingHoleMonster : MonsterManager
{
    protected GapingHoleMonster(float maxHealth, float currentHealth, string[] moveset) : base(maxHealth, currentHealth, moveset)
    {

    }

    protected override void HandleLoadout()
    {
        GapingHoleMonster gapingHoleMonster = new GapingHoleMonster(50, 50, new string[] { "Lunge" , " Screech"});

        if (gapingHoleMonster.CurrentHealth <= 0) 
        {
        }
    }
}
