using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;

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