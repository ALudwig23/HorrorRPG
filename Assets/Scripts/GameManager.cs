using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentRoomScene = 1;
    private int _combatScene = 0;

    //Scripts reference
    [SerializeField] private CheckPlayerCollision CheckPlayerCollision;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private BattleManager BattleManager;

    private GameManager m_Instance = null;
    public GameManager Instance
    {
        get
        {
            //Looks for Instance GameManager as long as m_Instance is null
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();

                //Create a new GameManger if a GameManager could not be found
                if (m_Instance != null)
                {
                    GameObject go = new GameObject("GameManger");
                    go.AddComponent<GameManager>();
                }
                //Make sure the same GameManger is kept throughout the entire game
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    private void FixedUpdate()
    {
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
        PlayerMovement = FindObjectOfType<PlayerMovement>();

        //Assign CheckPlayerCollision to the script after pressing play
        CheckPlayerCollision = FindObjectOfType<CheckPlayerCollision>();
    }

    //Manager for in game scenes
    private void InGameScenesManager()
    {
        if (CheckPlayerCollision == null)
            return;

        //Check if player collided with enemy
        if (CheckPlayerCollision.CollidedWithMonster == true)
        {
            Debug.Log("Loading Combat Scene");
            SceneManager.LoadScene(_combatScene);
        }

        //Exit combat and return player to current room
        if (BattleManager != null)
        {
            if (BattleManager.BattleWon == true)
            {
                SceneManager.LoadScene(_currentRoomScene);
            }
        }
        
    }

}
