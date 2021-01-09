using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Sword,
    Bow,
    SpellBook,
    Shield
}

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public WeaponType weaponType;
    public int damageAmount;
    private void Awake()
    {
        itemType = ItemType.Weapon;
    }
    
}
