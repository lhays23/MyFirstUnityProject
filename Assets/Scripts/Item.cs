using UnityEngine;

public enum ItemType { Weapon, Armor, Consumable, Misc }

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStackSize = 1;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
