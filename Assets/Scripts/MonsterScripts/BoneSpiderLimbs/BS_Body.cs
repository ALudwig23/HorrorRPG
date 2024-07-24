using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Body : MonoBehaviour
{
    private bool _targetingBody = false;
    private bool _targetedBody = false;
    private bool _prioritizedBody = false;

    public bool TargetedBody
    {
        get { return _targetedBody; }
        set { _targetedBody = value; }
    }

    [SerializeField] private BS_Head _bs_Head;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
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
        if (_targetingBody == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_bs_Head.TargetedHead == false && _bs_LeftClaw == false && _bs_RightClaw == false && _bs_LeftLeg == false && _bs_RightLeg == false)
                {
                    Debug.Log("Attacked Body");
                    _targetedBody = true;
                }
                else if (_prioritizedBody == true)
                {
                    _targetedBody = true;
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
            _targetingBody = false;
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
