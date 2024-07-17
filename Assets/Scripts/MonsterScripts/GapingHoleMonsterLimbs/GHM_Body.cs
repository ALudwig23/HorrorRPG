using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHM_Body : MonoBehaviour
{
    private bool _targetingBody = false;
    private bool _targetedBody = false;
    private bool _prioritizedBody = false;

    public bool TargetedBody
    {
        get { return _targetedBody; }
        set { _targetedBody = value; }
    }

    [SerializeField] private GHM_Head _ghmHead;
    [SerializeField] private GHM_LeftLeg _ghmLeftLeg;
    [SerializeField] private GHM_RightLeg _ghmRightLeg;
    [SerializeField] private CursorMovement _cursorMovement;

    private void Start()
    {
        _cursorMovement = FindObjectOfType<CursorMovement>();
    }

    private void Update()
    {
        if (_targetingBody == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_ghmHead.TargetedHead == false && _ghmLeftLeg.TargetedLeftLeg == false && _ghmRightLeg.TargetedRightLeg == false)
                {
                    Debug.Log("Attacked Body");
                    _targetedBody = true;
                }
                else if (_prioritizedBody == true)
                {
                    _targetedBody= true;
                }
                else
                {
                    _targetedBody = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Body");
            _targetingBody = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Body");
            _targetingBody = true;
            _prioritizedBody = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Body");
            _targetingBody= false;
            _targetedBody = false;
        }
        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Body");
            _targetingBody = false;
            _targetedBody = false;
            _prioritizedBody = false;
        }
    }
}
