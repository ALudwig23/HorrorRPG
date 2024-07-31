using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBackButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private bool isMouseOver = false;       // Is the mouse currently over the button

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component on this GameObject
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;  // Start with the button hidden
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    private void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;  // Show the button image when the mouse enters the collider
            isMouseOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;  // Hide the button image when the mouse exits the collider
            isMouseOver = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMouseOver)
        {
            OnButtonClick();  // Trigger button click if the mouse is over the button
        }
    }

    private void OnButtonClick()
    {
        Debug.Log("Button clicked!");
        // Add your button click functionality here
    }
}
