using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class GapingHoleMonster : MonoBehaviour
{
    private float _maxHealth = 500f;
    private float _currentHealth = 500f;
    private float _headHealth = 50f;
    private float _bodyHealth = 100f;
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
    private bool _leftLegDestroyed = false;
    private bool _rightLegDestroyed = false;
    
    //Individual Body Part Scripts
    [SerializeField] private GHM_Head _ghmHead;
    [SerializeField] private GHM_Body _ghmBody;
    [SerializeField] private GHM_LeftLeg _ghmLeftLeg;
    [SerializeField] private GHM_RightLeg _ghmRightLeg;

    //Store Body Part Trigger Values in main script
    private bool _headTrigger;
    private bool _bodyTrigger;
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
    //================================================================
    public bool HeadTrigger
    {
        get { return _headTrigger; }
    }
    public bool BodyTrigger
    {
        get { return _bodyTrigger; }
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

    private void Start()
    {
        _dialogueBox = GameObject.Find("DialogueBox");
        _dialogueText = _dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Debug.Log(_headTrigger);
        _headTrigger = _ghmHead.TargetedHead;
        _bodyTrigger = _ghmBody.TargetedBody;
        _leftLegTrigger = _ghmLeftLeg.TargetedLeftLeg;
        _rightLegTrigger = _ghmRightLeg.TargetedRightLeg;
    }

    public void OnDeath()
    {
        if (_currentHealth <= 0)
        {
            _monsterDied = true;
            SoundManager.Instance.PlaySFX("MonsterDeath");
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

        _ghmHead.TargetedHead = false;
        _ghmBody.TargetedBody = false;
        _ghmLeftLeg.TargetedLeftLeg = false;
        _ghmRightLeg.TargetedRightLeg = false;
    }

    public IEnumerator BodyDamaged()
    {
        _bodyHealth -= _playerStats.BaseDamage;
        _currentHealth = 0f;

        _text = $"The creature's body takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _ghmHead.TargetedHead = false;
        _ghmBody.TargetedBody = false;
        _ghmLeftLeg.TargetedLeftLeg = false;
        _ghmRightLeg.TargetedRightLeg = false;
    }

    public IEnumerator LeftLegDamaged()
    {
        _leftLegHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's left leg takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _ghmHead.TargetedHead = false;
        _ghmBody.TargetedBody = false;
        _ghmLeftLeg.TargetedLeftLeg = false;
        _ghmRightLeg.TargetedRightLeg = false;
    }

    public IEnumerator RightLegDamaged()
    {
        _rightLegHealth -= _playerStats.BaseDamage;
        _currentHealth -= _playerStats.BaseDamage;

        _text = $"The creature's right leg takes damage";
        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
        yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

        _finishedDialogue = true;

        _ghmHead.TargetedHead = false;
        _ghmBody.TargetedBody = false;
        _ghmLeftLeg.TargetedLeftLeg = false;
        _ghmRightLeg.TargetedRightLeg = false;
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
            _currentHealth -= _maxHealth / 3f;

            _text = $"The creature's body is destroyed";
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
        _movesRandomizer = Random.Range (0,2);

        switch (_movesRandomizer)
        {
            //Screech
            case 0:
                Debug.Log("Screech");
                _playerStats.CurrentSanity -= _sanityDamage;

                _text = $"The creature makes a disturbing noise";
                _damageDealt = true;
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
                
                _text = $"Your sanity decreased...";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);

                _finishedDialogue = true;
                break;

            //Lunge
            case 1:

                Debug.Log("Lunge");
                _playerStats.CurrentTotalHealth -= _damage * ( 100 / (100 + _playerStats.BaseDefence));

                _text = $"The creature lunges towards you";
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

