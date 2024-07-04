using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public GameObject pickupTextUI;  // Assign the Text UI element here

    private bool isPlayerInRange = false;
    private Key playerKeyHolder;

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
            if (playerKeyHolder != null)
            {
                playerKeyHolder.hasKey = true;
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
            playerKeyHolder = other.GetComponent<Key>();

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
            playerKeyHolder = null;

            if (pickupTextUI != null)
            {
                pickupTextUI.SetActive(false);
            }
        }
    }
}