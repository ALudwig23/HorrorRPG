using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private enum BattleState { PlayerTurn, MonsterTurn, BattleEnd}
    [SerializeField] private BattleState _battleState;

    private float _miniTimer = 1f;
    private float _waitTime = 0.1f;
    private float _enemyCount;
    private bool _battleWon;
    private bool _monsterLimbsUIActive = false;
    private string _text;
    private string _displayPlayerHealth;
    private string _displayPlayerSanity;
    public bool BattleWon
    {
        get { return _battleWon; }
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _previousSelection;

    //Spawn Position
    [SerializeField] private GameObject _spawnPositionMid;
    [SerializeField] private GameObject _spawnPositionRight;
    [SerializeField] private GameObject _spawnPositionLeft;

    //Battle Options
    [SerializeField] private GameObject _fightOption;
    [SerializeField] private Button _fightButton;
    [SerializeField] private GameObject _specialActionsOption;
    [SerializeField] private Button _specialActionsButton;
    [SerializeField] private GameObject _runOption;
    [SerializeField] private Button _runButton;

    //Fight Options
    [SerializeField] private GameObject _selectedMonsterLeft;
    [SerializeField] private GameObject _selectedMonsterMiddle;
    [SerializeField] private GameObject _selectedMonsterRight;
    [SerializeField] private List<GameObject> _selectedLimbLeftMonster;
    [SerializeField] private List<GameObject> _selectedLimbMiddleMonster;
    [SerializeField] private List<GameObject> _selectedLimbRightMonster;

    //Display UI
    [SerializeField] private GameObject _displayOptionBox;
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private GameObject _statusEffect;
    [SerializeField] private TMP_Text _playerSanity;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _dialogueText;

    //Monster Types
    private GameObject _monsterPrefab;
    [SerializeField] private GapingHoleMonster _gapingHoleMonster;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DialogueTypingManager _dialogueTypingManager;
    [SerializeField] private Coroutine _coroutine;
    [SerializeField] private GameManager _gameManager;


    void Start()
    {
        _battleState = BattleState.PlayerTurn;
        _playerStats = Resources.Load<PlayerStats>("PlayerStatsData");
        _gameManager = FindObjectOfType<GameManager>();

        _displayPlayerHealth = $"HP: {(int)_playerStats.CurrentHealth} / {_playerStats.MaxHealth}";
        _displayPlayerSanity = $"Sanity: {_playerStats.CurrentSanity} / {_playerStats.MaxSanity}";

        _playerHealth.text = _displayPlayerHealth;
        _playerSanity.text = _displayPlayerSanity;

        StartCoroutine(MonsterSetup());
        StartCoroutine(HandleState());
    }

    void Update()
    {
        Debug.Log(EventSystem.current);
        SelectionUI();

        if (Input.GetKeyDown(KeyCode.Backspace) && _miniTimer == 1f)
        {
            EventSystem.current.SetSelectedGameObject(_previousSelection);
            //----stopped here----
        }
    }

    private void CheckSanity()
    {
        if (_playerStats.CurrentSanity <= _playerStats.MaxSanity / 4)
        {

        }
    }

    private IEnumerator MonsterSetup()
    {
        _enemyCount = Random.Range(1, 4);

        switch (_gameManager.CollidedMonsterType)
        {
            case "GapingHoleMonster":
                
                //Load the prefab for GapingHoleMonster
                _monsterPrefab = Resources.Load<GameObject>("GapingHoleMonster");

                //Set the prefab as middle position child object
                GameObject gapingHoleMonster = Instantiate(_monsterPrefab);
                gapingHoleMonster.transform.SetParent(_spawnPositionMid.transform);

                _gapingHoleMonster = FindObjectOfType<GapingHoleMonster>();
                yield return new WaitForSeconds(0.1f);

                _gapingHoleMonster.CreateLimbTarget();

                _selectedLimbMiddleMonster.Add(_gapingHoleMonster.LeftLimbSelection);
                _selectedLimbMiddleMonster.Add(_gapingHoleMonster.RightLimbSelection);
                _selectedLimbMiddleMonster.Add(_gapingHoleMonster.HeadLimbSelection);

                //Configure button image and colour
                //Enemy Selection Button
                _selectedMonsterMiddle = new GameObject("MiddleMonsterSelect");
                _selectedMonsterMiddle.transform.SetParent(_canvas.transform, false);

                _selectedMonsterMiddle.AddComponent<Button>();

                RectTransform middleSelectRect = _selectedMonsterLeft.GetComponent<RectTransform>();
                middleSelectRect.anchoredPosition = new Vector2(-74f, -145.5f);
                middleSelectRect.sizeDelta = new Vector2(115f, 147f);

                //Left Leg button
                Button leftLegButton = _selectedLimbMiddleMonster[0].GetComponent<Button>();
                Image leftLegButtonImage = _selectedLimbMiddleMonster[0].GetComponent<Image>();
                ColorBlock selectedColour = leftLegButton.colors; //Main colour for selection

                leftLegButton.targetGraphic = leftLegButtonImage;
                leftLegButton.transition = Selectable.Transition.ColorTint;
                selectedColour.selectedColor = Color.red;
                leftLegButton.colors = selectedColour;

                //Right Leg Button
                Button rightLegButton = _selectedLimbMiddleMonster[1].GetComponent<Button>();
                Image rightLegButtonImage = _selectedLimbMiddleMonster[1].GetComponent<Image>();

                rightLegButton.targetGraphic = rightLegButtonImage;
                rightLegButton.transition = Selectable.Transition.ColorTint;
                rightLegButton.colors = selectedColour;

                //Head Button
                Button headButton = _selectedLimbMiddleMonster[2].GetComponent<Button>();
                Image headButtonImage = _selectedLimbMiddleMonster[2].GetComponent<Image>();

                headButton.targetGraphic = headButtonImage;
                headButton.transition = Selectable.Transition.ColorTint;
                headButton.colors = selectedColour;

                //Options navigation configuration
                Navigation leftLegButtonNavigation = leftLegButton.navigation;
                leftLegButtonNavigation.mode = Navigation.Mode.Explicit;
                leftLegButtonNavigation.selectOnRight = rightLegButton;
                leftLegButton.navigation = leftLegButtonNavigation;

                Navigation rightLegButtonNavigation = rightLegButton.navigation;
                rightLegButtonNavigation.mode = Navigation.Mode.Explicit;
                rightLegButtonNavigation.selectOnLeft = leftLegButton;
                rightLegButtonNavigation.selectOnRight = headButton;
                rightLegButton.navigation = rightLegButtonNavigation;

                Navigation headButtonNavigation = headButton.navigation;
                headButtonNavigation.mode = Navigation.Mode.Explicit;
                headButtonNavigation.selectOnLeft = rightLegButton;
                headButton.navigation = headButtonNavigation;

                Debug.Log("Encountered Monster With a Gaping Hole");
                break;

            case "":


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
                _specialActionsOption.SetActive(true);
                _runOption.SetActive(true);
                _displayOptionBox.SetActive(true);

                StopAllCoroutines();
                EventSystem.current.SetSelectedGameObject(_fightOption);
                break;

            case BattleState.MonsterTurn:

                _fightOption.SetActive(false);
                _specialActionsOption.SetActive(false);
                _runOption.SetActive(false);
                _displayOptionBox.SetActive(false);

                for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
                {
                    _selectedLimbMiddleMonster[i].SetActive(false);
                }

                if (_gapingHoleMonster != null)
                {
                    //Wait for previous dialogue to finish
                    Debug.Log(_dialogueTypingManager.ToNextDialogue);
                    yield return new WaitUntil(() => _gapingHoleMonster.FinishedDialogue == true);
                    
                    _gapingHoleMonster.FinishedDialogue = false;

                    StartCoroutine(_gapingHoleMonster.OnDamage());
                    yield return new WaitUntil(() => _gapingHoleMonster.FinishedDialogue == true);

                    _gapingHoleMonster.FinishedDialogue = false;
                    _gapingHoleMonster.OnDeath();

                    //End early if enemy or player dies
                    if (_gapingHoleMonster.MonsterDied == true || _playerStats.CurrentHealth <= 0f)
                    {
                        _battleState = BattleState.BattleEnd;
                        StartCoroutine(HandleState());
                    }
                    else
                    {
                        StartCoroutine(_gapingHoleMonster.MovesetHandler());
                        yield return new WaitUntil(() => _gapingHoleMonster.DamageDealt == true);
                        _playerHealth.text = _displayPlayerHealth;
                        _playerSanity.text = _displayPlayerSanity;
                        _gapingHoleMonster.DamageDealt = false;
                        yield return new WaitUntil(() => _gapingHoleMonster.FinishedDialogue == true);

                        _gapingHoleMonster.FinishedDialogue = false;

                        _battleState = BattleState.PlayerTurn;
                        StartCoroutine(HandleState());
                    }
                }
                break;

            case BattleState.BattleEnd:

                if (_playerStats.CurrentHealth > 0f)
                {
                    _dialogueTypingManager.StopDialogue();
                    _text = "You killed the monster...";
                    _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                    _battleWon = true;
                }
                else if (_playerStats.CurrentHealth <= 0f)
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

    private void SelectionUI()
    {
        //Show fight options
        if (_battleState == BattleState.PlayerTurn && EventSystem.current.currentSelectedGameObject == _fightOption)
        {
            if (_selectedMonsterLeft != null)
            {
                _selectedMonsterLeft.SetActive(true);
            }
            if (_selectedMonsterMiddle != null)
            {
                _selectedMonsterRight.SetActive(true);
            }
            if (_selectedMonsterMiddle != null)
            {
                _selectedMonsterRight.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _previousSelection = EventSystem.current.currentSelectedGameObject;

                if (_selectedMonsterLeft != null)
                {
                    _selectedMonsterLeft.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_selectedMonsterLeft);
                }
                if (_selectedMonsterMiddle != null)
                {
                    _selectedMonsterMiddle.SetActive(true);

                    if (_selectedMonsterLeft == null)
                    {
                        EventSystem.current.SetSelectedGameObject(_selectedMonsterMiddle);
                    }
                }
                if (_selectedMonsterRight != null)
                {
                    _selectedMonsterRight.SetActive(true);

                    if (_selectedMonsterLeft == null)
                    {
                        EventSystem.current.SetSelectedGameObject(_selectedMonsterMiddle);
                    }
                }

                
            }
            /*if (Input.GetKeyDown(KeyCode.Return))
            {
                for (int i = 0; i < _selectedLimb.Count; i++)
                {
                    _selectedLimb[i].SetActive(true);
                }

                EventSystem.current.SetSelectedGameObject(_selectedLimb[0]);

                if (_monsterLimbsUIActive == false)
                {
                    Debug.Log("ActiveUI");
                    _monsterLimbsUIActive = true;
                    StartCoroutine(MonsterLimbsUI());
                }
            }*/
        }
        //Show special actions options
        if (EventSystem.current.currentSelectedGameObject == _specialActionsOption)
        {
            for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
            {
                _selectedLimbMiddleMonster[i].SetActive(false);
            }
        }

        //Show confirmation option to run
        if (EventSystem.current.currentSelectedGameObject == _runOption)
        {
            for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
            {
                _selectedLimbMiddleMonster[i].SetActive(false);
            }
        }
    }

    private IEnumerator MonsterLimbDisplay()
    {

    }
    private IEnumerator SelectedMonsterLimbs()
    {
        yield return new WaitForSeconds(_waitTime);

        Debug.Log("MonsterLimbUIActive");
        //GapingHoleMonster Selection
        while (_gapingHoleMonster != null)
        {
            if (EventSystem.current.currentSelectedGameObject == _selectedLimbMiddleMonster[0])
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (_gapingHoleMonster.LeftLegHealth <= 0)
                    {
                        _dialogueTypingManager.StopDialogue();
                        _text = "This part has already been destroyed";
                        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    }
                    else
                    {
                        _dialogueTypingManager.StopDialogue();
                        StartCoroutine(_gapingHoleMonster.LeftLegDamaged());

                        _battleState = BattleState.MonsterTurn;
                        _monsterLimbsUIActive = false;
                        StartCoroutine(HandleState());
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                }
            }
            else if (EventSystem.current.currentSelectedGameObject == _selectedLimbMiddleMonster[1])
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (_gapingHoleMonster.RightLegHealth <= 0)
                    {
                        _dialogueTypingManager.StopDialogue();
                        _text = "This part has already been destroyed";
                        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    }
                    else
                    {
                        _dialogueTypingManager.StopDialogue();
                        StartCoroutine(_gapingHoleMonster.RightLegDamaged());

                        _battleState = BattleState.MonsterTurn;
                        _monsterLimbsUIActive = false;
                        StartCoroutine(HandleState());
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                }
            }
            else if (EventSystem.current.currentSelectedGameObject == _selectedLimbMiddleMonster[2])
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (_gapingHoleMonster.HeadHealth <= 0)
                    {
                        _dialogueTypingManager.StopDialogue();
                        _text = "This part has already been destroyed";
                        _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                    }
                    else
                    {
                        _dialogueTypingManager.StopDialogue();
                        StartCoroutine(_gapingHoleMonster.HeadDamaged());

                        _battleState = BattleState.MonsterTurn;
                        _monsterLimbsUIActive = false;
                        StartCoroutine(HandleState());
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                }
            }
            yield return null;
        }
    }
}
