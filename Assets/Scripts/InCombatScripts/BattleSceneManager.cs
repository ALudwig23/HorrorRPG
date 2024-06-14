using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum BattleState { BattleStart, PlayerTurn, EnemyTurn, BattleWon, BattleLost }
    public bool BattleWon { get { return _battleWon; } }

    private int _numOfEnemies;
    private bool _battleWon;
    
    private GameObject _enemy1;
    private GameObject _enemy2;
    private GameObject _enemy3;
    private Sprite _enemySprite1;
    private Sprite _enemySprite2;
    private Sprite _enemySprite3;
    private Transform _enemyPos1;
    private Transform _enemyPos2;
    private Transform _enemyPos3;
 
    [SerializeField] private CheckPlayerCollision _checkPlayerCollision;
    [SerializeField] private MonsterManager _monsterManagerInfo;

    [SerializeField] private BattleState _state;

    private void Start()
    {
        _state = BattleState.BattleStart;
        BattleSetup();
    }

    private void BattleSetup()
    {
        if (_state != BattleState.BattleStart)
            return;

        //**Placeholder** Randomizes amount of enemies in an encounter 1 - 3 (depends on room level)
        //_numOfEnemies = Random.Range (1, 4);
        _numOfEnemies = 1;

        //Spawn enemies in suitable positions based on enemy count
        switch (_numOfEnemies)
        {
            case 1:
           
                _enemyPos1 = GameObject.FindWithTag("Pos3").transform;
                break;

            case 2:

                _enemyPos1 = GameObject.FindWithTag("Pos2").transform;
                _enemyPos2 = GameObject.FindWithTag("Pos4").transform;
                break;

            case 3:

                _enemyPos1 = GameObject.FindWithTag("Pos1").transform;
                _enemyPos2 = GameObject.FindWithTag("Pos3").transform;
                _enemyPos3 = GameObject.FindWithTag("Pos5").transform;
                break;
        }

        //Spawn enemies based on enemy count
        if (_enemy1 != null)
        {
            GameObject enemy1GO = Instantiate(_enemy1, _enemyPos1);
            enemy1GO.AddComponent<SpriteRenderer>().sprite = _enemySprite1;
        }
       
        if (_enemy2 != null)
        {
            Instantiate(_enemySprite2, _enemyPos2);
        }
        
        if (_enemy3 != null)
        {
            Instantiate(_enemySprite3, _enemyPos3);
        } 
    }

    //Keeps track of turns and how battle ended
    private void BattleProgress()
    {
        if (_state == BattleState.PlayerTurn)
        {
            //Playerui active

        }

        if (_state == BattleState.EnemyTurn)
        {
            //playerui inactive, enemy attacks, dialougue explains, cooldowns in between text
        }

        if (_state == BattleState.BattleWon)
        {
            _battleWon = true;
        }

        if (_state == BattleState.BattleLost)
        {
            _battleWon = true;
        }
    }
}
