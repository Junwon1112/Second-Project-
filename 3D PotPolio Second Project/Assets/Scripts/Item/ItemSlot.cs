using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
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

    //������ ���Կ� ������ �Ҵ�
    //������ ���Կ� ������ ���� �߰�
    //������ ���Կ� ������ ���� ����
    //������ ����ִ��� Ȯ��


}
