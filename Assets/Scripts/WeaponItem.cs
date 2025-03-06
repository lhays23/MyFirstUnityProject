using UnityEngine;

[System.Serializable]
public class WeaponItem : Item
{
    public int attackDamage;
    public float attackSpeed;

    public override void Use()
    {
        Debug.Log("Equipped weapon: " + itemName);
    }
}