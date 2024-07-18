using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public GameObject pickupTextUI;  // Assign the Text UI element here
    public Item keyItem;  // Assign the key item in the Inspector

    private bool isPlayerInRange = false;
    private PlayerInventory playerInventory;

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
            if (playerInventory != null)
            {
                playerInventory.AddItem(keyItem);
                Destroy(gameObject);

                if (pickupTextUI != null)
                {
                    pickupTextUI.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            if (pickupTextUI != null)
            {
                pickupTextUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerInventory = null;

            if (pickupTextUI != null)
            {
                pickupTextUI.SetActive(false);
            }
        }
    }
}
