using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;

    private bool _isFacingDownward;
    private bool _isFacingUpward;
    private bool _isFacingLeft;
    private bool _isFacingRight;

    private bool _isMovingDownward;
    private bool _isMovingUpward;
    private bool _isMovingLeft;
    private bool _isMovingRight;

    //Inputs
    private Vector2 _inputDirection;
    private Vector2 _normalizedInputDirection;

    //Components 
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Animator _playerAnimator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponentInChildren<Transform>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        HandleAnimator();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        Movement();
    }

    protected void Movement()
    {
        //Normalize vector to make sure movement in all direction is the same speed
        _normalizedInputDirection = _inputDirection.normalized * acceleration;
        _rigidbody2D.velocity = _normalizedInputDirection;
    }

    private void HandleAnimator()
    {
        if (_rigidbody2D.velocity.x == 0 && _rigidbody2D.velocity.y == 0)
        {
            _isMovingDownward = false;
            _isMovingUpward = false;
            _isMovingLeft = false; 
            _isMovingRight = false;
        }

        if (_rigidbody2D.velocity.x > 0)
        {
            _isMovingRight = true;
            _isMovingLeft = false;

            _isFacingUpward = false;
            _isFacingDownward = false;
            _isFacingRight = true;
            _isFacingLeft = false;

            
            SoundManager.Instance.PlaySFX("SteppingConcrete");
        }

        if (_rigidbody2D.velocity.x < 0)
        {
            _isMovingLeft = true;
            _isMovingRight = false;

            _isFacingUpward = false;
            _isFacingDownward = false;
            _isFacingLeft = true;
            _isFacingRight = false;
            SoundManager.Instance.PlaySFX("SteppingConcrete");
        }

        if (_rigidbody2D.velocity.y > 0)
        {
            _isMovingUpward = true;
            _isMovingDownward = false;

            _isFacingUpward = true;
            _isFacingDownward = false;
            _isFacingRight = false;
            _isFacingLeft = false;
            SoundManager.Instance.PlaySFX("SteppingConcrete");
        }

        if (_rigidbody2D.velocity.y < 0)
        {
            _isMovingDownward = true;
            _isMovingUpward = false;

            _isFacingDownward = true;
            _isFacingUpward = false;
            _isFacingRight = false;
            _isFacingLeft = false;
            SoundManager.Instance.PlaySFX("SteppingConcrete");
        }

    }

    private void UpdateAnimator()
    {
        _playerAnimator.SetBool("isFacingDownward", _isFacingDownward);
        _playerAnimator.SetBool("isFacingUpward", _isFacingUpward);
        _playerAnimator.SetBool("isFacingLeft", _isFacingLeft);
        _playerAnimator.SetBool("isFacingRight", _isFacingRight);

        _playerAnimator.SetBool("isMovingDownward", _isMovingDownward);
        _playerAnimator.SetBool("isMovingUpward", _isMovingUpward);
        _playerAnimator.SetBool("isMovingLeft", _isMovingLeft);
        _playerAnimator.SetBool("isMovingRight", _isMovingRight);
    }
}