using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_LeftClaw : MonoBehaviour
{
    private bool _targetingLeftClaw = false;
    private bool _targetedLeftClaw = false;
    private bool _prioritizedLeftClaw = false;

    public bool TargetedLeftClaw
    {
        get { return _targetedLeftClaw; }
        set { _targetedLeftClaw = value; }
    }

    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_RightClaw _bs_RightClaw;
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
        if (_targetingLeftClaw == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_bs_Head.TargetedHead == false && _bs_Body.TargetedBody == false && _bs_RightClaw == false && _bs_LeftLeg == false && _bs_RightLeg == false)
                {
                    Debug.Log("Attacked Left Claw");
                    _targetedLeftClaw = true;
                }
                else if (_prioritizedLeftClaw == true)
                {
                    _targetedLeftClaw = true;
                }
                else
                {
                    _targetedLeftClaw = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Left Claw");
            _targetingLeftClaw = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Left Claw");
            _targetingLeftClaw = true;
            _prioritizedLeftClaw = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Left Claw");
            _targetingLeftClaw = false;
            _targetedLeftClaw = false;
        }
        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Left Claw");
            _targetingLeftClaw = false;
            _targetedLeftClaw = false;
            _prioritizedLeftClaw = false;
        }
    }
}
