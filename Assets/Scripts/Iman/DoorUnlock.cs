using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public string keyItemName;  // The name of the key item required to unlock the door
    private bool isPlayerInRange = false;
    private PlayerInventory playerInventory;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null && playerInventory.HasItem(keyItemName))
            {
                Door door = GetComponent<Door>();
                if (door != null)
                {
                    door.Unlock();
                    door.Open();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerInventory = null;
        }
    }
}