using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_RightLeg : MonoBehaviour
{
    private bool _targetingRightLeg = false;
    private bool _targetedRightLeg = false;
    private bool _prioritizedRightLeg = false;
    public bool TargetedRightLeg
    {
        get { return _targetedRightLeg; }
        set { _targetedRightLeg = value; }
    }

    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
    [SerializeField] private BS_RightClaw _bs_RightClaw;
    [SerializeField] private BS_LeftLeg _bs_LeftLeg;
    [SerializeField] private Canvas _canvas;
    private CursorMovement _cursorMovement;
    private Transform _pointerTransform;

    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
        _pointerTransform = _canvas.transform.Find("Pointer");
        _cursorMovement = _pointerTransform.GetComponent<CursorMovement>();
    }

    private void Update()
    {
        if (_targetingRightLeg == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                Debug.Log("working");
                if (_bs_Head.TargetedHead == false && _bs_Body.TargetedBody == false && _bs_LeftClaw == false && _bs_RightClaw == false && _bs_LeftLeg == false)
                {
                    Debug.Log("Attacked Right Leg");
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
            _targetedRightLeg = false;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Right Leg");
            _targetingRightLeg = false;
            _targetedRightLeg = false;
            _prioritizedRightLeg = false;
        }
    }
}
