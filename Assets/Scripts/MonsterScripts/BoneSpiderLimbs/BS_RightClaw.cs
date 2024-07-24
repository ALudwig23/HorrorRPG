using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_RightClaw : MonoBehaviour
{
    private bool _targetingRightClaw = false;
    private bool _targetedRightClaw = false;
    private bool _prioritizedRightClaw = false;

    public bool TargetedRightClaw
    {
        get { return _targetedRightClaw; }
        set { _targetedRightClaw = value; }
    }

    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
    [SerializeField] private BS_LeftLeg _bs_LeftLeg;
    [SerializeField] private BS_RightLeg _bs_RightLeg;
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
        if (_targetingRightClaw == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_bs_Head.TargetedHead == false && _bs_Body.TargetedBody == false && _bs_LeftClaw == false && _bs_LeftLeg == false && _bs_RightLeg == false)
                {
                    Debug.Log("Attacked Right Claw");
                    _targetedRightClaw = true;
                }
                else if (_prioritizedRightClaw == true)
                {
                    _targetedRightClaw = true;
                }
                else
                {
                    _targetedRightClaw = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Right Claw");
            _targetingRightClaw = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Right Claw");
            _targetingRightClaw = true;
            _prioritizedRightClaw = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Right Claw");
            _targetingRightClaw = false;
            _targetedRightClaw = false;
        }
        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Right Claw");
            _targetingRightClaw = false;
            _targetedRightClaw = false;
            _prioritizedRightClaw = false;
        }
    }
}
