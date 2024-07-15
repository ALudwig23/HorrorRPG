using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentRoomScene = 0;
    private int _combatScene = 1;
    private float _waitTime = 0.5f;
    private string _collidedMonsterType;

    public string CollidedMonsterType
    {
        get { return _collidedMonsterType; }
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _inGameMenu;

    //Scripts reference
    [SerializeField] private CheckPlayerCollision _checkPlayerCollision;
    [SerializeField] private TeleportTrigger _teleportTrigger;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private HandleInGameMenu _handleInGameMenu;

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            //Looks for Instance GameManager as long as m_Instance is null
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                //Create a new GameManger if a GameManager could not be found
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManger");
                    go.AddComponent<GameManager>();
                }
                //Make sure the same GameManger is kept throughout the entire game
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Update()
    {
        CollisionHandler();

        if (_waitTime >= 0f)
        {
            _waitTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.I) && _waitTime <= 0f)
        {
            HandleInGameMenu();
            _waitTime = 1f;
        }
    }

    private void FixedUpdate()
    {
        InGameScenesManager();
    }

    //Function for starting the game (used for start button)
    public void StartGame()
    {
        //Check if m_Instance is null or not on Scene 0 (Main Menu)
        if (Instance == null || Instance._currentRoomScene != 0)
            return;

        Instance._currentRoomScene = 2;
        SceneManager.LoadScene(_currentRoomScene);

        //Assign PlayerMovement to the script after pressing play
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    //Simple quit game function
    public void QuitGame()
    {
        Application.Quit();
    }

    private void InGameScenesManager()
    {
        //Check if player collided with enemy
        if (_checkPlayerCollision != null)
        {
            if (_checkPlayerCollision.CollidedWithMonster == true)
            {
                Debug.Log("Loading Combat Scene");
                SceneManager.LoadScene(Instance._combatScene);
            }
        }
        
        //Debug.Log($"Current Room = {Instance._currentRoomScene}");
        //Exit combat and return player to current room
        if (_battleManager == null)
        {
            _battleManager = FindObjectOfType<BattleManager>();
        }
        if (_battleManager == null)
            return;

        if (_battleManager.BattleWon == true)
        {
            SceneManager.LoadScene(Instance._currentRoomScene);
        }

    }

    public void CollisionHandler()
    {
        if (_checkPlayerCollision == null)
        {
            _checkPlayerCollision = FindObjectOfType<CheckPlayerCollision>();
        }
        
        if (_teleportTrigger == null)
        {
            _teleportTrigger = FindObjectOfType<TeleportTrigger>();
        }

        if (_checkPlayerCollision != null)
        {
            _collidedMonsterType = _checkPlayerCollision.CollidedMonsterType;
        }
        
        if (_teleportTrigger != null && _teleportTrigger.IsTeleporting == true)
        {
            Instance._currentRoomScene = _teleportTrigger.LoadScene;
        }
    } 

    public void HandleInGameMenu()
    { 
        if (_handleInGameMenu == null)
        {
            _handleInGameMenu = new HandleInGameMenu();
        }
        if (_handleInGameMenu == null)
            return;

        if (_canvas == null)
        {
            _canvas = FindObjectOfType<Canvas>();

            Transform inGameMenuTransform = _canvas.transform.Find("InGameMenu");
            _inGameMenu = inGameMenuTransform.gameObject;
        }
        if (_inGameMenu == null)
            return;

        Debug.Log("works");

        _handleInGameMenu.OpenOrCloseInGameMenu(_inGameMenu);
    }

    public void SaveGameState()
    {
        SaveData.SavePlayerStats(_playerStats);

    }

    public void LoadGameState()
    {

    }
}
