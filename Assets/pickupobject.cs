using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupobject : MonoBehaviour
{
    public string pickupKey = "e"; // Key to pick up and drop the object
    public GameObject keyPromptPrefab; // Reference to the key prompt prefab

    private bool isPlayerNearby = false;
    private bool isPickedUp = false;
    private GameObject player;
    private GameObject keyPromptInstance; // Instance of the key prompt

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(pickupKey))
        {
            Debug.Log("Pickup key pressed. isPickedUp: " + isPickedUp);
            if (!isPickedUp)
            {
                Debug.Log("Calling Pickup method");
                Pickup();
            }
            else
            {
                Debug.Log("Calling Drop method");
                Drop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            isPlayerNearby = true;
            player = other.gameObject;

            // Instantiate the key prompt near the object
            if (keyPromptPrefab != null && keyPromptInstance == null)
            {
                keyPromptInstance = Instantiate(keyPromptPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            isPlayerNearby = false;
            player = null;

            // Destroy the key prompt
            if (keyPromptInstance != null)
            {
                Destroy(keyPromptInstance);
            }
        }
    }

    private void Pickup()
    {
        Debug.Log("Pickup method called");
        isPickedUp = true;
        transform.SetParent(player.transform); // Make the object a child of the player
        transform.localPosition = new Vector3(0, 0.5f, 0); // Adjust position relative to the player
        GetComponent<Collider2D>().enabled = false; // Disable the collider to avoid further triggers

        // Destroy the key prompt
        if (keyPromptInstance != null)
        {
            Destroy(keyPromptInstance);
        }
        Debug.Log("Object picked up. isPickedUp set to true.");
    }

    private void Drop()
    {
        Debug.Log("Drop method called");
        isPickedUp = false;
        transform.SetParent(null); // Detach from the player
        transform.position = player.transform.position + player.transform.right * 1.5f; // Adjust drop position near the player
        GetComponent<Collider2D>().enabled = true; // Re-enable the collider

        // Optionally, show the key prompt again when the object is dropped
        if (isPlayerNearby && keyPromptPrefab != null)
        {
            keyPromptInstance = Instantiate(keyPromptPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
        Debug.Log("Object dropped. isPickedUp set to false.");
    }
}
