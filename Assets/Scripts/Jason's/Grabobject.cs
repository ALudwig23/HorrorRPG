using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Grabobject : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;

    private int layerIndex;
    private GameObject grabbedObject;

    void Start()
    {
        // Ensure the "Objects" layer exists and is assigned to the objects you want to grab
        layerIndex = LayerMask.NameToLayer("Objects");
        if (layerIndex == -1)
        {
            Debug.LogError("Layer 'Objects' not found. Please ensure you have an 'Objects' layer in your project.");
        }
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject == null)
            {
                Debug.Log("Object detected, attempting to grab.");
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
                Debug.Log("Object grabbed.");
            }
            else if (Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject != null)
            {
                Debug.Log("Dropping object.");
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
                Debug.Log("Object dropped.");
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance, Color.red);
    }
}
