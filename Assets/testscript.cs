using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class testscript : MonoBehaviour
{
    private Light2D light2D;

    void Start()
    {
        // Get the Light2D component attached to this GameObject
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        // Example: Modify light intensity based on player input or other logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        // Example: Toggle the light on/off
        light2D.enabled = !light2D.enabled;
    }
}
