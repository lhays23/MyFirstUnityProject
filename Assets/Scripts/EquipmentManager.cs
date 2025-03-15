using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public Image weaponSlotUI, armorSlotUI, accessorySlotUI; // ✅ UI Slots

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Dictionary<EquipmentType, ItemScriptableObject> equippedItems = new Dictionary<EquipmentType, ItemScriptableObject>();

    public void EquipItem(ItemScriptableObject item)
    {
        if (item.equipmentType == EquipmentType.None)
        {
            Debug.LogWarning("❌ This item cannot be equipped!");
            return;
        }

        if (equippedItems.ContainsKey(item.equipmentType))
        {
            UnequipItem(item.equipmentType);
        }

        equippedItems[item.equipmentType] = item;
        Debug.Log("✅ Equipped: " + item.itemName);

        UpdateEquipmentUI();
    }

    public void UnequipItem(EquipmentType type)
    {
        if (equippedItems.ContainsKey(type))
        {
            Debug.Log("❌ Unequipped: " + equippedItems[type].itemName);
            equippedItems.Remove(type);
        }

        UpdateEquipmentUI();
    }

    private void UpdateEquipmentUI()
    {
        if (equippedItems.ContainsKey(EquipmentType.Weapon))
            weaponSlotUI.sprite = equippedItems[EquipmentType.Weapon].icon;
        else
            weaponSlotUI.sprite = null;

        if (equippedItems.ContainsKey(EquipmentType.Armor))
            armorSlotUI.sprite = equippedItems[EquipmentType.Armor].icon;
        else
            armorSlotUI.sprite = null;

        if (equippedItems.ContainsKey(EquipmentType.Accessory))
            accessorySlotUI.sprite = equippedItems[EquipmentType.Accessory].icon;
        else
            accessorySlotUI.sprite = null;
    }
}
