using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Intensity of smoothing effect
    private float smoothTime = 0.05f;

    private Vector3 _cameraOffset = new Vector3(0f, 0f, -10f);
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
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }

}
