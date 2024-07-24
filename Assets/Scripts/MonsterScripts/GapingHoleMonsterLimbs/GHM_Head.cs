using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHM_Head : MonoBehaviour
{
    private bool _targetingHead = false;
    private bool _targetedHead = false;
    private bool _prioritizedHead = false;
    public bool TargetedHead
    {
        get { return _targetedHead; }
        set { _targetedHead = value; }
    }

    [SerializeField] private GHM_Body _ghmBody;
    [SerializeField] private GHM_LeftLeg _ghmLeftLeg;
    [SerializeField] private GHM_RightLeg _ghmRightLeg;
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
                if (_ghmBody.TargetedBody == false && _ghmLeftLeg.TargetedLeftLeg == false && _ghmRightLeg.TargetedRightLeg == false)
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
