using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMonster : MonoBehaviour
{
    private float _maxHealth = 200f;
    private float _currentHealth = 200f;
    private float _damage = 20f;
    private int _movesRandomizer;
    private string _text;
    private bool _finishedDialogue = false;
    private bool _damageDealt = false;
    private bool _monsterDied = false;
    private bool _leftLegDestroyed = false;
    private bool _rightLegDestroyed = false;
    private bool _headDestroyed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CreateLimbTarget()
    {
        yield return null;
    }
}
