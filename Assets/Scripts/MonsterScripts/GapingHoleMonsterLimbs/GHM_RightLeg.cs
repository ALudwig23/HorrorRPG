using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHM_RightLeg : MonoBehaviour
{
    private bool _targetingRightLeg = false;
    private bool _targetedRightLeg = false;
    private bool _prioritizedRightLeg = false;
    public bool TargetedRightLeg
    {
        get { return _targetedRightLeg; }
        set { _targetedRightLeg = value;}
    }

    [SerializeField] private GHM_Head _ghmHead;
    [SerializeField] private GHM_Body _ghmBody;
    [SerializeField] private GHM_LeftLeg _ghmLeftLeg;
    [SerializeField] private CursorMovement _cursorMovement;

    private void Start()
    {
        _cursorMovement = FindObjectOfType<CursorMovement>();
    }

    private void Update()
    {
        if (_targetingRightLeg == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_ghmHead.TargetedHead == false && _ghmBody.TargetedBody == false && _ghmLeftLeg.TargetedLeftLeg == false)
                {
                    Debug.Log("Attacked Head");
                    _targetedRightLeg = true;
                }
                else if (_prioritizedRightLeg == true)
                {
                    _targetedRightLeg = true;
                }
                else
                {
                    _targetedRightLeg = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Right Leg");
            _targetingRightLeg = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Right Leg");
            _targetingRightLeg = true;
            _prioritizedRightLeg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Right Leg");
            _targetingRightLeg = false;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Right Leg");
            _targetingRightLeg = false;
            _prioritizedRightLeg = false;
        }
    }
}
