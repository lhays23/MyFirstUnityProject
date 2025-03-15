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
        if (item != null)
        {
            Debug.Log("Using item: " + item.itemName);
            item.Use();
        }
        else
        {
            Debug.LogWarning("Tried to use a null item!");
        }
    }

}
