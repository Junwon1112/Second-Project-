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
    public ItemData takeSlotItemData;   //tempSlot을 발생시킨곳에서 받아온다.
    public uint takeSlotItemCount;      //tempSlot을 발생시킨곳에서 받아온다.

    

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }

}
