using UnityEngine;

[System.Serializable]
public class ConsumableItem : Item
{
    public int healAmount;

    public override void Use()
    {
        Debug.Log("Consumed: " + itemName + " and healed " + healAmount + " HP.");
    }
}
