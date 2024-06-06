using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { BattleStart, PlayerTurn, EnemyTurn, BattleWon, BattleLost }
public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab1;
    [SerializeField] private GameObject EnemyPrefab2;
    [SerializeField] private GameObject EnemyPrefab3;

    public Transform EnemyPos1;
    public Transform EnemyPos2;
    public Transform EnemyPos3;

    private MonsterInfo MonsterInfo1;
    private MonsterInfo MonsterInfo2;
    private MonsterInfo MonsterInfo3;
    private BattleState State;

    private void Start()
    {
        State = BattleState.BattleStart;
        EnemyPrefab1 = GameObject.FindWithTag("Enemy1");
    }

    private void BattleSetup()
    {
        if (State != BattleState.BattleStart)
            return;

        //Spawn enemies if it exists
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
}
