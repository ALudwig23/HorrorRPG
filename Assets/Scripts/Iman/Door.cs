using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isUnlocked = false;
    private bool isOpen = false;

    // Unlock the door
    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Door Unlocked!");
        // Additional unlock logic if needed
    }

    // Open the door if it is unlocked and currently closed
    public void Open()
    {
        if (isUnlocked && !isOpen)
        {
            isOpen = true;
            Debug.Log("Door Opened!");
            gameObject.SetActive(false); // Example: Disable the GameObject to "open" the door
        }
        else
        {
            Debug.Log("Door cannot be opened (either locked or already open).");
        }
    }

    public void Close()
    {
        if (isUnlocked && isOpen)
        {
            isOpen = false;
            Debug.Log("Door Closed!");
            gameObject.SetActive(true); // Example: Enable the GameObject to "close" the door
        }
        else
        {
            Debug.Log("Door cannot be closed (either locked or already closed).");
        }
    }

    // Check if the door is currently open
    public bool IsOpen()
    {
        return isOpen;
    }

    // Check if the door is currently unlocked
    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}



