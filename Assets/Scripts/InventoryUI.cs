using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab;
    public Transform itemGrid;

    private Inventory playerInventory;

    public Sprite emptySlotPlaceholder; // ✅ Assign in Inspector

    void UpdateInventoryUI()
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject); // ✅ Clear old UI slots
        }

        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
            Image icon = slot.transform.Find("Icon").GetComponent<Image>(); // ✅ Ensure correct reference

            if (playerInventory.items[i] != null)
            {
                icon.sprite = playerInventory.items[i].icon; // ✅ Show item icon
            }
            else
            {
                icon.sprite = emptySlotPlaceholder; // ✅ Show placeholder
            }

            // ✅ Tooltip for non-empty slots
            if (playerInventory.items[i] != null)
            {
                EventTrigger trigger = slot.AddComponent<EventTrigger>();

                EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
                hoverEntry.eventID = EventTriggerType.PointerEnter;
                hoverEntry.callback.AddListener((eventData) =>
                {
                    ItemTooltip.ShowTooltip(playerInventory.items[i].itemName, playerInventory.items[i].itemDescription);
                });

                EventTrigger.Entry exitEntry = new EventTrigger.Entry();
                exitEntry.eventID = EventTriggerType.PointerExit;
                exitEntry.callback.AddListener((eventData) =>
                {
                    ItemTooltip.HideTooltip();
                });

                trigger.triggers.Add(hoverEntry);
                trigger.triggers.Add(exitEntry);
            }
        }
    }

    void Start()
    {
        inventoryPanel.SetActive(false);

        PlayerInventory player = FindFirstObjectByType<PlayerInventory>();
        if (player != null)
        {
            playerInventory = player.GetComponent<Inventory>(); // ✅ Assign inventory
        }

        if (playerInventory == null)
        {
            Debug.LogError("❌ Inventory component not found on Player!"); // ✅ Debugging
            return; // ❌ Prevents further execution
        }

        UpdateInventoryUI(); // ✅ Only called if playerInventory is valid
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            UpdateInventoryUI();
        }
    }

    void CreateItemSlot(ItemScriptableObject item, int index)
    {
        GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
        slot.GetComponent<Image>().sprite = item.icon;

        Button button = slot.GetComponent<Button>();
        if (button != null)
        {
            PlayerInventory playerInventory = FindFirstObjectByType<PlayerInventory>(); // ✅ Ensure we use PlayerInventory
            button.onClick.AddListener(() => playerInventory.SelectItem(item)); // ✅ Correct method call
        }
    }


}
