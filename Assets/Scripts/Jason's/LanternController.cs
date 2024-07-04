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
    private bool lightsOn = true; // Initial state of lights
    private bool lastLightsState = true; // Track last lights state
    private Light2D light2D;

    void Start()
    {
        popupText.SetActive(false); // Hide the popup text at the start
        light2D = GetComponentInChildren<Light2D>(); // Get the Light2D component (assuming it's a child)
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
        lightsOn = lastLightsState;
        UpdateLightState();
        TorchlightState.instance.SetTorchlightState(true); // Update torchlight state to on
        Debug.Log("Lantern picked up. Lights " + (lightsOn ? "on" : "off") + ".");
    }

    void DropLantern()
    {
        // Detach lantern from player
        transform.SetParent(null);
        isPickedUp = false;
        gameObject.SetActive(true); // Ensure the lantern is visible when dropped

        // Store current lights state before dropping
        lastLightsState = lightsOn;

        // Reset lights state when dropped
        UpdateLightState();
        TorchlightState.instance.SetTorchlightState(false); // Update torchlight state to off
        Debug.Log("Lantern dropped. Lights " + (lightsOn ? "on" : "off") + ".");
    }

    void ToggleLanternLights()
    {
        lightsOn = !lightsOn; // Toggle the lights state
        UpdateLightState();
        Debug.Log("Lantern lights " + (lightsOn ? "on" : "off") + ".");
    }

    void UpdateLightState()
    {
        if (light2D != null)
        {
            light2D.enabled = lightsOn; // Enable or disable the Light2D component based on lightsOn
        }
    }
}
