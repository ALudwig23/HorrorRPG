using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{
    private enum BattleState { PlayerTurn, MonsterTurn, BattleEnd}
    [SerializeField] private BattleState _battleState;
    private bool _battleWon;
    public bool BattleWon
    {
        get { return _battleWon; }
    }
    //Spawn Position
    [SerializeField] private GameObject _spawnPositionMid;
    [SerializeField] private GameObject _spawnPositionRight;
    [SerializeField] private GameObject _spawnPositionLeft;

    //Battle Option Buttons
    [SerializeField] private GameObject _fightButton;
    [SerializeField] private GameObject _specialActionsButton;
    [SerializeField] private GameObject _runButton;

    //Fight Options
    [SerializeField] private GameObject _selectedMonster;
    [SerializeField] private List<GameObject> _selectedLimb;

    //Display UI
    [SerializeField] private GameObject _displayOptionBox;
    [SerializeField] private GameObject _playerStatus;
    [SerializeField] private GameObject _statusEffect;
    [SerializeField] private GameObject _sanityBar;
    [SerializeField] private GameObject _dialogueBox;

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

        MonsterSetup();
        StartCoroutine(HandleState());
    }

    void Update()
    {
        if (_battleState == BattleState.PlayerTurn)
        {
            SelectionUI();
        }
        
    }

    private void MonsterSetup()
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

                _fightButton.SetActive(true);
                _specialActionsButton.SetActive(true);
                _runButton.SetActive(true);
                _displayOptionBox.SetActive(true);

                break;

            case BattleState.MonsterTurn:

                _fightButton.SetActive(false); 
                _specialActionsButton.SetActive(false);
                _runButton.SetActive(false);
                _displayOptionBox.SetActive(false);
                
                _coroutine = CoroutineHost.Instance.StartCoroutine(_gapingHoleMonster.MovesetHandler());
                yield return new WaitUntil(() => _gapingHoleMonster.FinishedDialogue == true);
                
                _battleState = BattleState.PlayerTurn;
                StopCoroutine(HandleState());
                StartCoroutine(HandleState());
                break;

            case BattleState.BattleEnd:

                _battleWon = true;
                break;
        }
    }

    private void SelectionUI()
    {
        if (EventSystem.current == _fightButton)
        {

        }

        if (EventSystem.current == _specialActionsButton)
        {

        }

        if (EventSystem.current == _runButton)
        {

        }

    }
}
