using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentRoomScene = 1;
    private int _combatScene = 0;

    [SerializeField] private Camera _playerCamera;

    //Scripts reference
    [SerializeField] private CheckPlayerCollision _checkPlayerCollision;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BattleManager _battleManager;

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

    private void FixedUpdate()
    {
        CameraManager();
        InGameScenesManager();
    }

    //Function for starting the game (used for start button)
    public void StartGame()
    {
        //Check if m_Instance is null or not on Scene 1 (Main Menu)
        if (Instance == null || Instance._currentRoomScene != 1)
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

    private void CameraManager()
    {
        //Only run when not in battle or main menu
        if (Instance._currentRoomScene < 2)
            return;

        if (_playerCamera != null)
            return;

        //Find Camera and add Player Camera script
        _playerCamera = FindObjectOfType<Camera>();

        if (_playerCamera.GetComponent<PlayerCamera>() == null)
        {
            _playerCamera.AddComponent<PlayerCamera>();
        }
    }

    //Manager for in game scenes
    private void InGameScenesManager()
    {
        if (_checkPlayerCollision == null)
        {
            //Assign CheckPlayerCollision to the script
            _checkPlayerCollision = FindObjectOfType<CheckPlayerCollision>();
        }

        if (_checkPlayerCollision == null)
            return;

        //Check if player collided with enemy
        if (_checkPlayerCollision.CollidedWithMonster == true)
        {
            Debug.Log("Loading Combat Scene");
            SceneManager.LoadScene(Instance._combatScene);
        }

        //Exit combat and return player to current room
        if (_battleManager != null)
        {
            if (_battleManager.BattleWon == true)
            {
                SceneManager.LoadScene(Instance._currentRoomScene);
            }
        }
        
    }

}
