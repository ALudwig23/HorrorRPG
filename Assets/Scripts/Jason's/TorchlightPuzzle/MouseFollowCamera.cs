using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowCamera : MonoBehaviour
{
    public float followSpeed = 5f; // Speed at which the camera follows the mouse

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Maintain the original z position

        // Interpolate between the current position and the mouse position
        transform.position = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
    }
}
