using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BattleManager : MonoBehaviour
{
    private float _miniTimer = 1f;
    private float _waitTime = 0.1f;
    private float _enemyCount;
    private float _enemyTypeSpawnChance;

    private bool _battleWon;
    private bool _attackMissed;

    private string _text;
    private string _leftMonster;
    private string _middleMonster;
    private string _rightMonster;

    private enum BattleState { PlayerTurn, MonsterTurn, BattleEnd }

    public bool BattleWon
    {
        get { return _battleWon; }
    }

    //Battle State
    [Header("Battle State")]
    [SerializeField] private BattleState _battleState;

    //General Reference
    [Header("General Object Reference")]
    [SerializeField] private TMP_FontAsset _fontAsset;
    [SerializeField] private Sprite _buttonSprite;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DialogueTypingManager _dialogueTypingManager;
    [SerializeField] private CursorMovement _cursorMovement;
    [SerializeField] private Light2D _battleSpotLight;
    private GameObject _previousSelection;
    private Coroutine _coroutine;
    private GameManager _gameManager;

    //Battle Options
    [Header("Main Battle Options Reference")]
    [SerializeField] private GameObject _fightOption;
    [SerializeField] private Button _fightButton;
    [SerializeField] private GameObject _statusOption;
    [SerializeField] private Button _statusButton;
    [SerializeField] private GameObject _actionsOption;
    [SerializeField] private Button _actionsButton;
    [SerializeField] private GameObject _runOption;
    [SerializeField] private Button _runButton;

    //Fight Options
    [Header("Fight Options")]

    //Display UI
    [Header("General UI Display")]
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private TMP_Text _playerSanity;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject _mainDisplay;
    [SerializeField] private GameObject _fightDisplay;
    [SerializeField] private GameObject _statusDisplay;
    [SerializeField] private GameObject _actionDisplay;
    [SerializeField] private GameObject _uiBackground;

    //Monster Types
    [Header("Monster Type Reference")]
    [SerializeField] private GameObject _monsterPrefab1;
    [SerializeField] private GameObject _miniMonsterPrefab1;
    [SerializeField] private GameObject _gapingHoleMonster;
    [SerializeField] private GameObject _miniGapingHoleMonster;
    [SerializeField] private GapingHoleMonster _gapingHoleMonsterScript;

    [SerializeField] private SpiderMonster _spiderMonster;

    void Start()
    {
        _battleState = BattleState.PlayerTurn;

        _gameManager = FindObjectOfType<GameManager>();

        _playerHealth.text = $"Total HP: {(int)_playerStats.CurrentTotalHealth} / {(int)_playerStats.MaxTotalHealth}";
        _playerSanity.text = $"Sanity: {(int)_playerStats.CurrentSanity} / {(int)_playerStats.MaxSanity}";

        StartCoroutine(MonsterSetup());
        StartCoroutine(HandleState());
    }

    void Update()
    {
        if (_cursorMovement.EnterPressed == true)
        {
            StartCoroutine(TargetedMonsterLimbs());
            _cursorMovement.EnterPressed = false;
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(EventSystem.current);
    }

    private void CheckSanity()
    {
        if (_playerStats.CurrentSanity <= 50f)
        {
            _battleSpotLight.intensity = 0f;
            Debug.Log("SanityLow");
            
            SpriteRenderer[] fightDisplayMonsters = _fightDisplay.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in fightDisplayMonsters)
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    private IEnumerator MonsterSetup()
    {
        _enemyCount = Random.Range(1, 4);
        _middleMonster = _gameManager.CollidedMonsterType;

        //ColorBlock buttonSelectedColour;

        switch (_middleMonster)
        {
            case "GapingHoleMonster":

                //Load the prefab for GapingHoleMonster
                _monsterPrefab1 = Resources.Load<GameObject>("GapingHoleMonster");
                _miniMonsterPrefab1 = Resources.Load<GameObject>("GapingHoleMini");

                //Set up prefab location
                GameObject gapingHoleMonster = Instantiate(_monsterPrefab1);
                gapingHoleMonster.transform.SetParent(_mainDisplay.transform);
                gapingHoleMonster.transform.position = new Vector3(_mainDisplay.transform.position.x, _mainDisplay.transform.position.y, -1f);

                GameObject miniGapingHoleMonster = Instantiate(_miniMonsterPrefab1);
                miniGapingHoleMonster.transform.SetParent(_fightDisplay.transform);
                miniGapingHoleMonster.transform.position = new Vector3(_fightDisplay.transform.position.x, _fightDisplay.transform.position.y, -1f);

                _gapingHoleMonsterScript = miniGapingHoleMonster.GetComponent<GapingHoleMonster>();
                yield return new WaitForSeconds(0.1f);

                break;

            case "SpiderMonster":

                _monsterPrefab1 = Resources.Load<GameObject>("SpiderMonster");

                break;
        }
    }

    private IEnumerator HandleState()
    {
        //Allow objects to load in before proceeding
        yield return new WaitForSeconds(_waitTime);
        Debug.Log("Handling State");
        switch (_battleState)
        {
            case BattleState.PlayerTurn:

                _fightOption.SetActive(true);
                _statusOption.SetActive(true);
                _actionsOption.SetActive(true);
                _runOption.SetActive(true);
                _uiBackground.SetActive(true);

                StopAllCoroutines();
                EventSystem.current.SetSelectedGameObject(_fightOption);
                break;

            case BattleState.MonsterTurn:

                _fightDisplay.SetActive(false);
                _uiBackground.SetActive(false);

                //Wait for previous dialogue to finish
                if (_attackMissed == false)
                {
                    yield return new WaitUntil(() => _gapingHoleMonsterScript.FinishedDialogue == true);
                    _gapingHoleMonsterScript.FinishedDialogue = false;
                }
                else
                {
                    yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
                }

                StartCoroutine(HandleMonsterTurn());

                break;

            case BattleState.BattleEnd:

                if (_playerStats.CurrentTotalHealth > 0f)
                {
                    _dialogueTypingManager.StopDialogue();
                    _text = "You killed the monster...";
                    _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                    _battleWon = true;
                }
                else if (_playerStats.CurrentTotalHealth <= 0f)
                {
                    _dialogueTypingManager.StopDialogue();
                    _text = "You died...";
                    _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                    _battleWon = false;
                }
                break;
        }
    }

    private IEnumerator HandleMonsterTurn()
    {
        yield return new WaitForSeconds(_waitTime);

        Debug.Log("Handling Monster Turn");

        if (_leftMonster != null)
        {
            switch (_leftMonster)
            {
                case "":
                    break;
            }
        }

        if (_middleMonster != null)
        {
            switch (_middleMonster)
            {
                case "GapingHoleMonster":

                    StartCoroutine(_gapingHoleMonsterScript.OnDamage());
                    yield return new WaitUntil(() => _gapingHoleMonsterScript.FinishedDialogue == true);

                    _gapingHoleMonsterScript.FinishedDialogue = false;
                    _gapingHoleMonsterScript.OnDeath();

                    //End early if enemy or player dies
                    if (_gapingHoleMonsterScript.MonsterDied == true || _playerStats.CurrentTotalHealth <= 0f)
                    {
                        _battleState = BattleState.BattleEnd;
                        StartCoroutine(HandleState());
                    }
                    else
                    {
                        StartCoroutine(_gapingHoleMonsterScript.MovesetHandler());
                        yield return new WaitUntil(() => _gapingHoleMonsterScript.DamageDealt == true);
                        _playerHealth.text = $"Total HP: {(int)_playerStats.CurrentTotalHealth} / {(int)_playerStats.MaxTotalHealth}";
                        _playerSanity.text = $"Sanity: {(int)_playerStats.CurrentSanity} / {(int)_playerStats.MaxSanity}";
                        _gapingHoleMonsterScript.DamageDealt = false;
                        yield return new WaitUntil(() => _gapingHoleMonsterScript.FinishedDialogue == true);

                        _gapingHoleMonsterScript.FinishedDialogue = false;

                        CheckSanity();

                        Debug.Log("Finished State");
                        _battleState = BattleState.PlayerTurn;
                        StartCoroutine(HandleState());
                    }

                    break;
            }
        }

        if (_rightMonster != null)
        {
            switch (_rightMonster)
            {
                case "":
                    break;
            }

        }

        yield return null;
    }

    //Limbs for respective monsters
    private IEnumerator TargetedMonsterLimbs()
    {
        EventSystem.current.SetSelectedGameObject(null);

        yield return new WaitForSeconds(_waitTime);

        Debug.Log("Handling Target");

        if (_leftMonster != null)
        {
            switch (_leftMonster)
            {
                case "":
                    break;
            }
        }                                                    

        if (_middleMonster != null)
        {
            switch (_middleMonster)
            {
                case "GapingHoleMonster":

                    if (_gapingHoleMonsterScript.HeadTrigger == true)
                    {
                        if (_gapingHoleMonsterScript.HeadHealth <= 0)
                        {
                            _dialogueTypingManager.StopDialogue();
                            _text = "This part has already been destroyed";
                            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                        }
                        else
                        {
                            _dialogueTypingManager.StopDialogue();
                            StartCoroutine(_gapingHoleMonsterScript.HeadDamaged());

                            _battleState = BattleState.MonsterTurn;
                            _attackMissed = false;
                            StartCoroutine(HandleState());
                        }
                    }

                    else if (_gapingHoleMonsterScript.BodyTrigger == true)
                    {
                        if (_gapingHoleMonsterScript.BodyHealth <= 0)
                        {
                            _dialogueTypingManager.StopDialogue();
                            _text = "This part has already been destroyed";
                            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                        }
                        else
                        {
                            _dialogueTypingManager.StopDialogue();
                            StartCoroutine(_gapingHoleMonsterScript.BodyDamaged());

                            _battleState = BattleState.MonsterTurn;
                            _attackMissed = false;
                            StartCoroutine(HandleState());
                        }
                    }

                    else if (_gapingHoleMonsterScript.LeftLegTrigger == true)
                    {
                        if (_gapingHoleMonsterScript.LeftLegHealth <= 0)
                        {
                            _dialogueTypingManager.StopDialogue();
                            _text = "This part has already been destroyed";
                            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                        }
                        else
                        {
                            _dialogueTypingManager.StopDialogue();
                            StartCoroutine(_gapingHoleMonsterScript.LeftLegDamaged());

                            _battleState = BattleState.MonsterTurn;
                            _attackMissed = false;
                            StartCoroutine(HandleState());
                        }
                    }

                    else if (_gapingHoleMonsterScript.RightLegTrigger == true)
                    {
                        if (_gapingHoleMonsterScript.RightLegHealth <= 0)
                        {
                            _dialogueTypingManager.StopDialogue();
                            _text = "This part has already been destroyed";
                            _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                        }
                        else
                        {
                            _dialogueTypingManager.StopDialogue();
                            StartCoroutine(_gapingHoleMonsterScript.RightLegDamaged());

                            _battleState = BattleState.MonsterTurn;
                            _attackMissed = false;
                            StartCoroutine(HandleState());
                        }
                    }

                    else
                    {
                        _dialogueTypingManager.StopDialogue();
                        _text = "Your attack missed...";
                        _dialogueTypingManager.StartDialogue(_text, _dialogueText);

                        _attackMissed = true;
                        _battleState = BattleState.MonsterTurn;
                        StartCoroutine(HandleState());
                    }

                    break;
            }
        }

        if (_rightMonster != null)
        {
            switch (_rightMonster)
            {
                case "":
                    break;
            }

        }

        yield return null;
    }
}
