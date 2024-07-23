using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_LeftLeg : MonoBehaviour
{
    private bool _targetingLeftLeg = false;
    private bool _targetedLeftLeg = false;
    private bool _prioritizedLeftLeg = false;

    public bool TargetedLeftLeg
    {
        get { return _targetedLeftLeg; }
        set { _targetedLeftLeg = value; }
    }

    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
    [SerializeField] private BS_RightClaw _bs_RightClaw;
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
        if (_targetingLeftLeg == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_bs_Head.TargetedHead == false && _bs_Body.TargetedBody == false && _bs_LeftClaw == false && _bs_RightClaw == false && _bs_RightLeg == false)
                {
                    Debug.Log("Attacked Left Leg");
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
