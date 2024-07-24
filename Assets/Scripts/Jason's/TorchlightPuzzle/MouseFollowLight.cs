using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;


public class MouseFollowLight : MonoBehaviour
{
    public GameObject torchLight; // Reference to the torchlight
    public float followSpeed = 5f; // Speed at which the torchlight follows the mouse cursor

    private CinemachineVirtualCamera virtualCamera;
    private Collider2D cameraCollider;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            cameraCollider = virtualCamera.GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Smoothly interpolate the torchlight's position to the mouse position
        torchLight.transform.position = Vector3.Lerp(torchLight.transform.position, mousePosition, followSpeed * Time.deltaTime);
    }
}
