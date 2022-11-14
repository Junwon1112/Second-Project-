using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempSlotInfoUI : MonoBehaviour
{
    //기본적으로 Info창 아이템 이미지로 이용하며
    //상속을 통해 새로운 클래스를 만들어 Split창에 이용할 것임

    public Image itemImage;                //Image에 프로퍼티로 스프라이트가 존재한다. 

    // 아이템 움직일 떄 사용
    public ItemData tempSlotItemData;   //tempSlot을 발생시킨곳에서 받아온다.
    public uint tempSlotItemCount;      //tempSlot을 발생시킨곳에서 받아온다.

    

    private void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }



    public void SetTempSlotWithData(ItemData itemData, uint count)
    {
        itemImage.sprite = itemData.itemIcon;   //여기서 두번쨰 스플릿할때 에러남(아마 상속받아서 split쪽에서 ok누른뒤 에러나는거 같음)
        tempSlotItemData = itemData;
        tempSlotItemCount = count;
    }

}
