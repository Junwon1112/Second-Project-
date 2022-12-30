using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot  //: MonoBehaviour
{
    public int slotID = -1; //외부에서 몇번째 슬롯인지 구분하는 슬롯 아이디. 할당전엔 -1값을 할당해 놓음

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

    //아이템 슬롯에 아이템 갯수 추가
    //아이템 슬롯에 아이템 갯수 감소
 


    //아이템 슬롯에 아이템 할당(없는거에서 있는거로)
    public void AssignSlotItem(ItemData newItemData, uint newItemCount = 1 )
    {
        if(IsEmpty())
        {
            SlotItemData = newItemData;
            ItemCount = newItemCount;
            Debug.Log("빈 슬롯에 할당한다");
        }
        
    }

    //(슬롯에 아이템이 존재할 때 할당)
    public void IncreaseSlotItem(uint count = 1)
    {
        if(!IsEmpty())
        {
            if(ItemCount + count <= slotItemData.itemMaxCount)
            {
                ItemCount += count;
                Debug.Log("기존 슬롯에 추가한다");
            }
            else
            {
                ItemCount = (uint)slotItemData.itemMaxCount;
                Debug.Log("기존 슬롯이 꽉차있다");
            }
        }
    }

    public void DecreaseSlotItem(uint count = 1)
    {
        if(ItemCount - count > 0)
        {
            ItemCount -= count;
        }
        else if(ItemCount - count <= 0)
        {
            ItemCount = 0;
            SlotItemData = null;
        }
    }

    public void ClearSlotItem()
    {
        SlotItemData = null;
        ItemCount = 0;
    }



    //슬롯이 비어있는지 확인
    public bool IsEmpty()
    {
        return (slotItemData == null);
    }
}
