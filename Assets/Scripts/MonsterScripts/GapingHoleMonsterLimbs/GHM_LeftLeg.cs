using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHM_LeftLeg : MonoBehaviour
{
    private bool _targetingLeftLeg = false;
    private bool _targetedLeftLeg = false;
    private bool _prioritizedLeftLeg = false;

    public bool TargetedLeftLeg
    {
        get { return _targetedLeftLeg; }
        set { _targetedLeftLeg = value;}
    }

    [SerializeField] private GHM_Head _ghmHead;
    [SerializeField] private GHM_Body _ghmBody;
    [SerializeField] private GHM_RightLeg _ghmRightLeg;
    [SerializeField] private CursorMovement _cursorMovement;

    private void Start()
    {
        _cursorMovement = FindObjectOfType<CursorMovement>();
    }

    private void Update()
    {
        if (_targetingLeftLeg == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_ghmHead.TargetedHead == false && _ghmBody.TargetedBody == false && _ghmRightLeg.TargetedRightLeg == false)
                {
                    Debug.Log("Attacked Head");
                    _targetedLeftLeg = true;
                }
                else if (_prioritizedLeftLeg == true)
                {
                    _targetedLeftLeg = true;
                }
                else
                {
                    _targetedLeftLeg = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Left Leg");
            _targetingLeftLeg = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Left Leg");
            _targetingLeftLeg = true;
            _prioritizedLeftLeg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Left Leg");
            _targetingLeftLeg = false;
            _targetedLeftLeg = false;
        }
        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Left Leg");
            _targetingLeftLeg = false;
            _targetedLeftLeg = false;
            _prioritizedLeftLeg = false;
        }
    }
}
