using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public Camera mainCamera; //main camera ref

    private void Start()
    {
        //nothin
    }

    void Update()
    {
        // get mouse position to screen position
        Vector3 mouseScreenPosition = Input.mousePosition;

        // convert screen space to world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 10f));

        // print de xyz numbers of mouse position in world space into console
        Debug.Log("Mouse World Position: " + mouseWorldPosition);
    }
}

