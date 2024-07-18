using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSmash : MonoBehaviour
{
    
    public GameObject pickupTextUI;  // Assign the Text UI element here
    public float displayTime = 2.0f; // How long to display the message

    private bool isPlayerInRange = false;

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
            SmashBox();
        }
    }

    private void SmashBox()
    {
       

        if (pickupTextUI != null)
        {
            pickupTextUI.SetActive(true);
            StartCoroutine(HidePickupTextAfterDelay());
        }

        Destroy(gameObject);
    }

    private IEnumerator HidePickupTextAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (pickupTextUI != null)
        {
            pickupTextUI.SetActive(false);
        }
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
