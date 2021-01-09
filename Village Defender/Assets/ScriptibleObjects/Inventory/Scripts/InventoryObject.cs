using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject Database;
    public Inventory Container;

    public void addItem(Item _item, int _amount) {

        for (int i = 0; i < Container.Item.Count; i++)
        {
            if (Container.Item[i].item.Id == _item.Id) {
                Container.Item[i].addAmount(_amount);
                return;
            }
        }
        Container.Item.Add(new InventorySlot(_item.Id, _item, _amount));
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

        Debug.Log("file was saved");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("file was loaded");
        }
    }

    [ContextMenu("Clear")]
    public void Clear() {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory {
    public List<InventorySlot> Item = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot {
    public int ID;
    public Item item;
    public int amount;

    public InventorySlot(int _id, Item _item, int _amount) {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void addAmount(int value) {
        amount += value;
    }
}
