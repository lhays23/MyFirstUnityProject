using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Inventory inventory;

    public ItemScriptableObject[] defaultItems; // Array for default items

    void Awake()
    {
        // ✅ Ensure Inventory is assigned
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>(); // ✅ Auto-add if missing
        }
    }

    void Start()
    {
        // ✅ Add default items at the start of the game
        if (defaultItems.Length > 0)
        {
            foreach (var item in defaultItems)
            {
                if (item != null)
                {
                    inventory.AddItem(item);
                    Debug.Log("✅ Added default item: " + item.itemName);
                }
            }
        }
        else
        {
            Debug.LogWarning("⚠️ No default items assigned in PlayerInventory!");
        }
    }

    void Update()
    {
        // ✅ Press Q to use the first item
        if (Input.GetKeyDown(KeyCode.Q) && inventory.items.Count > 0)
        {
            inventory.UseItem(inventory.items[0]); // Uses the first item in inventory
        }
    }
}
