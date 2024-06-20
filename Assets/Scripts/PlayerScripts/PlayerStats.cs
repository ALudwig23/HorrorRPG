using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _maxSanity = 150f;
    [SerializeField] private float _currentSanity = 150f;
    [SerializeField] private float _damage = 20f;

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
    public float Damage
    {
        get { return _damage; }
    }
}
