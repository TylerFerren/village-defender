using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Default, Weapon, Armor, Potion, Currency
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;

    public ItemType itemType;
    [TextArea(10,20)]public string Discription;


    [Header("UI Elements")]
    public ItemSlotUISettings UISettings;
    public Color backgroundColor;
    public Sprite icon;
    public Color iconColor;

    public Item createItem() {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int Id;
   


    public Item(ItemObject item) {
        name = item.name;
        Id = item.Id;
    }
}
