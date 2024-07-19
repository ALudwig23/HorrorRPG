using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    private bool _isDead;

    //Player Base Stats
    [SerializeField] private float _maxTotalHealth = 200f;
    [SerializeField] private float _maxHeadHealth = 100f;
    [SerializeField] private float _maxBodyHealth = 150f;
    [SerializeField] private float _maxLeftArmHealth = 50f;
    [SerializeField] private float _maxRightArmHealth = 50f;
    [SerializeField] private float _maxLeftLegHealth = 75f;
    [SerializeField] private float _maxRightLegHealth = 75f;
    [SerializeField] private float _baseDefence = 50f;
    [SerializeField] private float _maxSanity = 150f;
    [SerializeField] private float _baseDamage = 20f;

    //Player Current Status
    [SerializeField] private float _currentTotalHealth = 100f;
    [SerializeField] private float _currentHeadHealth = 100f;
    [SerializeField] private float _currentBodyHealth = 150f;
    [SerializeField] private float _currentLeftArmHealth = 50f;
    [SerializeField] private float _currentRightArmHealth = 50f;
    [SerializeField] private float _currentLeftLegHealth = 75f;
    [SerializeField] private float _currentRightLegHealth = 75f;
    [SerializeField] private float _currentSanity = 150f;
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
    public float BaseDamage
    {
        get { return _baseDamage; }
    }
    //==============================================================
    public float CurrentTotalHealth
    {
        get { return _currentTotalHealth; }
        set { _currentTotalHealth = value; }
    }
    public float CurrentHeadHealth
    {
        get { return _currentHeadHealth; }
        set { _currentHeadHealth = value; }
    }
    public float CurrentBodyHealth
    {
        get { return _currentBodyHealth; }
        set { _currentBodyHealth = value; }
    }
    public float CurrentLeftArmHealth
    {
        get { return _currentLeftArmHealth; }
        set { _currentLeftArmHealth = value; }
    }
    public float CurrentRightArmHealth
    {
        get { return _currentRightArmHealth; }
        set { _currentRightArmHealth = value; }
    }
    public float CurrentLeftLegHealth
    {
        get { return _currentLeftLegHealth; }
        set { _currentLeftLegHealth = value; }
    }
    public float CurrentRightLegHealth
    {
        get { return _currentRightLegHealth; }
        set { _currentRightLegHealth = value; }
    }
    public float CurrentSanity
    {
        get { return _currentSanity; }
        set { _currentSanity = value; }
    }

    public void SavePlayerStats()
    {
        SaveData.SavePlayerStats(this);
    }

    public void LoadPlayerStats()
    {
        PlayerData playerData = SaveData.LoadPlayerStats();

        _maxTotalHealth = playerData.MaxTotalHealth;
        _maxHeadHealth = playerData.MaxHeadHealth;
        _maxBodyHealth = playerData.MaxBodyHealth;
        _maxLeftArmHealth = playerData.MaxLeftArmHealth;
        _maxRightArmHealth = playerData.MaxRightArmHealth;
        _maxLeftLegHealth = playerData.MaxLeftLegHealth;
        _maxRightLegHealth = playerData.MaxRightLegHealth;
        _maxSanity = playerData.MaxSanity;
        _baseDefence = playerData.BaseDefence;
        _baseDamage = playerData.BaseDamage;

        _currentTotalHealth = playerData.CurrentTotalHealth;
        _currentHeadHealth = playerData.CurrentHeadHealth;
        _currentBodyHealth = playerData.CurrentBodyHealth;
        _currentLeftArmHealth = playerData.CurrentLeftArmHealth;
        _currentRightArmHealth = playerData.CurrentRightArmHealth;
        _currentLeftLegHealth = playerData.CurrentLeftLegHealth;
        _currentRightLegHealth = playerData.CurrentRightLegHealth;
        _currentSanity = playerData.CurrentSanity;
    }

    public void ResetToBaseStats()
    {
        _currentTotalHealth = _maxTotalHealth;
        _currentHeadHealth = _maxHeadHealth;
        _currentBodyHealth = _maxBodyHealth;
        _currentLeftArmHealth = _maxLeftArmHealth;
        _currentRightArmHealth = _maxRightArmHealth;
        _currentLeftLegHealth = _maxLeftLegHealth;
        _currentRightLegHealth = _maxRightLegHealth;
        _currentSanity = _maxSanity;
    }

    public void DamageTaken(float damage, float sanityDamage)
    {
        
    }

    private void CheckHealth()
    {
        if (_currentTotalHealth <= 0f || _currentHeadHealth <= 0f || _currentBodyHealth <= 0f)
        {
            _isDead = true;
        }
    }

    private void CheckSanity()
    {

    }
}
