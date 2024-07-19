using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Item added: " + item.itemName);

        // Notify the UI to update
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null && inventoryUI.gameObject.activeSelf)
        {
            inventoryUI.UpdateInventoryUI();
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(string itemName)
    {
        Item itemToRemove = null;
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                itemToRemove = item;
                break;
            }
        }
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            Debug.Log("Item removed: " + itemName);

            // Notify the UI to update
            InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
            if (inventoryUI != null && inventoryUI.gameObject.activeSelf)
            {
                inventoryUI.UpdateInventoryUI();
            }
        }
    }
}


