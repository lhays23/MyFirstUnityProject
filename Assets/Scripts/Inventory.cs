using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemScriptableObject> items = new List<ItemScriptableObject>();

    public void AddItem(ItemScriptableObject newItem)
    {
        items.Add(newItem);
        Debug.Log("Added item: " + newItem.itemName);

        // Show all items in inventory
        Debug.Log("Current Inventory:");
        foreach (var item in items)
        {
            Debug.Log(item.itemName);
        }
    }

    public void UseItem(ItemScriptableObject item)
    {
        if (items.Contains(item))
        {
            item.Use();
            if (item.itemType == ItemType.Consumable)
            {
                items.Remove(item);
            }
        }
    }
}
