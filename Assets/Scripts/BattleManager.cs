using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { BattleStart, PlayerTurn, EnemyTurn, BattleWon, BattleLost }
public class BattleManager : MonoBehaviour
{
    private int _numOfEnemies;
    private bool _battleWon;
    public bool BattleWon
    {
        get { return _battleWon; }
    }

    [SerializeField] private GameObject EnemyPrefab1;
    [SerializeField] private GameObject EnemyPrefab2;
    [SerializeField] private GameObject EnemyPrefab3;

    public Transform EnemyPos1;
    public Transform EnemyPos2;
    public Transform EnemyPos3;

    private MonsterInfo _monsterInfo1;
    private MonsterInfo _monsterInfo2;
    private MonsterInfo _monsterInfo3;
    private BattleState State;

    private void Start()
    {
        State = BattleState.BattleStart;
        EnemyPrefab1 = GameObject.FindWithTag("Enemy1");
        BattleSetup();
    }

    private void BattleSetup()
    {
        if (State != BattleState.BattleStart)
            return;

        //**Placeholder** Randomizes amount of enemies in an encounter 1 - 3 (depends on room level)
        _numOfEnemies = Random.Range (1, 4);

        //Spawn enemies in suitable positions based on enemy count
        switch (_numOfEnemies)
        {
            case 1:

                EnemyPos1 = GameObject.FindWithTag("Pos3").transform;
                break;

            case 2:

                EnemyPos1 = GameObject.FindWithTag("Pos2").transform;
                EnemyPos2 = GameObject.FindWithTag("Pos4").transform;
                break;

            case 3:

                EnemyPos1 = GameObject.FindWithTag("Pos1").transform;
                EnemyPos2 = GameObject.FindWithTag("Pos3").transform;
                EnemyPos3 = GameObject.FindWithTag("Pos5").transform;
                break;
        }

        //Spawn enemies based on enemy count
        if (EnemyPrefab1 != null)
        {
            GameObject enemy1GO = Instantiate(EnemyPrefab1, EnemyPos1);
            enemy1GO.GetComponent<MonsterInfo>();
        }
       
        if (EnemyPrefab2 != null)
        {
            Instantiate(EnemyPrefab2, EnemyPos2);
        }
        
        if (EnemyPrefab3 != null)
        {
            Instantiate(EnemyPrefab3, EnemyPos3);
        } 

    }

    //Keeps track of turns and how battle ended
    private void BattleProgress()
    {
        if (State == BattleState.BattleWon)
        {
            _battleWon = true;
        }

        if (State == BattleState.BattleLost)
        {
            _battleWon = false;
        }
    }
}
