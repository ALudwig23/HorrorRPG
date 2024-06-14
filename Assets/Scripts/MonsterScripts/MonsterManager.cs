using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private float _maxHealth { get; set; }
    private float _currentHealth { get; set; }
    private string[] _moveset {  get; set; }
    protected float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    protected float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    protected string[] Moveset
    {
        get { return _moveset; }
        set { _moveset = value;  }
    }

    protected MonsterManager(float maxHealth, float currentHealth, string[] moveset)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
        _moveset = moveset;
    }

    //Handle monsters loadout
    protected virtual void HandleLoadout()
    {

    }
}
