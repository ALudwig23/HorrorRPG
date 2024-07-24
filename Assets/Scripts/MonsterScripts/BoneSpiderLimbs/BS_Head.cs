using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Head : MonoBehaviour
{
    private bool _targetingHead = false;
    private bool _targetedHead = false;
    private bool _prioritizedHead = false;
    public bool TargetedHead
    {
        get { return _targetedHead; }
        set { _targetedHead = value; }
    }

    [SerializeField] private BS_Body _bs_Body;
    [SerializeField] private BS_LeftClaw _bs_LeftClaw;
    [SerializeField] private BS_RightClaw _bs_RightClaw;
    [SerializeField] private BS_LeftLeg _bs_LeftLeg;
    [SerializeField] private BS_RightLeg _bs_RightLeg;
    [SerializeField] private CursorMovement _cursorMovement;
    private Canvas _canvas;
    private Transform _pointerTransform;

    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
        _pointerTransform = _canvas.transform.Find("Pointer");
        _cursorMovement = _pointerTransform.GetComponent<CursorMovement>();
    }

    private void Update()
    {
        if (_targetingHead == true)
        {
            if (_cursorMovement.EnterPressed == true)
            {
                if (_bs_Body.TargetedBody == false && _bs_LeftClaw == false && _bs_RightClaw == false && _bs_LeftLeg == false && _bs_RightLeg)
                {
                    Debug.Log("Attacked Head");
                    _targetedHead = true;
                }
                else if (_prioritizedHead == true)
                {
                    _targetedHead = true;
                }
                else
                {
                    _targetedHead = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("On Head");
            _targetingHead = true;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("Prioritized Head");
            _targetingHead = true;
            _prioritizedHead = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointer"))
        {
            Debug.Log("No Longer On Head");
            _targetingHead = false;
            _targetedHead = false;
        }

        if (collision.CompareTag("AttackPointerCenter"))
        {
            Debug.Log("No Longer Prioritizing Head");
            _targetingHead = false;
            _targetedHead = false;
            _prioritizedHead = false;
        }
    }
}

