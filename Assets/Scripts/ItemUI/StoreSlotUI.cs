using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSlotUI : MonoBehaviour
{
    public ItemData sellingItemData;

    private Image itemImage;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice_Text;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        itemName = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        itemPrice_Text = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(Sprite _itemSprite, string _itemName, int _itemPrice)
    {
        itemImage.sprite = _itemSprite;
        itemName.text = _itemName;
        itemPrice_Text.text = _itemPrice.ToString();
    }

    public void SetItem(Sprite _itemSprite, string _itemName, string _itemPrice_text)
    {
        itemImage.sprite = _itemSprite;
        itemName.text = _itemName;
        itemPrice_Text.text = _itemPrice_text;
    }
}
