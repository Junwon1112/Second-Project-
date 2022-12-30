using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot  //: MonoBehaviour
{
    public int slotID = -1; //�ܺο��� ���° �������� �����ϴ� ���� ���̵�. �Ҵ����� -1���� �Ҵ��� ����

    //�ʿ��Ѱ� - ������ �����Ͷ� ������ ����
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

    //������ ���Կ� ������ ���� �߰�
    //������ ���Կ� ������ ���� ����
 


    //������ ���Կ� ������ �Ҵ�(���°ſ��� �ִ°ŷ�)
    public void AssignSlotItem(ItemData newItemData, uint newItemCount = 1 )
    {
        if(IsEmpty())
        {
            SlotItemData = newItemData;
            ItemCount = newItemCount;
            Debug.Log("�� ���Կ� �Ҵ��Ѵ�");
        }
        
    }

    //(���Կ� �������� ������ �� �Ҵ�)
    public void IncreaseSlotItem(uint count = 1)
    {
        if(!IsEmpty())
        {
            if(ItemCount + count <= slotItemData.itemMaxCount)
            {
                ItemCount += count;
                Debug.Log("���� ���Կ� �߰��Ѵ�");
            }
            else
            {
                ItemCount = (uint)slotItemData.itemMaxCount;
                Debug.Log("���� ������ �����ִ�");
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



    //������ ����ִ��� Ȯ��
    public bool IsEmpty()
    {
        return (slotItemData == null);
    }
}
