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

    void Start()
    {
        inventoryPanel.SetActive(false);
        playerInventory = FindObjectOfType<PlayerInventory>().GetComponent<Inventory>();
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        // Clear existing slots
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        // Create a slot for each item in inventory
        foreach (var item in playerInventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
            slot.GetComponentInChildren<Image>().sprite = item.icon;

            // ✅ Add tooltip events
            EventTrigger trigger = slot.AddComponent<EventTrigger>();

            // Mouse Hover (Show Tooltip)
            EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
            hoverEntry.eventID = EventTriggerType.PointerEnter;
            hoverEntry.callback.AddListener((eventData) =>
            {
                Vector2 mousePos = Input.mousePosition;
                ItemTooltip.ShowTooltip(item.itemName, item.itemDescription, mousePos); // ✅ Uses real description now
            });

            // Mouse Exit (Hide Tooltip)
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
