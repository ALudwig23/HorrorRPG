using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private enum BattleState { PlayerTurn, MonsterTurn, BattleEnd }
    [SerializeField] private BattleState _battleState;

    private float _miniTimer = 1f;
    private float _waitTime = 0.1f;
    private float _enemyCount;
    private float _enemyTypeSpawnChance;
    private bool _battleWon;
    private bool _monsterLimbsDisplay = false;
    private bool _monsterLimbsSelected = false;
    private string _text;
    private string _leftMonster;
    private string _middleMonster;
    private string _rightMonster;

    public bool BattleWon
    {
        get { return _battleWon; }
    }

    [SerializeField] private TMP_FontAsset _fontAsset;
    [SerializeField] private Sprite _buttonSprite;
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
    [SerializeField] private SpiderMonster _spiderMonster;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DialogueTypingManager _dialogueTypingManager;
    [SerializeField] private Coroutine _coroutine;
    [SerializeField] private GameManager _gameManager;


    void Start()
    {
        _battleState = BattleState.PlayerTurn;
        _buttonSprite = Resources.Load<Sprite>("Unselected");
        _playerStats = Resources.Load<PlayerStats>("PlayerStatsData");
        _gameManager = FindObjectOfType<GameManager>();

        _playerHealth.text = $"HP: {(int)_playerStats.CurrentHealth} / {(int)_playerStats.MaxHealth}";
        _playerSanity.text = $"Sanity: {(int)_playerStats.CurrentSanity} / {(int)_playerStats.MaxSanity}";

        StartCoroutine(MonsterSetup());
        StartCoroutine(HandleState());
    }

    void Update()
    {
        OptionSelection();
    }

    private void FixedUpdate()
    {
        Debug.Log(EventSystem.current);
        
    }

    private void StartTimer()
    {
        _miniTimer -= Time.deltaTime;
    }

    private void CheckSanity()
    {
        if (_playerStats.CurrentSanity <= _playerStats.MaxSanity / 4)
        {
            _playerStats.CurrentHealth -= 5f;
        }
    }

    private void MoreThanOneEnemy()
    {
        if (_enemyCount == 2)
        {
            _enemyTypeSpawnChance = Random.Range(0, 100);
        }
    }

    private IEnumerator MonsterSetup()
    {
        _enemyCount = Random.Range(1, 4);
        _middleMonster = _gameManager.CollidedMonsterType;

        ColorBlock buttonSelectedColour;

        switch (_middleMonster)
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

                for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
                {
                    _selectedLimbMiddleMonster[i].SetActive(false);
                }

                //Configure button image and colour
                //Left Leg button
                Button leftLegButton = _selectedLimbMiddleMonster[0].GetComponent<Button>();
                Image leftLegButtonImage = _selectedLimbMiddleMonster[0].GetComponent<Image>();

                buttonSelectedColour = leftLegButton.colors;
                buttonSelectedColour.selectedColor = Color.red;

                leftLegButton.targetGraphic = leftLegButtonImage;
                leftLegButton.transition = Selectable.Transition.ColorTint;
                leftLegButton.colors = buttonSelectedColour;

                //Right Leg Button
                Button rightLegButton = _selectedLimbMiddleMonster[1].GetComponent<Button>();
                Image rightLegButtonImage = _selectedLimbMiddleMonster[1].GetComponent<Image>();

                rightLegButton.targetGraphic = rightLegButtonImage;
                rightLegButton.transition = Selectable.Transition.ColorTint;
                rightLegButton.colors = buttonSelectedColour;

                //Head Button
                Button headButton = _selectedLimbMiddleMonster[2].GetComponent<Button>();
                Image headButtonImage = _selectedLimbMiddleMonster[2].GetComponent<Image>();

                headButton.targetGraphic = headButtonImage;
                headButton.transition = Selectable.Transition.ColorTint;
                headButton.colors = buttonSelectedColour;

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

                break;

            case "SpiderMonster":

                _monsterPrefab = Resources.Load<GameObject>("SpiderMonster");

                GameObject spiderMonster = Instantiate(_monsterPrefab);
                spiderMonster.transform.SetParent(_spawnPositionMid.transform);

                _spiderMonster = FindObjectOfType<SpiderMonster>();
                yield return new WaitForSeconds(0.1f);

                _spiderMonster.CreateLimbTarget();

                break;
        }

        //Enemy Selection Button
        //Middle Enemy Select
        _selectedMonsterMiddle = new GameObject("MiddleMonsterSelect");
        _selectedMonsterMiddle.transform.SetParent(_canvas.transform, false);

        RectTransform middleSelectRect = _selectedMonsterMiddle.AddComponent<RectTransform>();
        middleSelectRect.anchoredPosition = new Vector2(60f, -145.5f);
        middleSelectRect.sizeDelta = new Vector2(115f, 147f);

        Button middleSelectButton = _selectedMonsterMiddle.AddComponent<Button>();
        Image middleSelectSprite = _selectedMonsterMiddle.AddComponent<Image>();
        middleSelectSprite.sprite = _buttonSprite;

        buttonSelectedColour = middleSelectButton.colors;
        buttonSelectedColour.selectedColor = Color.red;

        middleSelectButton.targetGraphic = middleSelectSprite;
        middleSelectButton.transition = Selectable.Transition.ColorTint;
        middleSelectButton.colors = buttonSelectedColour;

        GameObject middleSelectChild = new GameObject("MiddleSelectChild");
        middleSelectChild.transform.SetParent(_selectedMonsterMiddle.transform);

        TMP_Text middleSelectText = middleSelectChild.AddComponent<TextMeshProUGUI>();
        middleSelectText.text = "Middle";
        middleSelectText.fontSize = 24f;
        middleSelectText.font = _fontAsset;
        middleSelectText.color = Color.black;
        middleSelectText.alignment = TextAlignmentOptions.Center;

        RectTransform middleSelectTextRect = middleSelectText.GetComponent<RectTransform>();
        middleSelectTextRect.position = middleSelectRect.position;
        middleSelectTextRect.sizeDelta = middleSelectRect.sizeDelta;
        middleSelectTextRect.localScale = middleSelectRect.localScale;

        Navigation middleSelectButtonNavigation = middleSelectButton.navigation;
        middleSelectButtonNavigation.mode = Navigation.Mode.Explicit;
        middleSelectButton.navigation = middleSelectButtonNavigation;
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
                        _playerHealth.text = $"HP: {(int)_playerStats.CurrentHealth} / {(int)_playerStats.MaxHealth}";
                        _playerSanity.text = $"Sanity: {(int)_playerStats.CurrentSanity} / {(int)_playerStats.MaxSanity}";
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

    private void OptionSelection()
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
                _selectedMonsterMiddle.SetActive(true);
            }
            if (_selectedMonsterRight != null)
            {
                _selectedMonsterRight.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (_selectedMonsterLeft != null)
                {
                    EventSystem.current.SetSelectedGameObject(_selectedMonsterLeft);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(_selectedMonsterMiddle);
                }

                if (_monsterLimbsDisplay == false)
                {
                    Debug.Log("ActiveUI");
                    _monsterLimbsDisplay = true;
                    StartCoroutine(SelectedMonster());
                }
                
            }
        }
        //Show special actions options
        if (EventSystem.current.currentSelectedGameObject == _specialActionsOption)
        {
            if (_selectedMonsterLeft != null)
            {
                _selectedMonsterLeft.SetActive(false);
            }
            if (_selectedMonsterMiddle != null)
            {
                _selectedMonsterMiddle.SetActive(false);
            }
            if (_selectedMonsterRight != null)
            {
                _selectedMonsterRight.SetActive(false);
            }
        }

        //Show confirmation option to run
        if (EventSystem.current.currentSelectedGameObject == _runOption)
        {
            if (_selectedMonsterLeft != null)
            {
                _selectedMonsterLeft.SetActive(false);
            }
            if (_selectedMonsterMiddle != null)
            {
                _selectedMonsterMiddle.SetActive(false);
            }
            if (_selectedMonsterRight != null)
            {
                _selectedMonsterRight.SetActive(false);
            }
        }
    }

    //Select monster to attack
    private IEnumerator SelectedMonster()
    {
        yield return new WaitForSeconds(_waitTime);

        //Reactivate the selections when returning from later selection
        if (_selectedMonsterLeft != null)
        {
            _selectedMonsterLeft.SetActive(true);
        }
        if (_selectedMonsterMiddle != null)
        {
            _selectedMonsterMiddle.SetActive(true);
        }
        if (_selectedMonsterRight != null)
        {
            _selectedMonsterRight.SetActive(true);
        }

        while (EventSystem.current.currentSelectedGameObject == _selectedMonsterLeft || EventSystem.current.currentSelectedGameObject == _selectedMonsterMiddle || EventSystem.current.currentSelectedGameObject == _selectedMonsterRight)
        {
            if (EventSystem.current.currentSelectedGameObject == _selectedMonsterLeft)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _previousSelection = EventSystem.current.currentSelectedGameObject;

                    if (_selectedMonsterLeft != null)
                    {
                        _selectedMonsterLeft.SetActive(false);
                    }
                    if (_selectedMonsterMiddle != null)
                    {
                        _selectedMonsterMiddle.SetActive(false);
                    }
                    if (_selectedMonsterRight != null)
                    {
                        _selectedMonsterRight.SetActive(false);
                    }

                    for (int i = 0; i < _selectedLimbLeftMonster.Count; i++)
                    {
                        _selectedLimbLeftMonster[i].SetActive(true);
                    }

                    EventSystem.current.SetSelectedGameObject(_selectedLimbLeftMonster[0]);

                    if (_monsterLimbsSelected == false)
                    {
                        Debug.Log("ActiveUI");
                        _monsterLimbsSelected = true;
                        StartCoroutine(SelectedMonsterLimbs());
                    }

                    _monsterLimbsDisplay = false;
                }
            }

            else if (EventSystem.current.currentSelectedGameObject == _selectedMonsterMiddle)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _previousSelection = EventSystem.current.currentSelectedGameObject;
