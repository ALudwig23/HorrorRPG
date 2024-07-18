using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorMovement : MonoBehaviour
{
    private float _waitTime = 1f;
    [SerializeField] private float acceleration;

    private bool _enterPressed = false;

    public bool EnterPressed
    {
        get { return _enterPressed; }
        set { _enterPressed = value; }
    }

    //Inputs
    private Vector2 _inputDirection;
    private Vector2 _normalizedInputDirection;

    //Components 
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_waitTime >= 0f)
        {
            _waitTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Return) && _waitTime <= 0f)
        {
            _enterPressed = true;
            _waitTime = 1f;
            Debug.Log($"Enter Pressed = {_enterPressed}");
        }
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

}
