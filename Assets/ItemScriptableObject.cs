using UnityEngine;

public enum EquipmentType { Weapon, Armor, Accessory, None }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public EquipmentType equipmentType;
    [TextArea(3, 5)] public string itemDescription;

    // ✅ Virtual Use Method (Overridden by specific item types)
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
