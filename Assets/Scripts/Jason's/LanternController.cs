using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternController : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public Transform lanternHoldPosition; // Position where the lantern should be held
    public GameObject popupText; // The popup text to show when near the lantern
    public KeyCode pickUpKey = KeyCode.F; // Key to pick up the lantern
    public KeyCode dropKey = KeyCode.Q; // Key to drop the lantern
    public KeyCode toggleLightKey = KeyCode.L; // Key to toggle lantern lights

    private bool isPickedUp = false;
    private Light2D light2D;

    void Start()
    {
        popupText.SetActive(false); // Hide the popup text at the start
        InitializeLight2DComponent();
    }

    void Update()
    {
        if (!isPickedUp && Input.GetKeyDown(pickUpKey) && IsPlayerNearLantern())
        {
            PickUpLantern();
        }
        else if (isPickedUp && Input.GetKeyDown(dropKey))
        {
            DropLantern();
        }
        else if (isPickedUp && Input.GetKeyDown(toggleLightKey))
        {
            ToggleLanternLights();
        }
    }

    bool IsPlayerNearLantern()
    {
        // Check if the player GameObject is near the lantern
        return Vector3.Distance(transform.position, player.transform.position) < 2f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !isPickedUp)
        {
            popupText.SetActive(true); // Show the popup text when the player collides with the lantern
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            popupText.SetActive(false); // Hide the popup text when the player leaves the lantern
        }
    }

    void PickUpLantern()
    {
        // Attach lantern to player
        transform.SetParent(lanternHoldPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isPickedUp = true;
        popupText.SetActive(false); // Hide the popup text after picking up

        // Set lights state when picked up
        bool currentState = TorchlightState.instance.GetTorchlightState();
        if (light2D != null)
        {
            UpdateLightState(currentState);
        }
        else
        {
            Debug.LogError("Light2D component is missing when picking up the lantern!");
        }

        Debug.Log("Lantern picked up. Lights " + (currentState ? "on" : "off") + ".");
    }

    void DropLantern()
    {
        // Detach lantern from player
        transform.SetParent(null);
        isPickedUp = false;
        gameObject.SetActive(true); // Ensure the lantern is visible when dropped

        // Reset lights state when dropped
        bool currentState = TorchlightState.instance.GetTorchlightState();
        if (light2D != null)
        {
            UpdateLightState(currentState);
        }
        else
        {
            Debug.LogError("Light2D component is missing when dropping the lantern!");
        }

        Debug.Log("Lantern dropped. Lights " + (currentState ? "on" : "off") + ".");
    }

    void ToggleLanternLights()
    {
        // Re-initialize light2D if it's null
        if (light2D == null)
        {
            InitializeLight2DComponent();
        }

        if (light2D != null)
        {
            bool currentState = !TorchlightState.instance.GetTorchlightState(); // Toggle the current state
            TorchlightState.instance.SetTorchlightState(currentState); // Update the torchlight state
            UpdateLightState(currentState); // Update the light state in the Light2D component

            Debug.Log("Lantern lights toggled. Now " + (currentState ? "on" : "off") + ".");
        }
        else
        {
            Debug.LogError("Light2D component is missing when toggling the lights!");
        }
    }

    void UpdateLightState(bool state)
    {
        if (light2D != null)
        {
            light2D.enabled = state; // Enable or disable the Light2D component based on the state
            Debug.Log("UpdateLightState called. Lights " + (state ? "on" : "off") + ".");
        }
        else
        {
            Debug.LogError("Light2D component is missing in UpdateLightState.");
        }
    }

    void InitializeLight2DComponent()
    {
        light2D = GetComponentInChildren<Light2D>();
        if (light2D != null)
        {
            Debug.Log("Light2D component found in InitializeLight2DComponent.");
            light2D.enabled = TorchlightState.instance.GetTorchlightState(); // Set initial light state based on TorchlightState
            Debug.Log("Light initialized: " + (light2D.enabled ? "on" : "off"));
        }
        else
        {
            Debug.LogError("Light2D component not found in InitializeLight2DComponent!");
        }
    }
}
