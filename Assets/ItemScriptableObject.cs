using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStackSize = 1;
    [TextArea(3, 5)] public string itemDescription; // ✅ Added detailed description

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
