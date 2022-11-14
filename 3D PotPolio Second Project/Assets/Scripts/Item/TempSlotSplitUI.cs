using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempSlotSplitUI : TempSlotInfoUI
{
    public bool isSpliting = false;     //SplitUI���� OK��ư ������ true�� �ٲ���

    private void Update()
    {
        //���� ���̶�� ������
        transform.position = (Vector3)Mouse.current.position.ReadValue();

    }

    public void ClearTempSlot()
    {
        itemImage.sprite = null;
        isSpliting = false;
        tempSlotItemData = null;
        tempSlotItemCount = 0;
    }

    //--------��ӹ��� TempSlotInfoUI�� ����--------------------------------

    //==================================================================================================================
    //public Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    //// ������ ������ �� ���
    //public ItemData tempSlotItemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    //public uint tempSlotItemCount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    //private void Awake()
    //{
    //    itemImage = GetComponentInChildren<Image>();
    //}

    //public void SetTempSlotWithData(ItemData itemData, uint count)
    //{
    //    itemImage.sprite = itemData.itemIcon;
    //    tempSlotItemData = itemData;
    //    tempSlotItemCount = count;
    //}
    //==================================================================================================================
}
