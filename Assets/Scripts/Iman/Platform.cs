using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public string requiredItemName = "Coin"; // The name of the item required to trigger the event
    public GameObject jumpscareEffect;       // The jumpscare effect to trigger
    public UIManager uiManager;              // Reference to the UIManager script

    private bool isPlayerOnPlatform = false;
    private GameObject player;
    private bool itemPlaced = false;         // Flag to track if the item has been placed
    private int interactionCount = 0;        // Counter to track the number of interactions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            player = null;
        }
    }

    void Update()
    {
        if (isPlayerOnPlatform && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                interactionCount++; // Increment interaction count
                if (!itemPlaced)
                {
                    if (playerInventory.HasItem(requiredItemName))
                    {
                        playerInventory.RemoveItem(requiredItemName);
                        TriggerJumpscare();
                        itemPlaced = true; // Set the flag to true after placing the item
                        ShowMessage("You put the missing piece", 2f);
                    }
                    else
                    {
                        ShowMessage("Something is missing here", 2f);
                    }
                }
                else
                {
                    // Display different messages based on the number of interactions
                    switch (interactionCount)
                    {
                        case 4:
                            ShowMessage("You've interacted here a few times!", 2f);
                            break;
                        case 6:
                            ShowMessage("Still here? You’ve dimwit!", 2f);
                            break;
                        default:
                            ShowMessage("Boo!", 2f);
                            break;
                    }
                }
            }
        }
    }

    private void TriggerJumpscare()
    {
        if (jumpscareEffect != null)
        {
            Instantiate(jumpscareEffect, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Jumpscare effect is not assigned.");
        }
    }

    private void ShowMessage(string message, float duration)
    {
        if (uiManager != null)
        {
            uiManager.ShowMessage(message, duration);
        }
        else
        {
            Debug.LogError("UIManager is not assigned.");
        }
    }
}





