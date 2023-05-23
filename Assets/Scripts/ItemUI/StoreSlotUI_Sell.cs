using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSlotUI_Sell : StoreSlotUI_Basic
{
    private ItemData itemData;
    private uint count;

    private Image itemImage;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice_Text;

    private TextMeshProUGUI itemCount_Text;
    public int targetItemSlotID;

    public override ItemData ItemData { get; set; }
    protected override Image ItemImage { get; set; }
    protected override TextMeshProUGUI ItemName { get; set; }
    protected override TextMeshProUGUI ItemPrice_Text { get; set; }

    public uint Count { get; set; }
    public TextMeshProUGUI ItemCount_Text { get; set; }

    private void Awake()
    {
        ItemImage = GetComponent<Image>();
        ItemName = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        ItemPrice_Text = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemCount_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void SetItem(Sprite _itemSprite, string _itemName, int _itemPrice)
    {
        ItemImage.sprite = _itemSprite;
        ItemName.text = _itemName;
        ItemPrice_Text.text = _itemPrice.ToString();
    }

    public override void SetItem(Sprite _itemSprite, string _itemName, string _itemPrice_text)
    {
        ItemImage.sprite = _itemSprite;
        ItemName.text = _itemName;
        ItemPrice_Text.text = _itemPrice_text;
    }
}
