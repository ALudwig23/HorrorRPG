using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Unlock()
    {
        // Implement your door opening logic here
        Debug.Log("Door Unlocked!");
        // Example: Destroy the door or animate it opening
        Destroy(gameObject);
    }
}
