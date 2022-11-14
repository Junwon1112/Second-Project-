using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempSlotSplitUI : TempSlotInfoUI
{
    public bool isSpliting = false;     //SplitUI에서 OK버튼 누르면 true로 바꿔줌

    private void Update()
    {
        //분할 중이라면 실행하
        transform.position = (Vector3)Mouse.current.position.ReadValue();

    }

    public void ClearTempSlot()
    {
        itemImage.sprite = null;
        isSpliting = false;
        tempSlotItemData = null;
        tempSlotItemCount = 0;
    }

    //--------상속받은 TempSlotInfoUI의 내용--------------------------------

    //==================================================================================================================
    //public Image itemImage;                //Image에 프로퍼티로 스프라이트가 존재한다. 

    //// 아이템 움직일 떄 사용
    //public ItemData tempSlotItemData;   //tempSlot을 발생시킨곳에서 받아온다.
    //public uint tempSlotItemCount;      //tempSlot을 발생시킨곳에서 받아온다.

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
