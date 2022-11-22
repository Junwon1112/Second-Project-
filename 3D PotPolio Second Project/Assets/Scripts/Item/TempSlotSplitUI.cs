using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class TempSlotSplitUI : TempSlotInfoUI
{
    //public bool isSpliting = false;     //SplitUI에서 OK버튼 누르면 true로 바꿔줌
    private TextMeshProUGUI tempSlotItemCountText;
    int takeID = -1;

    void Awake()
    {
        this.itemImage = GetComponentInChildren<Image>();
        tempSlotItemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //분할 중이라면 실행하기
        transform.position = (Vector3)Mouse.current.position.ReadValue();

    }

    public void ClearTempSlot()
    {
        itemImage.sprite = null;
        //isSpliting = false;   //splitUI에서 처리
        tempSlotItemData = null;
        tempSlotItemCount = 0;
    }

    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //여기서 두번쨰 스플릿할때 에러남(아마 상속받아서 split쪽에서 ok누른뒤 에러나는거 같음)
        tempSlotItemData = itemData;
        tempSlotItemCount = count;
        tempSlotItemCountText.text = tempSlotItemCount.ToString();
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
