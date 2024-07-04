using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private Key playerKeyHolder;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerKeyHolder != null && playerKeyHolder.hasKey)
            {
                Door door = GetComponent<Door>();
                if (door != null)
                {
                    door.Unlock();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerKeyHolder = null;
        }
    }
}
