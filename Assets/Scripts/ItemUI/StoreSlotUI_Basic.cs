using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class StoreSlotUI_Basic : MonoBehaviour
{
    public abstract ItemData ItemData { get; set; }

    protected abstract Image ItemImage { get; set; }
    protected abstract TextMeshProUGUI ItemName { get; set; }
    protected abstract TextMeshProUGUI ItemPrice_Text { get; set; }

    public abstract void SetItem(Sprite _itemSprite, string _itemName, int _itemPrice);
    public abstract void SetItem(Sprite _itemSprite, string _itemName, string _itemPrice_text);

}
