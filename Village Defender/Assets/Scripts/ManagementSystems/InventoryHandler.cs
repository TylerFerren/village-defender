using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    //public Inventory inventory;

    //[SerializeField] private Inventory_UI inventory_UI;
    //void Start()
    //{
    //    inventory = new Inventory();
    //    inventory_UI.setInventory(inventory);

    //    foreach (IItem item in inventory.GetItemsList()) {
    //        print(item.Amount);
    //    }
    //}

    public InventoryObject inventory;
    public InventoryUI inventoryUI;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemGround>();
        if (item) {
            inventory.addItem(new Item(item.itemObject), 1);
            inventoryUI.UpdateDisplay();
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Item.Clear();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
        }
    }
}
