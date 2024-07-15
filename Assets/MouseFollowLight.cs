using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;


public class MouseFollowLight : MonoBehaviour
{
    public GameObject torchLight;
    public float warpIntensity = 0.5f;
    public float maxWarpDistance = 2f;
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

        ApplyWarpEffect(mousePosition);

        torchLight.transform.position = mousePosition;

        Vector3 direction = (mousePosition - torchLight.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        torchLight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ApplyWarpEffect(Vector3 mousePosition)
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseScreenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float distanceFromCenter = Vector2.Distance(mouseScreenPosition, screenCenter);
        float warpFactor = Mathf.Clamp01(distanceFromCenter / maxWarpDistance);
        float warpAmount = warpFactor * warpIntensity;

        // Check if the mouse position is near the edge of the Cinemachine camera
        if (virtualCamera != null && cameraCollider != null)
        {
            Bounds cameraBounds = cameraCollider.bounds;
            float edgeBuffer = 0.1f; // Adjust this buffer as needed
            if (!cameraBounds.Contains(mousePosition) || distanceFromCenter > cameraBounds.size.magnitude * edgeBuffer)
            {
                warpAmount = 0f; // Disable warp effect if near the camera edge
            }
        }

        torchLight.transform.position = Vector3.Lerp(torchLight.transform.position, mousePosition, warpAmount);
        torchLight.GetComponent<Light>().intensity = 1f - warpFactor;
    }
}
