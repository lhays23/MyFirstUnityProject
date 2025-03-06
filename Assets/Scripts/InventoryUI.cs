using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab;
    public Transform itemGrid;

    private Inventory playerInventory;

    void Start()
    {
        inventoryPanel.SetActive(false); // Hide inventory at start
        playerInventory = FindObjectOfType<PlayerInventory>().GetComponent<Inventory>();
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Toggle inventory with Tab key
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
        }
    }
}
