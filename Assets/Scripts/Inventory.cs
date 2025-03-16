using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxInventorySize = 40; // ✅ Always 40 slots
    public List<ItemScriptableObject> items = new List<ItemScriptableObject>();

    void Start()
    {
        // Ensure the inventory list is always 40 slots
        while (items.Count < maxInventorySize)
        {
            items.Add(null); // ✅ Fill empty slots with null (placeholders)
        }
    }

    public bool AddItem(ItemScriptableObject item)
    {
        // Find the first empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null) // ✅ Check for empty slot
            {
                items[i] = item;
                Debug.Log("✅ Added item: " + item.itemName);
                return true;
            }
        }

        Debug.LogWarning("❌ Inventory is full! Cannot add " + item.itemName);
        return false; // ❌ Inventory full
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items[index] = null; // ✅ Remove item (leave empty slot)
            Debug.Log("❌ Removed item from slot " + index);
        }
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count && items[index] != null)
        {
            Debug.Log("✅ Using item: " + items[index].itemName);
            items[index].Use();
            RemoveItem(index); // Optional: Remove after use
        }
        else
        {
            Debug.LogWarning("❌ Cannot use item: Slot is empty or out of range.");
        }
    }

}
