using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{
    private string _collidedMonsterType = "none";
    private bool _collidedWithMonster;
    public bool CollidedWithMonster
    {
        get { return _collidedWithMonster; }
    }
    public string CollidedMonsterType
    {
        get { return _collidedMonsterType; }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check collision with monster and check what monster it is
        if (collision.gameObject.CompareTag("GapingHoleMonster"))
        {
            _collidedMonsterType = "GapingHoleMonster";
            _collidedWithMonster = true;
        }

        if (collision.gameObject.CompareTag("BoneSpider"))
        {
            _collidedMonsterType = "BoneSpider";
            _collidedWithMonster = true;
        }

        if (collision.gameObject.CompareTag("Monster3"))
        {
            _collidedMonsterType = "Monster3";
            _collidedWithMonster = true;
        }

        Debug.Log($"Collided with monster {CollidedWithMonster}");
        
        //if (collision.gameObject.CompareTag("Button"))
        //{
        //    _collidedMonsterType = "Button";
        //    _collidedWithMonster = true;
        //    Debug.Log("Collided with button");
        //}
        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Reset values after scene change
        if (collision.gameObject.CompareTag("GapingHoleMonster"))
        {
            _collidedMonsterType = "none";
            _collidedWithMonster = false;
            Debug.Log($"No longer colliding with monster {CollidedWithMonster}");
        }

        if (collision.gameObject.CompareTag("BoneSpider"))
        {
            _collidedMonsterType = "none";
            _collidedWithMonster = false;
        }

        if (collision.gameObject.CompareTag("Monster3"))
        {
            _collidedMonsterType = "none";
            _collidedWithMonster = false;
        }

        //if (collision.gameObject.CompareTag("Button"))
        //{
        //    _collidedMonsterType = "none";
        //    _collidedWithMonster = false;
        //}
    }
}
