using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; 
    public GameObject itemSlotPrefab; 
    public Transform itemSlotContainer; 

    private PlayerInventory playerInventory;
    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);

            if (isInventoryOpen)
            {
                UpdateInventoryUI();
            }
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in playerInventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            Image itemIcon = itemSlot.GetComponentInChildren<Image>();
            itemIcon.sprite = item.itemIcon;

            // Optionally add more item details here
        }
    }
}

