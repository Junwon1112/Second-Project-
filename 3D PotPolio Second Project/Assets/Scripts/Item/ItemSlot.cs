using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    //필요한것 - 아이템 데이터랑 아이템 수량
    ItemData slotItemData;
    uint itemCount;

    public ItemData SlotItemData
    {
        get
        {
            return slotItemData;
        }
        set
        {
            if(slotItemData != value)
            {
                slotItemData = value;
            }
        }
    }

    public uint ItemCount
    { get; set; }

    public ItemSlot() { }
    public ItemSlot(ItemData data, uint count)
    {
        slotItemData = data;
        itemCount = count;
    }

    public ItemSlot(ItemSlot newItemSlot)
    {
        slotItemData = newItemSlot.slotItemData;
        itemCount = newItemSlot.itemCount;
    }

    //아이템 슬롯에 아이템 할당
    //아이템 슬롯에 아이템 갯수 추가
    //아이템 슬롯에 아이템 갯수 감소
    //슬롯이 비어있는지 확인


}
