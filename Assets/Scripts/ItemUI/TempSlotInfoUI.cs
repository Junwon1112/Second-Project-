using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Infoâ ������ �̹����� �̿�
/// </summary>
public class TempSlotInfoUI : ItemSlotUI_Basic
{
    Image itemImage;                //Image�� ������Ƽ�� ��������Ʈ�� �����Ѵ�. 

    // ������ ������ �� ���
    ItemData itemData;   //tempSlot�� �߻���Ų������ �޾ƿ´�.
    uint slotUICount;      //tempSlot�� �߻���Ų������ �޾ƿ´�.

    public override Image ItemImage { get => itemImage; set => itemImage = value; }
    public override ItemData ItemData { get => itemData; set =>itemData = value; }
    public override uint SlotUICount { get => slotUICount; set =>slotUICount = value; }

    //RectTransform rectTransform_TempSlotInfo;

    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();

    }

}
