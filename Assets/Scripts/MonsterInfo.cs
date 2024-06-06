using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo
{
    public string Name { get; set; }
    public int MaxHealth {  get; set; }
    public string[] Moveset { get; set; }

    public MonsterInfo (string name, int maxHealth, string[] moveset)
    {
        Name = name;
        MaxHealth = maxHealth;
        Moveset = moveset;
    }
}

public class Monster1 : MonsterInfo
{
    public Monster1(string name, int maxHealth, string[] moveset) : base(name, maxHealth, moveset) { }


}
