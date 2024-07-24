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
    private float _clawDamage = 50f;
    private float _biteDamage = 20f;
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
    public float ClawDamage
    {
        get { return _clawDamage; }
    }
    public float BiteDamage
    {
        get { return _biteDamage; }
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

    void Update()
    {
        Debug.Log(_headTrigger);
        _headTrigger = _bs_Head.TargetedHead;
        _bodyTrigger = _bs_Body.TargetedBody;
        _leftClawTrigger = _bs_LeftClaw.TargetedLeftClaw;
        _rightClawTrigger = _bs_RightClaw.TargetedRightClaw;
        _leftLegTrigger = _bs_LeftLeg.TargetedLeftLeg;
        _rightLegTrigger = _bs_RightLeg.TargetedRightLeg;
    }

    public void OnDeath()
    {
        if (_currentHealth <= 0)
        {
            _monsterDied = true;
            Destroy(gameObject);
        }
    }

    public IEnumerator HeadDamaged()
    {
        _headHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's head takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator BodyDamaged()
    {
        _bodyHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's body takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator LeftClawDamaged()
    {
        _leftClawHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's left claw takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator RightClawDamaged()
    {
        _rightClawHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's right claw takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator LeftLegDamaged()
    {
        _leftLegHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's left leg takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator RightLegDamaged()
    {
        _rightLegHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's right leg takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _bs_Head.TargetedHead = false;
        _bs_Body.TargetedBody = false;
        _bs_LeftClaw.TargetedLeftClaw = false;
        _bs_RightClaw.TargetedRightClaw = false;
        _bs_LeftLeg.TargetedLeftLeg = false;
        _bs_RightLeg.TargetedRightLeg = false;
    }

    public IEnumerator OnDamage()
    {
        if (_headHealth <= 0 && _headDestroyed == false)
        {
            _headDestroyed = true;
            _currentHealth -= _maxHealth / 2f;

            _text = $"The creature's head is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
        }

        if (_bodyHealth <= 0 && _bodyDestroyed == false)
        {
            _bodyDestroyed = true;
            _currentHealth -= 0f;

            _text = $"The creature's body is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
        }

        if (_leftClawHealth <= 0 && _leftClawDestroyed == false)
        {
            _leftClawDestroyed = true;
            _currentHealth -= _maxHealth / 4f;

            _text = $"The creature's left claw is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        }

        if (_rightClawHealth <= 0 && _rightClawDestroyed == false)
        {
            _rightClawDestroyed = true;
            _currentHealth -= _maxHealth / 4f;

            _text = $"The creature's right claw is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
        }

        if (_leftLegHealth <= 0 && _leftLegDestroyed == false)
        {
            _leftLegDestroyed = true;
            _currentHealth -= _maxHealth / 4f;

            _text = $"The creature's left leg is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        }

        if (_rightLegHealth <= 0 && _rightLegDestroyed == false)
        {
            _rightLegDestroyed = true;
            _currentHealth -= _maxHealth / 4f;

            _text = $"The creature's right leg is destroyed";
            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
            yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
        }

        _finishedDialogue = true;
    }

    public IEnumerator MovesetHandler()
    {
        if (_leftClawDestroyed == true && _rightClawDestroyed == true)
        {
            _movesRandomizer = 1;
        }
        else if (_headDestroyed == true)
        {
            _movesRandomizer = 0;
        }
        else
        {
            _movesRandomizer = Random.Range(0, 2);
        }
        

        switch (_movesRandomizer)
        {
            //Slash
            case 0:
                Debug.Log("Slash");
                _playerStats.CurrentTotalHealth -= _clawDamage * (100 / (100 + _playerStats.BaseDefence));

                _text = $"The creature slashed you";
                _damageDealt = true;
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                _text = $"You took major damage...";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);

                _finishedDialogue = true;
                break;

            //Bite
            case 1:

                Debug.Log("Bite");
                _playerStats.CurrentTotalHealth -= _biteDamage * (100 / (100 + _playerStats.BaseDefence));

                _text = $"The creature bites you";
                _damageDealt = true;
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                _text = $"You took minor damage...";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);

                _finishedDialogue = true;
                break;
        }
    }
}
