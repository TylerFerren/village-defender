using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ItemSlotUISettings : MonoBehaviour
{
    [Header("Bacdground")]
    public Color backgroundColor;

    [Header("Icon")]
    public Sprite icon;
    public Color iconColor;

    [Header("Name")]
    public string Name;

    [Header("Amount")]
    public int Amount;

    public Image _background;
    public Image _icon;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _amount;

    private void Awake()
    {
        _background = transform.Find("Background").GetComponent<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _amount = transform.Find("Amount").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUiSettings(ItemObject itemObject)
    {
        _background.color = itemObject.backgroundColor;
        _icon.sprite = itemObject.icon;
        _icon.color = itemObject.iconColor;
        _name.text = itemObject.name;
        //_amount.text = itemObject.Amount.ToString("n0");
    }



}
