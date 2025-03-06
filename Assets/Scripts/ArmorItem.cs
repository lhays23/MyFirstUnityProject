using UnityEngine;

[System.Serializable]
public class ArmorItem : Item
{
    public int defenseValue;

    public override void Use()
    {
        Debug.Log("Equipped armor: " + itemName);
    }
}
