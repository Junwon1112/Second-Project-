using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Info창 아이템 이미지로 이용
/// </summary>
public class TempSlotInfoUI : ItemSlotUI_Basic
{
    Image itemImage;                //Image에 프로퍼티로 스프라이트가 존재한다. 

    // 아이템 움직일 떄 사용
    ItemData itemData;   //tempSlot을 발생시킨곳에서 받아온다.
    uint slotUICount;      //tempSlot을 발생시킨곳에서 받아온다.

    public override Image ItemImage { get => itemImage; set => itemImage = value; }
    public override ItemData ItemData { get => itemData; set =>itemData = value; }
    public override uint SlotUICount { get => slotUICount; set =>slotUICount = value; }

    //RectTransform rectTransform_TempSlotInfo;

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();

    }

}
