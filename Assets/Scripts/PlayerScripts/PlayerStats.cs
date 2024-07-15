using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    //Player Base Stats
    [SerializeField] private float _maxTotalHealth = 100f;
    [SerializeField] private float _maxHeadHealth;
    [SerializeField] private float _maxBodyHealth;
    [SerializeField] private float _maxLeftArmHealth;
    [SerializeField] private float _maxRightArmHealth;
    [SerializeField] private float _maxLeftLegHealth;
    [SerializeField] private float _maxRightLegHealth;
    [SerializeField] private float _baseDefence = 50f;
    [SerializeField] private float _maxSanity = 150f;
    [SerializeField] private float _baseDamage = 20f;

    //Player Current Status
    [SerializeField] private float _currentTotalHealth = 100f;
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
    public float CurrentSanity
    {
        get { return _currentSanity; }
        set { _currentSanity = value; }
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
    }
}
