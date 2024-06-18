using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;
    [SerializeField] private float _maxSanity = 150;
    [SerializeField] private float _currentSanity = 150;

    public float MaxHealth
    {
        get { return _maxHealth; }
    }
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    public float MaxSanity
    {
        get { return _maxSanity; }
    }
    public float CurrentSanity
    {
        get { return _currentSanity; }
        set { _currentSanity = value; }
    }
}
