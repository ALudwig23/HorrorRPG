using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public GameObject smashEffect;      // Particle effect for smashing
    public Item item;                   // Item to add to inventory when smashed
    public UIManager uiManager;         // Reference to the UIManager

    private bool isPlayerInRange = false;
    private bool hasItem = false;       // Flag to determine if the box contains an item

    void Start()
    {
        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UIManager>();
        hasItem = item != null;          // Determine if this box contains an item
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SmashBox();
        }
    }

    private void SmashBox()
    {
        if (smashEffect != null)
        {
            Instantiate(smashEffect, transform.position, transform.rotation);
        }

        // Add item to player's inventory if this box contains an item
        if (hasItem)
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);
                Debug.Log($"Item {item.itemName} added to inventory.");

                // Display the message using the UIManager
                if (uiManager != null)
                {
                    uiManager.ShowMessage("You got something!", 2f);  // Display the message for 2 seconds
                }
                else
                {
                    Debug.LogError("UIManager not found in the scene.");
                }
            }
            else
            {
                Debug.LogError("PlayerInventory not found in the scene.");
            }
        }
        else
        {
            if (uiManager != null)
            {
                uiManager.ShowMessage("This box is empty.", 2f); // Display message if box is empty
            }
            else
            {
                Debug.LogError("UIManager not found in the scene.");
            }
        }

        // Destroy the box GameObject
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited range.");
        }
    }
}




