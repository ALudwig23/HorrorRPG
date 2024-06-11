using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Intensity of smoothing effect
    private float _smoothTime = 0.05f;
    private float _panSpeed = 0.05f;

    private Vector3 _cameraOffset = new Vector3(0f, 0f, -10f);
    private Vector3 _panOffset;
    private Vector3 _velocity = Vector3.zero;

    private Transform _playerPosition;


    private void Update()
    {
        if (_playerPosition == null)
        {
            _playerPosition = GameObject.FindWithTag("Player").transform;
        }
        if (_playerPosition == null)
            return;

        //Allow for camera to follow player movement
        Vector3 targetPosition = _playerPosition.position + _cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

        CameraPan();
    }

    private void CameraPan()
    {
        //Adjust panning speed
        if (_cameraOffset.x < _panOffset.x)
        {
            _cameraOffset.x += _panSpeed + Time.deltaTime;
        }

        if (_cameraOffset.x > _panOffset.x)
        {
            _cameraOffset.x -= _panSpeed + Time.deltaTime;
        }

        if (_cameraOffset.y < _panOffset.y)
        {
            _cameraOffset.y += _panSpeed + Time.deltaTime;
        }

        if (_cameraOffset.y > _panOffset.y)
        {
            _cameraOffset.y -= _panSpeed + Time.deltaTime;
        }

        //Left pan and right pan
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _panOffset.x = -3f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _panOffset.x = 3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _panOffset.x = 0f;
        }

        //Upwards pan and downwards pan
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _panOffset.y = 3f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _panOffset.y = -3f;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _panOffset.y = 0f;
        }

        
    }
}
