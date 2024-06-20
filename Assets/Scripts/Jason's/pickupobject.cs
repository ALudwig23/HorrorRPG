using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupobject : MonoBehaviour
{
    public Transform Lantern;
    public GameObject popUpText; // Assign this in the Inspector with the UI Text object
    public KeyCode equipKey = KeyCode.E;
    public Vector3 popUpOffset = new Vector3(1f, 0.5f, 0f);


    private bool isPlayerNearby = false;
    private bool isEquipped = false;
    private Transform originalParent; // To store original parent before equipping

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");
            isPlayerNearby = true;
            popUpText.SetActive(true); // Show pop-up text
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone");
            isPlayerNearby = false;
            popUpText.SetActive(false); // Hide pop-up text
        }
    }

    private void Update()
    {

        if (isPlayerNearby && Input.GetKeyDown(equipKey))
        {
            if (!isEquipped)
            {
                Equip();
            }
            else
            {
                Unequip();
            }
        }
        else if (isEquipped && Input.GetKeyDown(equipKey))
        {
            Drop();
        }
    }

    private void FixedUpdate()
    {
        popUpText.transform.position = Lantern.transform.position;

        //// Ensure pop-up text follows Lantern position with an offset
        //if (popUpText.activeSelf && Lantern != null)
        //{
        //    popUpText.transform.position = Lantern.position + popUpOffset;
        //}
    }

    private void Equip()
    {
        // Store original parent
        originalParent = transform.parent;

        // Example: Move to player or attach to player's hand
        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);

        // Example: Adjust position relative to player if needed
        transform.localPosition = Vector3.zero; // Adjust as per your game's setup

        isEquipped = true;
        popUpText.SetActive(false); // Hide pop-up text after equipping
    }

    private void Unequip()
    {
        // Example: Move object to original parent
        transform.SetParent(originalParent); // Return to original parent

        // Example: Reset position (adjust as needed)
        transform.position = originalParent.position;

        isEquipped = false;
    }

    private void Drop()
    {
        // Example: Drop the object
        transform.SetParent(null); // Unparent from player

        // Example: Add physics to the dropped object if needed
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true; // Enable physics
        }

        isEquipped = false;
        popUpText.SetActive(false); // Hide pop-up text after dropping
    }
}
