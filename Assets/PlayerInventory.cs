using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Inventory inventory;
    private Inventory playerInventory;
    private ItemScriptableObject selectedItem;
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
        // Example: Adding starting items
        ItemScriptableObject smallHealthPotion = Resources.Load<ItemScriptableObject>("Small Health Potion");
        ItemScriptableObject sword = Resources.Load<ItemScriptableObject>("Sword");

        if (smallHealthPotion != null && playerInventory.AddItem(smallHealthPotion))
        {
            Debug.Log("✅ Added starting item: " + smallHealthPotion.itemName);
        }

        if (sword != null && playerInventory.AddItem(sword))
        {
            Debug.Log("✅ Added starting item: " + sword.itemName);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Example: Trying to use an item
        {
            if (selectedItem != null)
            {
                int index = playerInventory.items.IndexOf(selectedItem);
                if (index != -1)
                {
                    playerInventory.UseItem(index);
                }
            }
            else
            {
                Debug.LogWarning("❌ No item selected!");
            }
        }
    }

    public void SelectItem(ItemScriptableObject item)
    {
        if (item != null)
        {
            selectedItem = item;
            Debug.Log("✅ Selected item: " + selectedItem.itemName);
        }
        else
        {
            Debug.LogWarning("❌ Tried to select a null item!");
        }
    }


}
