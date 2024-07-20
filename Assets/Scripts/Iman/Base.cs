using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public Door door; // Reference to the Door script
    private bool isBoxOnBase = false;

    void Start()
    {
        door.Unlock(); // Test unlocking
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTriggerEnter2D called with {other.tag}");
        if (other.CompareTag("Box"))
        {
            isBoxOnBase = true;
            Debug.Log("Box has entered the base.");
            if (door != null)
            {
                Debug.Log("Attempting to open door...");
                door.Open();
            }
            else
            {
                Debug.Log("No door reference found.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"OnTriggerExit2D called with {other.tag}");
        if (other.CompareTag("Box"))
        {
            isBoxOnBase = false;
            Debug.Log("Box has exited the base.");
            if (door != null)
            {
                Debug.Log("Attempting to close door...");
                door.Close();
            }
            else
            {
                Debug.Log("No door reference found.");
            }
        }
    }

}



