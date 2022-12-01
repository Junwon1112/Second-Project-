using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipSlotUI : TempSlotInfoUI
{
    //public bool isSpliting = false;     //SplitUI에서 OK버튼 누르면 true로 바꿔줌
    private TextMeshProUGUI takeSlotItemCountText;
    int takeID = -1;
    public int equipSlotID = 1001;     //장비창 무기 슬롯 아이디 = 1001 번

    void Awake()
    {
        this.itemImage = GetComponentInChildren<Image>();
        takeSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ClearTempSlot();
    }

    public void ClearTempSlot()
    {
        itemImage.color = Color.clear;
        takeSlotItemCountText.alpha = 0;
        //isSpliting = false;   //splitUI에서 처리
        takeSlotItemData = null;
        takeSlotItemCount = 0;
    }

    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //여기서 두번쨰 스플릿할때 에러남(아마 상속받아서 split쪽에서 ok누른뒤 에러나는거 같음)
        itemImage.color = Color.white;

        takeSlotItemData = itemData;
        takeSlotItemCount = count;

        takeSlotItemCountText.text = takeSlotItemCount.ToString();
        takeSlotItemCountText.alpha = 1;
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
