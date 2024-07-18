using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public GameObject smashEffect;      // Particle effect for smashing
    public Item item;                   // Item to add to inventory when smashed
    public GameObject pickupTextUI;     // UI element for pickup message
    public float displayTime = 2.0f;    // Time to display the message

    private bool isPlayerInRange = false;
    private Coroutine hideCoroutine;

    void Start()
    {
        if (pickupTextUI != null)
        {
            pickupTextUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SmashBox();  // Smash the box and handle item pickup
        }
    }

    private void SmashBox()
    {
        if (smashEffect != null)
        {
            Instantiate(smashEffect, transform.position, transform.rotation);
        }

        // Add item to player's inventory if an item is defined
        if (item != null)
        {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);

                // Show pickup message
                ShowPickupMessage(item.itemName);
            }
        }

        // Destroy the box GameObject
        Destroy(gameObject);
    }

    private void ShowPickupMessage(string itemName)
    {
        if (pickupTextUI != null)
        {
            Text textComponent = pickupTextUI.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = $"Picked up: {itemName}";
                pickupTextUI.SetActive(true);

                // Stop previous coroutine if running
                if (hideCoroutine != null)
                {
                    StopCoroutine(hideCoroutine);
                }

                // Start coroutine to hide pickup text
                hideCoroutine = StartCoroutine(HidePickupTextAfterDelay());
            }
        }
    }

    private IEnumerator HidePickupTextAfterDelay()
    {
        Debug.Log("Hiding pickup message coroutine started.");
        float elapsedTime = 0f;

        while (elapsedTime < displayTime)
        {
            // Wait for player input (e.g., pressing a key)
            if (Input.GetKeyDown(KeyCode.E))
            {
                break; // Exit the loop early if E is pressed again
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        if (pickupTextUI != null)
        {
            pickupTextUI.SetActive(false);
            Debug.Log("Pickup text hidden.");
        }
        else
        {
            Debug.LogWarning("pickupTextUI is null or inactive.");
        }

        Debug.Log("Hiding pickup message coroutine ended.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
