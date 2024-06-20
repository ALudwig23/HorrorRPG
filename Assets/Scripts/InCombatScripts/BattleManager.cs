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
    private bool _battleWon;
    private string _text;
    public bool BattleWon
    {
        get { return _battleWon; }
    }
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
    [SerializeField] private GameObject _selectedMonster;
    [SerializeField] private List<GameObject> _selectedLimb;

    //Display UI
    [SerializeField] private GameObject _displayOptionBox;
    [SerializeField] private GameObject _playerStatus;
    [SerializeField] private GameObject _statusEffect;
    [SerializeField] private GameObject _sanityBar;
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

        StartCoroutine(MonsterSetup());
        StartCoroutine(HandleState());
    }

    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(EventSystem.current);
        if (_battleState == BattleState.PlayerTurn)
        {
            SelectionUI();
        }

        if (_spawnPositionMid == null)
        {
            _battleState = BattleState.BattleEnd;
            StartCoroutine(MonsterSetup());
        }
    }

    private IEnumerator MonsterSetup()
    {
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

                _selectedLimb.Add(_gapingHoleMonster.LeftLimbSelection);
                _selectedLimb.Add(_gapingHoleMonster.RightLimbSelection);
                _selectedLimb.Add(_gapingHoleMonster.HeadLimbSelection);

                //Configure button image and colour
                //Left Leg button
                Button leftLegButton = _selectedLimb[0].GetComponent<Button>();
                Image leftLegButtonImage = _selectedLimb[0].GetComponent<Image>();
                ColorBlock selectedColour = leftLegButton.colors; //Main colour for selection

                leftLegButton.targetGraphic = leftLegButtonImage;
                leftLegButton.transition = Selectable.Transition.ColorTint;
                selectedColour.selectedColor = Color.red;
                leftLegButton.colors = selectedColour;

                //Right Leg Button
                Button rightLegButton = _selectedLimb[1].GetComponent<Button>();
                Image rightLegButtonImage = _selectedLimb[1].GetComponent<Image>();

                rightLegButton.targetGraphic = rightLegButtonImage;
                rightLegButton.transition = Selectable.Transition.ColorTint;
                rightLegButton.colors = selectedColour;

                //Head Button
                Button headButton = _selectedLimb[2].GetComponent<Button>();
                Image headButtonImage = _selectedLimb[2].GetComponent<Image>();

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
        yield return new WaitForSeconds(0.1f);

        switch (_battleState)
        {
            case BattleState.PlayerTurn:

                _fightOption.SetActive(true);
                _specialActionsOption.SetActive(true);
                _runOption.SetActive(true);
                _displayOptionBox.SetActive(true);

                EventSystem.current.SetSelectedGameObject(_fightOption);
                break;

            case BattleState.MonsterTurn:

                _fightOption.SetActive(false);
                _specialActionsOption.SetActive(false);
                _runOption.SetActive(false);
                _displayOptionBox.SetActive(false);
                
                if (_gapingHoleMonster != null)
                {
                    _coroutine = CoroutineHost.Instance.StartCoroutine(_gapingHoleMonster.MovesetHandler());
                    yield return new WaitUntil(() => _gapingHoleMonster.FinishedDialogue == true);
                }
                  
                _battleState = BattleState.PlayerTurn;
                StopCoroutine(HandleState());
                StartCoroutine(HandleState());
                break;

            case BattleState.BattleEnd:

                if (_playerStats.CurrentHealth > 0)
                {
                    _battleWon = true;
                }

                if (_playerStats.CurrentHealth < 0)
                {
                    _battleWon = false;
                }
                break;

        }
    }

    private void SelectionUI()
    {
        //Show fight options
        if (EventSystem.current.currentSelectedGameObject == _fightOption)
        {
            for (int i = 0; i < _selectedLimb.Count; i++)
            {
                _selectedLimb[i].SetActive(true);
            }

            if (Input.GetKey(KeyCode.Return))
            {
                EventSystem.current.SetSelectedGameObject(_selectedLimb[0]);
            }
        }

        //Limb Selection Function
        if (_gapingHoleMonster != null)
        {
            if (EventSystem.current.currentSelectedGameObject == _selectedLimb[0])
            {
                if (Input.GetKey(KeyCode.Return) && _gapingHoleMonster.LeftLegHealth <= 0)
                {
                    _text = "This part has already been destroyed";
                    _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                }
                else if (Input.GetKey(KeyCode.Return))
                {
                    StartCoroutine(_gapingHoleMonster.LeftLegDamaged());
                }
            }
        }

        //Show special actions options
        if (EventSystem.current.currentSelectedGameObject == _specialActionsOption)
        {
            for (int i = 0; i < _selectedLimb.Count; i++)
            {
                _selectedLimb[i].SetActive(false);
            }
        }

        //Show confirmation option to run
        if (EventSystem.current.currentSelectedGameObject == _runOption)
        {
            for (int i = 0; i < _selectedLimb.Count; i++)
            {
                _selectedLimb[i].SetActive(false);
            }
        }

    }

}
