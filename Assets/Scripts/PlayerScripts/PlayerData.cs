using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Player Base Stats
    [SerializeField] private float _maxTotalHealth;
    [SerializeField] private float _maxHeadHealth;
    [SerializeField] private float _maxBodyHealth;
    [SerializeField] private float _maxLeftArmHealth;
    [SerializeField] private float _maxRightArmHealth;
    [SerializeField] private float _maxLeftLegHealth;
    [SerializeField] private float _maxRightLegHealth;
    [SerializeField] private float _maxSanity;
    [SerializeField] private float _baseDefence;
    [SerializeField] private float _baseDamage;

    //Player Current Status
    [SerializeField] private float _currentTotalHealth;
    [SerializeField] private float _currentSanity;
    [SerializeField] private float[] _currentPosition;

    public float MaxTotalHealth
    {
        get { return _maxTotalHealth; }
    }
    public float MaxHeadHealth
    {
        get { return _maxHeadHealth; }
    }
    public float MaxBodyHealth
    {
        get { return _maxBodyHealth; }
    }
    public float MaxLeftArmHealth
    {
        get { return _maxLeftArmHealth; }
    }
    public float MaxRightArmHealth
    {
        get { return _maxRightArmHealth; }
    }
    public float MaxLeftLegHealth
    {
        get { return _maxLeftLegHealth; }
    }
    public float MaxRightLegHealth
    {
        get { return _maxRightLegHealth; }
    }
    public float BaseDefence
    {
        get { return _baseDefence; }
    }
    public float MaxSanity
    {
        get { return _maxSanity; }
    }
    public float CurrentSanity
    {
        get { return _currentSanity; }
    }
    public float BaseDamage
    {
        get { return _baseDamage; }
    }
    //==============================================================
    public float CurrentTotalHealth
    {
        get { return _currentTotalHealth; }
    }

    public PlayerData(PlayerStats playerStats)
    {
        _maxTotalHealth = playerStats.MaxTotalHealth;
        _maxHeadHealth = playerStats.MaxHeadHealth;
        _maxBodyHealth = playerStats.MaxBodyHealth;
        _maxLeftArmHealth = playerStats.MaxLeftArmHealth;
        _maxRightArmHealth = playerStats.MaxRightArmHealth;
        _maxLeftLegHealth = playerStats.MaxLeftLegHealth;
        _maxRightLegHealth = playerStats.MaxRightLegHealth;
        _maxSanity = playerStats.MaxSanity;
        _baseDefence = playerStats.BaseDefence;
        _baseDamage = playerStats.BaseDamage;
        
        
    }
}
