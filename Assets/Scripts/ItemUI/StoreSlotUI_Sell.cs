using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSlotUI_Sell : StoreSlotUI_Basic
{
    private ItemData itemData;

    private Image itemImage;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice_Text;

    public override ItemData ItemData { get; set; }
    protected override Image ItemImage { get; set; }
    protected override TextMeshProUGUI ItemName { get; set; }
    protected override TextMeshProUGUI ItemPrice_Text { get; set; }

    private void Awake()
    {
        ItemImage = GetComponent<Image>();
        ItemName = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        ItemPrice_Text = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
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

    /// <summary>
    /// BuyButton 클래스의 버튼에서 실행할 예정
    /// </summary>
    public void Sell()
    {

    }
}