;
                    if (_selectedMonsterLeft != null)
                    {
                        _selectedMonsterLeft.SetActive(false);
                    }
                    if (_selectedMonsterMiddle != null)
                    {
                        _selectedMonsterMiddle.SetActive(false);
                    }
                    if (_selectedMonsterRight != null)
                    {
                        _selectedMonsterRight.SetActive(false);
                    }

                    for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
                    {
                        _selectedLimbMiddleMonster[i].SetActive(true);
                    }

                    EventSystem.current.SetSelectedGameObject(_selectedLimbMiddleMonster[0]);

                    if (_monsterLimbsSelected == false)
                    {
                        Debug.Log("ActiveUI");
                        _monsterLimbsSelected = true;
                        StartCoroutine(SelectedMonsterLimbs());
                    }

                    _monsterLimbsDisplay = false;
                }
            }

            else if (EventSystem.current.currentSelectedGameObject == _selectedMonsterRight)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _previousSelection = EventSystem.current.currentSelectedGameObject;

                    if (_selectedMonsterLeft != null)
                    {
                        _selectedMonsterLeft.SetActive(false);
                    }
                    if (_selectedMonsterMiddle != null)
                    {
                        _selectedMonsterMiddle.SetActive(false);
                    }
                    if (_selectedMonsterRight != null)
                    {
                        _selectedMonsterRight.SetActive(false);
                    }

                    for (int i = 0; i < _selectedLimbRightMonster.Count; i++)
                    {
                        _selectedLimbRightMonster[i].SetActive(true);
                    }

                    EventSystem.current.SetSelectedGameObject(_selectedLimbRightMonster[0]);

                    if (_monsterLimbsSelected == false)
                    {
                        Debug.Log("ActiveUI");
                        _monsterLimbsSelected = true;
                        StartCoroutine(SelectedMonsterLimbs());
                    }

                    _monsterLimbsDisplay = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                EventSystem.current.SetSelectedGameObject(_fightOption);
                _monsterLimbsDisplay = false;
            }

            yield return null;
        }

        
    }

    //Limbs for respective monsters
    private IEnumerator SelectedMonsterLimbs()
    {
        yield return new WaitForSeconds(_waitTime);

        while (_selectedMonsterLeft != null)
        {
            switch (_leftMonster)
            {
                case "":
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                EventSystem.current.SetSelectedGameObject(_previousSelection);
                _monsterLimbsSelected = false;
            }

            yield return null;
        }

        while (_selectedMonsterMiddle != null)
        {
            switch (_middleMonster)
            {
                case "GapingHoleMonster":

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
                                _monsterLimbsSelected = false;
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
                                _monsterLimbsSelected = false;
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
                                _monsterLimbsSelected = false;
                                StartCoroutine(HandleState());
                                EventSystem.current.SetSelectedGameObject(null);
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        for (int i = 0; i < _selectedLimbMiddleMonster.Count; i++)
                        {
                            _selectedLimbMiddleMonster[i].SetActive(false);
                        }

                        EventSystem.current.SetSelectedGameObject(_previousSelection);
                        _monsterLimbsSelected = false;
                        StartCoroutine(SelectedMonster());
                    }

                    break;
            }

            while (_selectedMonsterRight != null)
            {
                switch (_rightMonster)
                {
                    case "":
                        break;
                }

                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    EventSystem.current.SetSelectedGameObject(_previousSelection);
                    _monsterLimbsSelected = false;
                }
            }

            yield return null;
        }
    }
}
