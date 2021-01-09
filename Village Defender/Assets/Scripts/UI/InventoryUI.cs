using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryObject inventory;
    public Vector2 startPosition;
    public int Columns;
    public int X_Margin;
    public int Y_Margin;

    Dictionary<InventorySlot, GameObject> itemDisplay = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Item.Count; i++)
        {
            var _item = inventory.Container.Item[i];
            var slot = inventory.Database.GetItem[inventory.Container.Item[i].ID];
            Debug.Log(slot);
            var UiSetting = Instantiate(slot.UISettings.gameObject, Vector3.zero, Quaternion.identity, transform);
            UiSetting.GetComponent<RectTransform>().localPosition = GetPosition(i);
            UiSetting.GetComponentInChildren<TextMeshProUGUI>().text = _item.amount.ToString("n0");
            UiSetting.GetComponent<ItemSlotUISettings>().UpdateUiSettings(slot);
            itemDisplay.Add(_item, UiSetting);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Item.Count; i++)
        {
            var _item = inventory.Container.Item[i];
            var slot = inventory.Database.GetItem[inventory.Container.Item[i].ID];

            if (itemDisplay.ContainsKey(_item))
            {
                itemDisplay[_item].GetComponentInChildren<TextMeshProUGUI>().text = _item.amount.ToString("n0");
            }
            else
            {
                var UiSetting = Instantiate(slot.UISettings.gameObject, Vector3.zero, Quaternion.identity, transform);
                UiSetting.GetComponent<RectTransform>().localPosition = GetPosition(i);
                UiSetting.GetComponentInChildren<TextMeshProUGUI>().text = _item.amount.ToString("n0");
                UiSetting.GetComponent<ItemSlotUISettings>().UpdateUiSettings(slot);
                itemDisplay.Add(_item, UiSetting);
            }
        }
    }

    public Vector3 GetPosition(int i) {

        return new Vector3(startPosition.x + (X_Margin * (i % Columns)), startPosition.y + (-Y_Margin * (i / Columns)), 0f);
    }

   
}
