using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoneSpider : MonoBehaviour
{
    private float _maxHealth = 400f;
    private float _currentHealth = 400f;
    private float _headHealth = 200f;
    private float _bodyHealth = 100f;
    private float _leftClawHealth = 25f;
    private float _rightClawHealth = 25f;
    private float _leftLegHealth = 25f;
    private float _rightLegHealth = 25f;
    private float _damage = 20f;
    private float _sanityDamage = 25f;

    private int _movesRandomizer;

    private string _text;

    private bool _finishedDialogue = false;
    private bool _damageDealt = false;
    private bool _monsterDied = false;

    private bool _headDestroyed = false;
    private bool _bodyDestroyed = false;
    private bool _leftClawDestroyed = false;
    private bool _rightClawDestroyed = false;
    private bool _leftLegDestroyed = false;
    private bool _rightLegDestroyed = false;

    //Individual Body Part Scripts
    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
    [SerializeField] private BS_RightClaw _bs_RightClaw;
    [SerializeField] private BS_LeftLeg _bs_LeftLeg;
    [SerializeField] private BS_RightLeg _bs_RightLeg;

    //Store Body Part Trigger Values in main script
    private bool _headTrigger;
    private bool _bodyTrigger;
    private bool _leftClawTrigger;
    private bool _rightClawTrigger;
    private bool _leftLegTrigger;
    private bool _rightLegTrigger;

    [SerializeField] private GameObject _dialogueBox;

    [SerializeField] private TMP_FontAsset _fontAsset;
    [SerializeField] private Sprite _buttonSprite;
    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private Coroutine _dialogueCoroutine;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DialogueTypingManager _dialogueTypingManager;

    public float MaxHealth
    {
        get { return _maxHealth; }
    }
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    public float HeadHealth
    {
        get { return _headHealth; }
    }
    public float BodyHealth
    {
        get { return _bodyHealth; }
    }
    public float LeftClawHealth
    {
        get { return _leftClawHealth; }
    }
    public float RightClawHealth
    {
        get { return _rightClawHealth; }
    }
    public float LeftLegHealth
    {
        get { return _leftLegHealth; }
    }
    public float RightLegHealth
    {
        get { return _rightLegHealth; }
    }
    //=============================================================
    public float Damage
    {
        get { return _damage; }
    }
    public float SanityDamage
    {
        get { return _sanityDamage; }
    }
    public bool MonsterDied
    {
        get { return _monsterDied; }
    }
    //=============================================================
    public bool HeadTrigger
    {
        get { return _headTrigger; }
    }
    public bool BodyTrigger
    {
        get { return _bodyTrigger; }
    }
    public bool LeftClawTrigger
    {
        get { return _leftClawTrigger; }
    }
    public bool RightClawTrigger
    {
        get { return _rightClawTrigger; }
    }
    public bool LeftLegTrigger
    {
        get { return _leftLegTrigger; }
    }
    public bool RightLegTrigger
    {
        get { return _rightLegTrigger; }
    }
    //================================================================
    public bool FinishedDialogue
    {
        get { return _finishedDialogue; }
        set { _finishedDialogue = value; }
    }
    public bool DamageDealt
    {
        get { return _damageDealt; }
        set { _damageDealt = value; }
    }
    void Start()
    {
        _dialogueBox = GameObject.Find("DialogueBox");
        _dialogueText = _dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
