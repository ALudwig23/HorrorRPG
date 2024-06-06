using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int CurrentRoomScene = 1;
    private int CombatScene = 0;

    //Scripts reference
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private InCombat InCombat;

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
        if (PlayerMovement == null )
        {
            PlayerMovement = FindObjectOfType<PlayerMovement>();
        }

        InGameScenesManager();
    }

    //Function for starting the game (used for start button)
    public void StartGame()
    {
        //Check if m_Instance is null or not on Scene 1 (Main Menu)
        if (Instance == null || Instance.CurrentRoomScene != 1)
            return;

        Instance.CurrentRoomScene = 2;
        PlayerMovement.CanMove = true;
        SceneManager.LoadScene(CurrentRoomScene);
    }

    //Manager for in game scenes
    private void InGameScenesManager()
    {
        if (PlayerMovement == null)
            return;

        //Check if player collided with enemy
        if (PlayerMovement.CollidedWithEnemy == true)
        {
            PlayerMovement.CollidedWithEnemy = false;
            SceneManager.LoadScene(CombatScene);
        }

        if (InCombat.BattleEnd == true)
        {
            SceneManager.LoadScene(CurrentRoomScene);
        }
    }

}
